using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SEP3_warehouseAPI.Data;
using SEP3_warehouseAPI.Model;
using SEP3_warehouseAPI.Models;

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

        [HttpGet]
        public string Get(Order order)
        {
            

           var missingitems = new List<Item>(); 

            foreach (var item in order.items)
            {
                var i = db.Items.Find(item.ItemId);
                if (i == null)
                {
                    missingitems.Add(new Item()
                    {
                        ItemId = item.ItemId,
                        Stock = item.Stock,
                        Description = item.Description,
                        Name = item.Name,
                        BarCode = item.BarCode,
                }); 
                    db.SaveChangesAsync();

                    return "Couldn't find " + item.Name + ". Missing: " + item.Stock; 

                }
                int missing = 0;
                if (i != null)
                {
                    missing = (item.Stock - i.Stock);
                    i.Stock = 0;


                    missingitems.Add(new Item()
                    {
                        ItemId = item.ItemId,
                        Stock = missing,
                        Description = item.Description,
                        Name = item.Name,
                        
                        BarCode = item.BarCode

                    });
                    db.SaveChangesAsync();

                    return "Missing " + missing + " of item " + item.Stock;
                }

                else
                {
                    i.Stock -= item.Stock;
                    db.SaveChangesAsync();

                    return item.Name + " successfully ordered";
                }

                }
                return " ";

        }

          /*  if (missingitems == null)
                return "Order Sucsseful";
            else
                foreach (var item in missingitems)
                    return "Couldn't return " + item.Stock + " of item: " + item.Name; */
            

        [HttpPost]
        public async void AddItem(int barCode, string Description, int stock)
        {
            Item i = db.Items.Find(barCode);

        if (i == null)

            db.Add(new Item()
            {
                BarCode = barCode,
                Description = Description,
                Stock = stock,
              

            });

        else
        {
                i.Stock += stock;

                await db.SaveChangesAsync();

         }
        }

        public IList<Item> ShowAllItems()
        {
            return (IList<Item>) db.Items;
            
        }

    }
}
