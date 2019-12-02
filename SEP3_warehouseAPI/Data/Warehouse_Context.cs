using Microsoft.EntityFrameworkCore;
using SEP3_warehouseAPI.Models;

namespace SEP3_warehouseAPI.Data
{
    public class Warehouse_Context : DbContext

    {
            public DbSet<Item> Items { get; set; }
            public DbSet<Warehouse> Warehouses { get; set; }
 
    }
    }
