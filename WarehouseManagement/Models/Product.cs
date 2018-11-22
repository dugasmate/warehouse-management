using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
    }
}
