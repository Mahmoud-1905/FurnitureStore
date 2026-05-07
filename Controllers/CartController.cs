using FurnitureStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace FurnitureStore.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "CartSession";

        private List<CartSessionItem> GetCart()
        {
            var sessionCart = HttpContext.Session.GetString(CartSessionKey);
            return sessionCart == null ? new List<CartSessionItem>() : JsonSerializer.Deserialize<List<CartSessionItem>>(sessionCart)!;
        }

        private void SaveCart(List<CartSessionItem> cart)
        {
            HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }

        public IActionResult Index()
        {
            return View(GetCart());
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult AddToCart(string name, decimal price, string image, int quantity = 1)
        {
            if (quantity < 1) quantity = 1;

            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductName == name);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                int newId = cart.Any() ? cart.Max(c => c.Id) + 1 : 1;
                cart.Add(new CartSessionItem
                {
                    Id = newId,
                    ProductName = name,
                    Price = price,
                    ImageUrl = image,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction("Index");
        }
    }
}
