﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SEP3_warehouseAPI.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int BarCode { get; set; }
        public string Description { get; set; }
        public int WarehouseId { get; set; }
    }
}