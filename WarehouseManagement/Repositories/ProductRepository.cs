using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Repository
{
    public class ProductRepository : ICRUDRepository<Product>
    {
        private StockContext stockContext;

        public ProductRepository(StockContext stockContext)
        {
            this.stockContext = stockContext;
        }

        public async Task Create(Product product)
        {
            await stockContext.Products.AddAsync(product);
            await stockContext.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Read(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> ReadAll()
        {
            var products = await stockContext.Products.ToListAsync();
            return products;
        }

        public Task Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
