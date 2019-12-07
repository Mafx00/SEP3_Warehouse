using SEP3_warehouseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP3_warehouseAPI.Model
{
    public class Order
    {
        public long OrderId { get; set; }

        public IEnumerable<Item> items { get; set; }

        public List<long> ItemId { get; set; }
    }
}
