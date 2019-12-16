using System;
using System.Collections.Generic;

namespace SEP3_warehouseAPI.Model
{
    public partial class WarehouseItem
    {
        public int id { get; set; }
        public int? barcode { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? amount { get; set; }
    }
}
