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

        public ProductController(ICRUDRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> ListProducts()
        {
            var products = await productRepository.ReadAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SelectProduct(long id)
        {
            var product = await productRepository.ReadAsync(id);
            return Ok(product);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await productRepository.CreateAsync(product);
            return Ok();
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateProduct([FromBody]Product product)
        {
            await productRepository.UpdateAsync(product);
            return RedirectToAction("");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> RemoveProduct(long id)
        {
            await productRepository.DeleteAsync(id);
            return RedirectToAction("");
        }
    }
}
