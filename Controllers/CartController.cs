using FurnitureStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FurnitureStore.Controllers
{
    public class CartController : Controller
    {
        private static List<CartItem> cart = new List<CartItem>();

        public IActionResult Index()
        {
            return View(cart);
        }

        public IActionResult AddToCart(string name, decimal price, string image)
        {
            var item = cart.FirstOrDefault(x => x.ProductName == name);

            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductName = name,
                    Price = price,
                    ImageUrl = image,
                    Quantity = 1
                });
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var item = cart.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                cart.Remove(item);
            }

            return RedirectToAction("Index");
        }
    }
}



