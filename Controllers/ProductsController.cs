using FurnitureStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using FurnitureStore.Data;


namespace FurnitureStore.Controllers
{

    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = GetProducts();
            return View(products);
        }

        private List<Product> GetProducts()
        {
            using var context = new AppDbContext(new DbContextOptions<AppDbContext>());
            {
                context.Products.ToList();
            }
            return new List<Product>
            {
                
            };
        }

       

    }
}