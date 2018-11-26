using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;
using WarehouseManagement.Models.ViewModels;

namespace WarehouseManagement.Services
{
    public class ProductService
    {
        public ICRUDRepository<Product> productRepository;
        public MNBService mnbService;

        public ProductService(ICRUDRepository<Product> productRepository, MNBService mnbService)
        {
            this.productRepository = productRepository;
            this.mnbService = mnbService;
        }

        public async Task CreateAsync(Product product)
        {
            bool isInvalid = false;
            PropertyInfo[] properties = typeof(Product).GetProperties();
            for (int i = 1; i < properties.Length; i++)
            {
                if (properties[i].Name != "Quantity")
                {
                    if (properties[i].GetValue(product) == null || string.IsNullOrWhiteSpace(properties[i].GetValue(product).ToString()) ||
                        properties[i].GetValue(product).ToString() == "0" || properties[i].GetValue(product).ToString().StartsWith("-"))
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

        public async Task<List<ProductViewModel>> ReadAllAsync()
        {
            var products = await productRepository.ReadAllAsync();
            var euroRate = await mnbService.GetEuroRate();
            List<ProductViewModel> productViews = new List<ProductViewModel>();

            foreach (var product in products)
            {
                var viewModel = new ProductViewModel
                {
                    Id = product.Id,
                    ProductCode = product.ProductCode,
                    Name = product.Name,
                    Price = product.Price.ToString("C0", CultureInfo.CreateSpecificCulture("hu-HU")),
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Weight = product.Weight,
                    EuroPrice = (product.Price / euroRate).ToString("C2", CultureInfo.CreateSpecificCulture("de-DE"))
                };
                productViews.Add(viewModel);
            }
            return productViews;
        }

        public async Task UpdateAsync(Product product)
        {
            bool isInvalid = false;
            PropertyInfo[] properties = typeof(Product).GetProperties();
            for (int i = 1; i < properties.Length; i++)
            {
                if (properties[i].Name != "Quantity")
                {
                    if (properties[i].GetValue(product) == null || string.IsNullOrWhiteSpace(properties[i].GetValue(product).ToString()) ||
                        properties[i].GetValue(product).ToString() == "0" || properties[i].GetValue(product).ToString().StartsWith("-"))
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

        public async Task<bool> CheckProducts()
        {
            bool productsExist = true;
            var products = await productRepository.ReadAllAsync();
            if (products.Count == 0)
            {
                productsExist = false;
            }
            return productsExist;
        }
    }
}
