using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SEP3_warehouseAPI.Data;
using SEP3_warehouseAPI.Model;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace SEP3_warehouseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseContext db;


        public WarehouseController(WarehouseContext context)
        {
            db = context;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("checkOrder")]
        public IActionResult Get([FromBody]Order order)
        {   
          //  string json = JsonSerializer.Serialize(order.orderedItems);
          //  IEnumerable<WarehouseItem> itemsordered = JsonSerializer.Deserialize<IEnumerable<WarehouseItem>>(json);

            var itemsOrdered = order.orderedItems;

            var missingitems = new List<WarehouseItem>();

            foreach (var item in itemsOrdered)
            {
                WarehouseItem i = db.Stock.Where(b=>b.barcode == (int)item.barcode).SingleOrDefault();
               
                if (i == null)
                {
                    missingitems.Add(new WarehouseItem()
                    {        
                        amount = i.amount,
                        description = i.description,
                        name = i.name,
                        barcode = i.barcode,
                    });
                }
                if (i != null)
                {
                    int? missing = item.barcode =- i.amount;
                    if (missing > 0)
                    {
                        i.amount = 0;
                        missingitems.Add(new WarehouseItem()
                        {
                            id = item.id,
                           amount = missing,
                            description = item.description,
                            name = item.name,
                            barcode = item.barcode

                        });
                    }
                    else
                    {
                        //enough items available
                        i.amount -= item.amount;
                        db.SaveChangesAsync();
                    }
                }
            }

            order.orderInfo = ToStringOrder(missingitems);
            return Ok(order);
        }


        [HttpPost("restock")]
        public void AddItem(int barCode, string name, string description, int stock)
        {
            WarehouseItem i = db.Stock.Find(barCode);

            if (i == null)
            {

                db.Add(new WarehouseItem()
                {
                    name = name,
                    barcode = barCode,
                    description = description,
                    amount = stock,

                });

            }

            else
            {
                i.amount += stock;

                db.SaveChangesAsync();

            }

        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IList<WarehouseItem>), 200)]
        public IActionResult ShowAllItems()
        {
            return Ok(db.Stock.ToList());
        }


        [HttpPost("addStock")]
        public async void AddStock(int barCode, int stock)
        {
            WarehouseItem i = db.Stock.Find(barCode);

            if (i != null)

            {
                i.amount += stock;

                await db.SaveChangesAsync();
            }

        }

        private  String ToStringOrder(List<WarehouseItem> order)
        {
            return "could not find the following items:" + string.Join(",", order.Select(x => x.name) + string.Join(",", order.Select(x => x.amount)));
        }
       

    }
}
