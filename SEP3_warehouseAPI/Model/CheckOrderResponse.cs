
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP3_warehouseAPI.Model
{
    public class CheckOrderResponse
    {
        public Order OriginalOrder { get; set; }
        public IEnumerable<WarehouseItem> MissingItems { get; set; }
        public String status { get; set; }
    }
}
