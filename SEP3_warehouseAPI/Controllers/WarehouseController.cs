using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using SEP3_warehouseAPI.Data;
using SEP3_warehouseAPI.Model;
using System.Text.Json;

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

        [ProducesResponseType(typeof(CheckOrderResponse), 200)]
        [HttpPost("checkOrder")]
        public Order Get([FromBody]Order order)
        {   
            
            string json = JsonSerializer.Serialize(order.orderedItems);
            IEnumerable<WarehouseItem> itemsordered = JsonSerializer.Deserialize<IEnumerable<WarehouseItem>>(json);

            //  IEnumerable<int?> itemsordered = JsonSerializer.Deserialize<Items>(json.ToString()).items.Select(i=>i.barcode);

            var missingitems = new List<WarehouseItem>();

            foreach (var item in itemsordered)
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
            return order;
        }

        [HttpPost("restock")]
        public String AddItem(int id, int barCode, string name, string Description, int stock)
        {
            WarehouseItem i = db.Stock.Find(barCode);

            if (i == null)

                db.Add(new WarehouseItem()
                {
                    name = name,
                    barcode = barCode,
                    description = Description,
                    amount = stock,


                });

            else
            {
                i.amount += stock;

                db.SaveChangesAsync();

            }

            return "successful";
        }
        [HttpGet("getAll")]
        public IList<WarehouseItem> ShowAllItems()
        {
            return (IList<WarehouseItem>)db.Stock;

        }


        [HttpPost("addStock")]
        public async void AddStock(int barCode, int stock)
        {
            WarehouseItem i = db.Stock.Find(barCode);

            if (i == null)

            {
                
            }

            else
            {
                i.amount += stock;

                await db.SaveChangesAsync();

            }
        }

        public String ToStringOrder(List<WarehouseItem> order)
        {
            foreach (var item in order)
                return "could not find" + item.amount + " of" + item.name;

            return " order not successful";
        }

    }
}
