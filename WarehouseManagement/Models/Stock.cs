using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Models
{
    public class Stock
    { 
        public long StockId { get; set; }

        public virtual Product Product { get; set; }
    }
}
