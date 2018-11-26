using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;
using WarehouseManagement.Models.ViewModels;

namespace WarehouseManagement.Services
{
    public class StockService
    {
        public ICRUDRepository<Product> productRepository;
        public MNBService mnbService;

        public StockService(ICRUDRepository<Product> productRepository, MNBService mnbService)
        {
            this.productRepository = productRepository;
            this.mnbService = mnbService;
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
                    Price = (product.Price * product.Quantity).ToString("C0", CultureInfo.CreateSpecificCulture("hu-HU")),
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Weight = product.Weight * product.Quantity,
                    EuroPrice = ((product.Price / euroRate) * product.Quantity).ToString("C2", CultureInfo.CreateSpecificCulture("de-DE"))
                };
                productViews.Add(viewModel);
            }
            return productViews;
        }

        public async Task UpdateStock(List<StockDTO> stocks)
        {
            for (int i = 0; i < stocks.Count; i++)
            {

                var product = await productRepository.ReadAsync(stocks[i].Id);
                if (product.Quantity + stocks[i].Quantity < 0)
                {
                    product.Quantity = 0;
                }
                else
                {
                    product.Quantity += stocks[i].Quantity;
                }
                await productRepository.UpdateAsync(product);
            }

        }

    }
}
