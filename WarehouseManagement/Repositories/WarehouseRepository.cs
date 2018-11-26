using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WarehouseManagement.Context;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Repositories
{
    public class WarehouseRepository : ICRUDRepository<Product>
    {
        private WarehouseContext warehouseContext;

        public WarehouseRepository(WarehouseContext stockContext)
        {
            this.warehouseContext = stockContext;
        }

        public async Task CreateAsync(Product product)
        {

            await warehouseContext.Products.AddAsync(product);
            await warehouseContext.SaveChangesAsync();

        }

        public async Task<Product> ReadAsync(long id)
        {
            var product = await warehouseContext.Products.FindAsync(id);
            return product;
        }

        public async Task<List<Product>> ReadAllAsync()
        {
            var products = await warehouseContext.Products.ToListAsync();
            return products;
        }

        public async Task UpdateAsync(Product product)
        {
            warehouseContext.Products.Update(product);
            await warehouseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            warehouseContext.Products.Remove(product);
            await warehouseContext.SaveChangesAsync();
        }
    }
}
