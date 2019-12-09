using System;
using System.Collections.Generic;
using System.Linq;
using SEP3_warehouseAPI.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace SEP3_warehouseAPI.Data
{
    public class WarehouseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public WarehouseContext(DbContextOptions<WarehouseContext> options)
            : base(options)
        { }

        public Microsoft.EntityFrameworkCore.DbSet<Item> Items { get; set; }

    }
}
