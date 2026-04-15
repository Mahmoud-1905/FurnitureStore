using Microsoft.AspNetCore.Mvc;
using FurnitureStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace FurnitureStore.Controllers
{
    public class AdminController : Controller
    {
        private static List<Product> products = new List<Product>();

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = products.Count + 1;
            products.Add(product);
            return RedirectToAction("Products");
        }

        public IActionResult Delete(int id)
        {
            var item = products.FirstOrDefault(x => x.Id == id);
            if (item != null)
                products.Remove(item);

            return RedirectToAction("Products");
        }
    }

}


