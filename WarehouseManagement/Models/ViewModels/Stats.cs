using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Models.ViewModels
{
    public class Stats
    {
        public double TotalWeight { get; set; }
        public int TotalValue { get; set; }
        public decimal EuroValue { get; set; }
        public double MaxQuantity { get; set; }
        public Product HeaviestProduct { get; set; }
    }
}
