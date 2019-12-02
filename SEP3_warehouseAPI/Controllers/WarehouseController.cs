using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SEP3_warehouseAPI.Data;
using SEP3_warehouseAPI.Models;

namespace SEP3_warehouseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly Warehouse_Context db;


        public WarehouseController(Warehouse_Context context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddItems(long id, long warehouseId, int amount)
        {
            var item = await db.Items.FindAsync(id);

            var itemUpdate = new Item
            {
                ItemId = id,
                BarCode = item.BarCode,
                Description = item.Description,
                Stock = item.Stock + amount,
                WarehouseId = warehouseId

            };

            db.Items.Add(itemUpdate);
            await db.SaveChangesAsync();
            return Ok();
        }

    }
}
