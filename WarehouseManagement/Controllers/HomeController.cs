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
            var products = await productRepository.ReadAllAsync();
            return Ok(products);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> Read(int id)
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

        [HttpGet("/delete/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await productRepository.DeleteAsync(id);
            return RedirectToAction("");
        }

        [HttpPut("/update")]
        public async Task<IActionResult> Update([FromBody]Product product)
        {
            await productRepository.UpdateAsync(product);
            return RedirectToAction("");
        }
    }
}
