using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;
using WarehouseManagement.Models.ViewModels;

namespace WarehouseManagement.Services
{
    public class StockService
    {
        public ICRUDRepository<Stock> stockRepository;

        public StockService(ICRUDRepository<Stock> stockRepository)
        {
            this.stockRepository = stockRepository;
        }

        public async Task<List<SortedStock>> SortStockAsync()
        {
            var stock = await stockRepository.ReadAllAsync();
            var sortedList = stock.OrderBy(o => o.ProductId).ToList();
            List<SortedStock> sortedStock = new List<SortedStock>();
            int switcher = 0;
            for (int i = 0; i < sortedList.Count; i++)
            {
                if (i == 0)
                {
                    sortedStock.Add(new SortedStock { Name = sortedList[i].Product.Name, Quantity = 1 });
                }

                else if (sortedList[i].ProductId != sortedList[i - 1].ProductId)
                {
                    switcher += 1;
                    sortedStock.Add(new SortedStock { Name = sortedList[i].Product.Name, Quantity = 1 });
                }

                else
                {
                    sortedStock[switcher].Quantity += 1;
                }
            }
            return sortedStock;
        }

        public async Task ChangeItemCountAsync(Stock item, int count)
        {
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var copiedItem = new Stock { ProductId = item.ProductId };
                    await stockRepository.CreateAsync(copiedItem);
                }
            }

            if (count < 0)
            {
                count *= -1;
                for (int i = 0; i < count; i++)
                {
                    await stockRepository.DeleteAsync(item.ProductId);
                }
            }
        }
    }
}
