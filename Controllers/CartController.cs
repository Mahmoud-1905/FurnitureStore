using FurnitureStore.Data;
using FurnitureStore.Models;
using FurnitureStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id ?? string.Empty;
        }

        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Account");

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var cartItems = new List<CartSessionItem>();

            if (cart != null && cart.CartItems.Any())
            {
                cartItems = cart.CartItems.Select(ci => new CartSessionItem
                {
                    Id = ci.CartItemId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    ImageUrl = ci.Product.ImageUrl ?? string.Empty,
                    Quantity = ci.Quantity
                }).ToList();
            }

            return View(cartItems);
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            if (quantity < 1) quantity = 1;

            var userId = await GetCurrentUserIdAsync();
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Account");

            // Find the product to ensure it exists and is active
            var product = await _context.Products.FindAsync(productId);
            if (product == null || !product.IsActive)
            {
                return NotFound();
            }

            // Find or create cart
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync(); // Save to get CartId
            }

            // Check if item already exists in cart
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                _context.CartItems.Update(cartItem);
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Account");

            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.CartItemId == id && ci.Cart.UserId == userId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            var userId = await GetCurrentUserIdAsync();
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Account");

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null && cart.CartItems.Any())
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
