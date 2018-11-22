using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Models.ViewModels;
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

        [HttpGet("")]
        public async Task<IActionResult> Stats()
        {
            var stats = await statService.MakeStatistics();
            return View(stats);
        }
    }
}
