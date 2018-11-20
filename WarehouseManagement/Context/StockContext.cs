using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement
{
    public class StockContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Product> Products { get; set; }

        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {

        }
    }
}
