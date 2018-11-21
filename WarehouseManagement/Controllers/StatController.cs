using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    [Route("stats")]
    public class StatController : Controller
    {
        public StatService statService;

        public StatController(StatService statService)
        {
            this.statService = statService;
        }

        [HttpGet("weight")]
        public async Task<IActionResult> Weight()
        {
            double totalWeight = await statService.TotalWeightCounter();
            return Ok(totalWeight);
        }

        [HttpGet("value")]
        public async Task<IActionResult> Value()
        {
            int totalValue = await statService.TotalValueCounter();
            return Ok(totalValue);
        }

        [HttpGet("quantity")]
        public async Task<IActionResult> Quantity()
        {
            var stock = await statService.MostItemsFinder();
            return Ok(stock);
        }

        [HttpGet("heaviest")]
        public async Task<IActionResult> HeaviestItem()
        {
            var stock = await statService.HeaviestItemFinder();
            return Ok(stock);
        }
    }
}
