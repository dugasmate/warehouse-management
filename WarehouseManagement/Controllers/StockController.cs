﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Models;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    [Route("stock")]
    public class StockController : Controller
    {
        public StockService stockService;

        public StockController(StockService stockService)
        {
            this.stockService = stockService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Stock()
        {
            var stock = await stockService.SortStockAsync();
            return Ok(stock);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItem([FromBody]Stock item)
        {
            await stockService.AddItemAsync(item);
            return Ok();
        }

        //[HttpGet("delete/{id}")]
        //public async Task<IActionResult> RemoveItem(long id)
        //{
        //    await stockService.DeleteAsync(id);
        //    return RedirectToAction("");
        //}
    }
}
