﻿using Microsoft.EntityFrameworkCore;
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
            bool isInvalid = false;
            PropertyInfo[] properties = typeof(Product).GetProperties();
            for (int i = 1; i < properties.Length; i++)
            {
                if (properties[i].GetValue(product) == null || string.IsNullOrWhiteSpace(properties[i].GetValue(product).ToString()) || properties[i].GetValue(product).ToString() == "0")
                {
                    isInvalid = true;
                }
            }

            if (!isInvalid)
            {
                await stockContext.Products.AddAsync(product);
                await stockContext.SaveChangesAsync();
            }
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
            var productCount = stockContext.Products.Count(a => a.Id == product.Id);
            bool isInvalid = false;

            if (productCount != 0)
            {
                PropertyInfo[] properties = typeof(Product).GetProperties();
                for (int i = 1; i < properties.Length; i++)
                {
                    if (properties[i].Name != "Quantity")
                    {
                        if (properties[i].GetValue(product) == null || string.IsNullOrWhiteSpace(properties[i].GetValue(product).ToString()) || properties[i].GetValue(product).ToString() == "0")
                        {
                            isInvalid = true;
                        }
                    }

                }

                if (!isInvalid)
                {
                    stockContext.Products.Update(product);
                    await stockContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAsync(long id)
        {
            var productToDelete = await stockContext.Products.FindAsync(id);
            if (productToDelete != null)
            {
                stockContext.Products.Remove(productToDelete);
                await stockContext.SaveChangesAsync();
            }
        }
    }
}
