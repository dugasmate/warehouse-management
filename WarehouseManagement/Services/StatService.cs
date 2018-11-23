using System;
using System.Collections.Generic;
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


        public async Task<Stats> MakeStatistics()
        {
            double totalWeight = await TotalWeightCounter();
            int totalValue = await TotalValueCounter();
            var maxQuantity = await MostItemsFinder();
            var heaviestProduct = await HeaviestItemFinder();
            var euroRate = await mnbstatService.GetEuroRate();
            var totalEuroValue = Math.Round(totalValue / euroRate, 2);
            Stats stats = new Stats
            {
                TotalWeight = totalWeight,
                TotalValue = totalValue,
                EuroValue = totalEuroValue,
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

        public async Task<int> TotalValueCounter()
        {
            var products = await productRepository.ReadAllAsync();
            int totalValue = 0;
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
            var productsSortedByWeight = products.OrderByDescending(o => o.Weight).ToList();
            var heaviestItem = new Product { Name = productsSortedByWeight[0].Name, Weight = productsSortedByWeight[0].Weight * productsSortedByWeight[0].Quantity };
            return heaviestItem;
        }
    }
}
