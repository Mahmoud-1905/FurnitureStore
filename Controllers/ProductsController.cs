using FurnitureStore.Models;
using FurnitureStore.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FurnitureStore.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product>()
            {
                new Product { Id = 1, Name = "Product1", Price = 500, ImageUrl = "~/images/3.jpg" },
                new Product { Id = 2, Name = "Product2", Price = 200, ImageUrl = "~/images/7.jpg" }
            };

            return View(products);
        }
    }
}
