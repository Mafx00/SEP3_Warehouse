using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SEP3_warehouseAPI.Model
{
    public class Warehouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WarehouseId { get; set; }
        public string Location { get; set; }
        public ICollection<WarehouseItem> items { get; set; }
    }
}
