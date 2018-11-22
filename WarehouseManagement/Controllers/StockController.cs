using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Models;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    public class StockController : Controller
    {
        public StockService stockService;

        public StockController(StockService stockService)
        {
            this.stockService = stockService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("stock")]
        public async Task<IActionResult> Stock()
        {
            var stock = await stockService.SortStockAsync();
            return View(stock);
        }

        [HttpPost("stock")]
        public async Task<IActionResult> AddItem([FromForm] long id, [FromForm] int count)
        {
            await stockService.ChangeItemCountAsync(id, count);
            return RedirectToAction("stock");
        }
    }
}
