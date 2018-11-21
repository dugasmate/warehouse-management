using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Services
{
    public class StatService
    {
        public ICRUDRepository<Stock> stockRepository;

        public StatService (ICRUDRepository<Stock> stockRepository)
        {
            this.stockRepository = stockRepository;
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
    }
}
