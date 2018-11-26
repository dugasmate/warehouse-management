using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.Context
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {

        }
    }
}
