using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Interfaces;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    public class ProductController : Controller
    {
        public ICRUDRepository<Product> productRepository;

        public ProductController(ICRUDRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            var products = await productRepository.ReadAllAsync();
            return View(products);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            await productRepository.CreateAsync(product);
            return RedirectToAction("products");
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> SelectProduct(long id)
        {
            var product = await productRepository.ReadAsync(id);
            return View(product);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct([FromForm] Product product)
        {
            await productRepository.UpdateAsync(product);
            return RedirectToAction("products");
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveProduct([FromForm]long id)
        {
            await productRepository.DeleteAsync(id);
            return RedirectToAction("products");
        }
    }
}
