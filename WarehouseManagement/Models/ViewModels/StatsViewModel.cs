using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Models.ViewModels
{
    public class StatsViewModel
    {
        public double TotalWeight { get; set; }
        public string TotalValue { get; set; }
        public string EuroValue { get; set; }
        public Product MaxQuantity { get; set; }
        public Product HeaviestProduct { get; set; }
    }
}

