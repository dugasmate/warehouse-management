﻿using System;
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
        public ICRUDRepository<Stock> stockRepository;
        public StockService stockService;
        public ICRUDRepository<Product> productRepository;
        public MNBService mnbstatService;

        public StatService(ICRUDRepository<Stock> stockRepository, StockService stockService, ICRUDRepository<Product> productRepository, MNBService mnbstatService)
        {
            this.stockRepository = stockRepository;
            this.stockService = stockService;
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
            var stock = await stockRepository.ReadAllAsync();
            double totalWeight = 0;
            for (int i = 0; i < stock.Count; i++)
            {
                totalWeight += stock[i].Product.Weight;
            }

            return totalWeight;
        }

        public async Task<int> TotalValueCounter()
        {
            var stock = await stockRepository.ReadAllAsync();
            int totalValue = 0;
            for (int i = 0; i < stock.Count; i++)
            {
                totalValue += stock[i].Product.Price;
            }

            return totalValue;
        }

        public async Task<SortedStock> MostItemsFinder()
        {
            var stock = await stockService.SortStockAsync();
            var stockSortedByQuantity = stock.OrderByDescending(o => o.Quantity).ToList();
            return stockSortedByQuantity[0];
        }

        public async Task<Product> HeaviestItemFinder()
        {
            var products = await productRepository.ReadAllAsync();
            var productsSortedByWeight = products.OrderByDescending(o => o.Weight).ToList();
            return productsSortedByWeight[0];
        }
    }
}
