using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        public ICRUDRepository<Product> productRepository;
        public ICRUDRepository<Stock> stockRepository;

        public ProductController(ICRUDRepository<Product> productRepository, ICRUDRepository<Stock> stockRepository)
        {
            this.productRepository = productRepository;
            this.stockRepository = stockRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await productRepository.ReadAllAsync();
            return Ok(products);
        }

        [HttpGet("/stock")]
        public async Task<IActionResult> Stock()
        {
            var products = await stockRepository.ReadAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(long id)
        {
            var product = await productRepository.ReadAsync(id);
            return Ok(product);
        }

        [HttpPost("/add")]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            await productRepository.CreateAsync(product);
            return Ok();
        }

        [HttpPost("/addstock")]
        public async Task<IActionResult> AddStock([FromBody]Stock stock)
        {
            await stockRepository.CreateAsync(stock);
            return Ok();
        }

        [HttpGet("/delete/{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            await productRepository.DeleteAsync(id);
            return RedirectToAction("");
        }

        [HttpPatch("/update")]
        public async Task<IActionResult> Update([FromBody]Product product)
        {
            await productRepository.UpdateAsync(product);
            return RedirectToAction("");
        }
    }
}
