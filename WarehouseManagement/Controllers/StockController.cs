using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    [Route("stock")]
    public class StockController : Controller
    {
        public ICRUDRepository<Stock> stockRepository;

        public StockController(ICRUDRepository<Stock> stockRepository)
        {
            this.stockRepository = stockRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Stock()
        {
            var products = await stockRepository.ReadAllAsync();
            return Ok(products);
        }


        [HttpPost("/addstock")]
        public async Task<IActionResult> AddStock([FromBody]Stock stock)
        {
            await stockRepository.CreateAsync(stock);
            return Ok();
        }
    }
}
