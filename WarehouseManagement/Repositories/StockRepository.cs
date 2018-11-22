using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Context;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Repositories
{
    public class StockRepository : ICRUDRepository<Stock>
    {
        private WarehouseContext stockContext;

        public StockRepository(WarehouseContext stockContext)
        {
            this.stockContext = stockContext;
        }

        public async Task CreateAsync(Stock item)
        {
            await stockContext.Stocks.AddAsync(item);
            await stockContext.SaveChangesAsync();
        }

        public async Task<Stock> ReadAsync(long id)
        {
            var selectedItem = await stockContext.Stocks.Include(a => a.Product).FirstOrDefaultAsync(x => x.StockId == id);
            return selectedItem;
        }

        public async Task<List<Stock>> ReadAllAsync()
        {
            var stock = await stockContext.Stocks.Include(a => a.Product).ToListAsync();
            return stock;
        }

        public async Task UpdateAsync(Stock item)
        {
            stockContext.Stocks.Update(item);
            await stockContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var selectedItem = await stockContext.Stocks.Include(a => a.Product).FirstOrDefaultAsync(x => x.ProductId == id);
            stockContext.Stocks.Remove(selectedItem);
            await stockContext.SaveChangesAsync();
        }
    }
}
