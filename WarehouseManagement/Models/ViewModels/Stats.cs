﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Models.ViewModels
{
    public class Stats
    {
        public double TotalWeight { get; set; }
        public int TotalValue { get; set; }
        public SortedStock MaxQuantity { get; set; }
        public Product HeaviestProduct { get; set; }
    }
}
