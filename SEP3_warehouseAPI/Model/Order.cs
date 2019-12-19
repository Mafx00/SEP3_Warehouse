using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP3_warehouseAPI.Model
{
    public class Order
    {
        public int orderId { get; set; }
        public string account { get; set; }
        public IEnumerable<WarehouseItem> orderedItems { get; set; }
        public string orderInfo { get; set; }

    }

}
