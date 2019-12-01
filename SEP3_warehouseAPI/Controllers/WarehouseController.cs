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

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return db.Items;
        }
    }
}
