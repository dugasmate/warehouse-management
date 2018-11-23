using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Services
{
    public class ProductService
    {
        public ICRUDRepository<Product> productRepository;

        public ProductService(ICRUDRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task CreateAsync(Product product)
        {
            bool isInvalid = false;
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
                await productRepository.CreateAsync(product);
            }
        }

        public async Task<Product> ReadAsync(long id)
        {
            var product = await productRepository.ReadAsync(id);
            return product;
        }

        public async Task<List<Product>> ReadAllAsync()
        {
            var products = await productRepository.ReadAllAsync();
            return products;
        }

        public async Task UpdateAsync(Product product)
        {
            bool isInvalid = false;
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
                    await productRepository.UpdateAsync(product);
                }
            
        }

        public async Task DeleteAsync(long id)
        {
            var productToDelete = await productRepository.ReadAsync(id);
            if (productToDelete != null)
            {
                await productRepository.DeleteAsync(productToDelete);
            }
        }
    }
}
