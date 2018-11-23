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
    public class ProductRepository : ICRUDRepository<Product>
    {
        private WarehouseContext stockContext;

        public ProductRepository(WarehouseContext stockContext)
        {
            this.stockContext = stockContext;
        }

        public async Task CreateAsync(Product product)
        {

            await stockContext.Products.AddAsync(product);
            await stockContext.SaveChangesAsync();

        }

        public async Task<Product> ReadAsync(long id)
        {
            var product = await stockContext.Products.FindAsync(id);
            return product;
        }

        public async Task<List<Product>> ReadAllAsync()
        {
            var products = await stockContext.Products.ToListAsync();
            return products;
        }

        public async Task UpdateAsync(Product product)
        {
                    stockContext.Products.Update(product);
                    await stockContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
                stockContext.Products.Remove(product);
                await stockContext.SaveChangesAsync();
        }
    }
}
