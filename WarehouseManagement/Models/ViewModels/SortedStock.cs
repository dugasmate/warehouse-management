using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Models.ViewModels
{
    public class SortedStock
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
