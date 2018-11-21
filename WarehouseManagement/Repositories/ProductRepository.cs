﻿using Microsoft.EntityFrameworkCore;
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

        public async Task CreateAsync(Product product)
        {
            await stockContext.Products.AddAsync(product);
            await stockContext.SaveChangesAsync();
        }

        public async Task<Product> ReadAsync(int id)
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

        public async Task DeleteAsync(int id)
        {
            var productToDelete = await stockContext.Products.FindAsync(id);
            if(productToDelete != null)
            {
                stockContext.Products.Remove(productToDelete);
            }

            await stockContext.SaveChangesAsync();
        }
    }
}