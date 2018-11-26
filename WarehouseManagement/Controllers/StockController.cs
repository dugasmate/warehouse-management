using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Models.ViewModels;
using WarehouseManagement.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarehouseManagement.Controllers
{
    public class StockController : Controller
    {
        StockService stockService;
        public StockController(StockService stockService)
        {
            this.stockService = stockService;
        }

        [HttpGet("inventory")]
        public async Task<IActionResult> Inventory()
        {
            var products = await stockService.ReadAllAsync();
            return View(products);
        }

        [HttpPost("inventory")]
        public async Task<IActionResult> Inventory([FromForm] List<StockDTO> stocks)
        {
            await stockService.UpdateStock(stocks);
            return RedirectToAction("inventory");
        }
    }
}
