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
    public class StatService
    {
        public ICRUDRepository<Product> productRepository;
        public MNBService mnbstatService;

        public StatService(ICRUDRepository<Product> productRepository, MNBService mnbstatService)
        {
            this.productRepository = productRepository;
            this.mnbstatService = mnbstatService;
        }


        public async Task<StatsViewModel> MakeStatistics()
        {
            double totalWeight = await TotalWeightCounter();
            long totalValue = await TotalValueCounter();
            var maxQuantity = await MostItemsFinder();
            var heaviestProduct = await HeaviestItemFinder();
            var euroRate = await mnbstatService.GetEuroRate();
            StatsViewModel stats = new StatsViewModel
            {
                TotalWeight = totalWeight,
                TotalValue = totalValue.ToString("C0", CultureInfo.CreateSpecificCulture("hu-HU")),
                EuroValue = (totalValue / euroRate).ToString("C2", CultureInfo.CreateSpecificCulture("de-DE")),
                MaxQuantity = maxQuantity,
                HeaviestProduct = heaviestProduct
            };
            return stats;
        }

        public async Task<double> TotalWeightCounter()
        {
            var products = await productRepository.ReadAllAsync();
            double totalWeight = 0;
            for (int i = 0; i < products.Count; i++)
            {
                totalWeight += products[i].Weight * products[i].Quantity;
            }

            return totalWeight;
        }

        public async Task<long> TotalValueCounter()
        {
            var products = await productRepository.ReadAllAsync();
            long totalValue = 0;
            for (int i = 0; i < products.Count; i++)
            {
                totalValue += products[i].Price * products[i].Quantity;
            }

            return totalValue;
        }

        public async Task<Product> MostItemsFinder()
        {
            var stock = await productRepository.ReadAllAsync();
            var stockSortedByQuantity = stock.OrderByDescending(o => o.Quantity).ToList();
            return stockSortedByQuantity[0];
        }

        public async Task<Product> HeaviestItemFinder()
        {
            var products = await productRepository.ReadAllAsync();
            List<Product> productsTotalWeight = new List<Product>();
            foreach (var product in products)
            {
                productsTotalWeight.Add(new Product { Name = product.Name, Weight = product.Weight * product.Quantity });

            }
            var productsSortedByWeight = productsTotalWeight.OrderByDescending(o => o.Weight).ToList();
            return productsSortedByWeight[0];
        }
    }
}
