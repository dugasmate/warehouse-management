using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    [Route("stock")]
    public class StockController : Controller
    {
        public StockService stockServices;

        public StockController(StockService stockServices)
        {
            this.stockServices = stockServices;
        }

        [HttpGet("")]
        public async Task<IActionResult> Stock()
        {
            var stock = await stockServices.ReadAllAsync();
            return Ok(stock);
        }

        //[HttpPost("add")]
        //public async Task<IActionResult> AddItem([FromBody]Stock item)
        //{
        //    await stockServices.CreateAsync(item);
        //    return Ok();
        //}

        //[HttpGet("delete/{id}")]
        //public async Task<IActionResult> RemoveItem(long id)
        //{
        //    await stockServices.DeleteAsync(id);
        //    return RedirectToAction("");
        //}
    }
}
