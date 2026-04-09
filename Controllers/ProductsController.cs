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
                    new Product { Id = 1, Name = "Modern Minimalist Console Table", Price = 500, ImageUrl = "~/images/1.jpg" },
                new Product { Id = 2, Name = "Luxury Modern Kitchen Island", Price = 200, ImageUrl = "~/images/2.jpeg" },
                new Product { Id = 3, Name = "Contemporary White Sectional Sofa", Price = 150, ImageUrl = "~/images/3.jpg" },
                new Product { Id = 4, Name = "Wicker Hanging Egg Chair", Price = 300, ImageUrl = "~/images/4.jpg" },
                new Product { Id = 5, Name = "Modern Round Marble Dining Set", Price = 400, ImageUrl = "~/images/5.jpg" },
                new Product { Id = 6, Name = "Built-in Coffee Bar Cabinet", Price = 120, ImageUrl = "~/images/6.jpg" },
                new Product { Id = 7, Name = "Round Wooden Nesting Table Set", Price = 220, ImageUrl = "~/images/7.jpg" },
                new Product { Id = 8, Name = "Curved Emerald Green Velvet Loveseat", Price = 180, ImageUrl = "~/images/8.jpg" },
                new Product { Id = 9, Name = "Leaf-Shaped Designer Accent Chair", Price = 140, ImageUrl = "~/images/9.jpg" },
                new Product { Id = 10, Name = "Abstract Ceramic Figurines & Vases", Price = 260, ImageUrl = "~/images/10.jpg" },
                new Product { Id = 11, Name = "Modern Vanity with Backlit Mirror", Price = 400, ImageUrl = "~/images/11.jpg" },
                new Product { Id = 12, Name = "Minimalist Wooden Display Cabinet", Price = 90, ImageUrl = "~/images/12.jpg" },
                new Product { Id = 13, Name = "Compact Home Office Workstation", Price = 500, ImageUrl = "~/images/13.jpg" },
                new Product { Id = 14, Name = "Mid-Century Modern Dressing Table", Price = 210, ImageUrl = "~/images/14.jpg" },
                new Product { Id = 15, Name = "Large White Sectional Sofa & Organic Coffee Table", Price = 130, ImageUrl = "~/images/15.jpg" },
                new Product { Id = 16, Name = "Irregular Full-Length Light-Up Mirror", Price = 170, ImageUrl = "~/images/16.jpg" },
                new Product { Id = 17, Name = "Contemporary Beige & Brown Sectional", Price = 280, ImageUrl = "~/images/17.jpg" },
                new Product { Id = 18, Name = "Minimalist Entryway Bench & Mirror", Price = 350, ImageUrl = "~/images/18.jpg" },
                new Product { Id = 19, Name = "Circular Striped Vanity Set", Price = 320, ImageUrl = "~/images/19.jpeg" },
                new Product { Id = 20, Name = "Plush \"Bubble\" Armchair", Price = 75, ImageUrl = "~/images/20.jpg" },
                new Product { Id = 21, Name = "Emerald Green Velvet Sofa", Price = 200, ImageUrl = "~/images/21.jpg" },
                new Product { Id = 22, Name = "Modern Upholstered Dining Chairs", Price = 60, ImageUrl = "~/images/22.jpeg" }



            };

            return View(products);
        }
    }
}
