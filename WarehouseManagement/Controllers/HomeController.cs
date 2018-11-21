using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    public class HomeController : Controller
    {
        public ICRUDRepository<Product> productRepository;

        public HomeController(ICRUDRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await productRepository.ReadAll();
            return Ok(products);
        }

        [HttpPost("/add")]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            await productRepository.Create(product);
            return Ok();
        }
    }
}
