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
    public class ProductController : Controller
    {
        public ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            var products = await productService.ReadAllAsync();
            return View(products);
        }

        [HttpGet("add")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            await productService.CreateAsync(product);
            return RedirectToAction("products");
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> SelectProduct(long id)
        {
            var product = await productService.ReadAsync(id);
            return View(product);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct([FromForm] Product product)
        {
            await productService.UpdateAsync(product);
            return RedirectToAction("products");
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveProduct([FromForm]long id)
        {
            await productService.DeleteAsync(id);
            return RedirectToAction("products");
        }

        [HttpGet("inventory")]
        public async Task<IActionResult> Inventory()
        {
            var products = await productService.ReadAllAsync();
            return View(products);
        }

        [HttpPost("inventory")]
        public async Task<IActionResult> Inventory([FromForm] Product[] products)
        {
            await productService.UpdateAsync(products[0]);
            return RedirectToAction("inventory");
        }
    }
}
