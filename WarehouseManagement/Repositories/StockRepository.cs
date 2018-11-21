using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Repositories
{
    public class StockRepository : ICRUDRepository<Stock>
    {
        private StockContext stockContext;

        public StockRepository(StockContext stockContext)
        {
            this.stockContext = stockContext;
        }

        public async Task CreateAsync(Stock stock)
        {
            await stockContext.Stocks.AddAsync(stock);
            await stockContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Stock>> ReadAllAsync()
        {
            var stocks = stockContext.Stocks.Include(a => a.Product).ToList();
            return stocks;
        }

        public Task<Stock> ReadAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Stock t)
        {
            throw new NotImplementedException();
        }
    }
}
