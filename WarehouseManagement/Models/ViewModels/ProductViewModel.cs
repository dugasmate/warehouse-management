using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Models.ViewModels
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public long Quantity { get; set; }
        public string EuroPrice { get; set; }
    }

}
