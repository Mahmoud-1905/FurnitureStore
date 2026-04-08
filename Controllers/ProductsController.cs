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
                new Product { Id = 1, Name = "Modern corner sofa, Allora off-white", Price = 500, ImageUrl = "~/images/3.jpg" },
                new Product { Id = 2, Name = "5-piece serving table set - luxurious and professional design", Price = 200, ImageUrl = "~/images/7.jpg" },
                new Product { Id = 3, Name = "A distinctive swing with an elegant circular design", Price = 150, ImageUrl = "~/images/4.jpg" },
                new Product { Id = 4, Name = "Modern and elegant dining table with 8 chairs", Price = 300, ImageUrl = "~/images/5.jpg" }
            
            
            
            
            };

            return View(products);
        }
    }
}
