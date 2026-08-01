// Controllers/CheckoutController.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FurnitureStore.Models;
using FurnitureStore.Data; // Assuming AppDbContext is in Data namespace
using FurnitureStore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 1. Show cart with coupon box
        public async Task<IActionResult> CartReview()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || string.IsNullOrEmpty(user.Id))
                return RedirectToAction("Login", "Account");

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            var cartItems = cart?.CartItems.ToList() ?? new List<CartItem>();
            var subTotal = cartItems.Sum(ci => (ci.Product?.Price ?? 0m) * ci.Quantity);
            var model = new CartReviewViewModel
            {
                CartItems = cartItems,
                SubTotal = subTotal,
                CouponCode = string.Empty,
                DiscountAmount = 0,
                Total = subTotal
            };
            return View(model);
        }

        // 2. Apply coupon
        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartReviewViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || string.IsNullOrEmpty(user.Id))
                return RedirectToAction("Login", "Account");

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            var cartItems = cart?.CartItems.ToList() ?? new List<CartItem>();
            var subTotal = cartItems.Sum(ci => (ci.Product?.Price ?? 0m) * ci.Quantity);
            var coupon = _context.Coupons
                .FirstOrDefault(c => c.Code == model.CouponCode && c.IsActive && c.StartDate <= DateTime.UtcNow && c.EndDate >= DateTime.UtcNow);

            decimal discount = 0;
            int? couponId = null;
            if (coupon != null)
            {
                couponId = coupon.CouponId;
                if (coupon.IsPercentage)
                {
                    discount = subTotal * (coupon.DiscountValue / 100m);
                    if (coupon.MaxDiscountAmount.HasValue && discount > coupon.MaxDiscountAmount.Value)
                        discount = coupon.MaxDiscountAmount.Value;
                }
                else
                {
                    discount = coupon.DiscountValue;
                }
                // Minimum order amount check
                if (coupon.MinimumOrderAmount.HasValue && subTotal < coupon.MinimumOrderAmount.Value)
                    discount = 0;
            }

            // Store coupon id for later order creation
            if (couponId.HasValue)
                TempData["CouponId"] = couponId.Value;
            else
                TempData.Remove("CouponId");

            var viewModel = new CartReviewViewModel
            {
                CartItems = cartItems,
                SubTotal = subTotal,
                CouponCode = model.CouponCode,
                DiscountAmount = discount,
                Total = subTotal - discount
            };
            // Preserve discount for later steps
            TempData["DiscountAmount"] = discount;
            return View("CartReview", viewModel);
        }

        // 3. Enter address (GET)
        public async Task<IActionResult> EnterAddress()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || string.IsNullOrEmpty(user.Id))
                return RedirectToAction("Login", "Account");

            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == user.Id && a.IsDefault) ?? new Address();
            var vm = new AddressViewModel
            {
                AddressId = address.AddressId,
                AddressName = address.AddressName,
                StreetAddress = address.StreetAddress,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country,
                IsDefault = address.IsDefault
            };
            return View(vm);
        }

        // 4. Save address (POST)
        [HttpPost]
        public async Task<IActionResult> EnterAddress(AddressViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || string.IsNullOrEmpty(user.Id))
                return RedirectToAction("Login", "Account");

            Address address;
            if (model.AddressId == 0)
            {
                address = new Address
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Addresses.Add(address);
            }
            else
            {
                address = _context.Addresses.FirstOrDefault(a => a.AddressId == model.AddressId && a.UserId == user.Id)
                          ?? throw new InvalidOperationException("Address not found or does not belong to user.");
            }
            // Populate fields
            address.AddressName = model.AddressName;
            address.StreetAddress = model.StreetAddress;
            address.City = model.City;
            address.State = model.State;
            address.PostalCode = model.PostalCode;
            address.Country = model.Country;
            address.IsDefault = model.IsDefault;

            await _context.SaveChangesAsync();
            TempData["AddressId"] = address.AddressId;
            return RedirectToAction("ConfirmOrder");
        }

        // 5. Confirm order (summary before payment)
        public async Task<IActionResult> ConfirmOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || string.IsNullOrEmpty(user.Id))
                return RedirectToAction("Login", "Account");

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            var cartItems = cart?.CartItems.ToList() ?? new List<CartItem>();
            var subTotal = cartItems.Sum(ci => (ci.Product?.Price ?? 0m) * ci.Quantity);

            decimal discount = 0m;
            if (TempData.TryGetValue("DiscountAmount", out var discObj))
            {
                if (discObj is decimal dd) discount = dd;
                else if (!string.IsNullOrEmpty(discObj?.ToString()) && decimal.TryParse(discObj.ToString(), out var pd)) discount = pd;
            }

            var total = subTotal - discount;

            int? addressId = null;
            if (TempData.TryGetValue("AddressId", out var addrObj))
            {
                if (addrObj is int ai) addressId = ai;
                else if (!string.IsNullOrEmpty(addrObj?.ToString()) && int.TryParse(addrObj.ToString(), out var pai)) addressId = pai;
            }

            var address = addressId.HasValue ? await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId.Value) : null;

            int? couponId = null;
            if (TempData.TryGetValue("CouponId", out var cObj))
            {
                if (cObj is int ciVal) couponId = ciVal;
                else if (!string.IsNullOrEmpty(cObj?.ToString()) && int.TryParse(cObj.ToString(), out var pci)) couponId = pci;
            }
            var vm = new OrderConfirmationViewModel
            {
                CartItems = cartItems,
                SubTotal = subTotal,
                DiscountAmount = discount,
                Total = total,
                Address = address,
                CouponId = couponId
            };
            // Preserve TempData for next request
            TempData.Keep("AddressId");
            TempData.Keep("CouponId");
            TempData["DiscountAmount"] = discount;
            return View(vm);
        }

        // 6. Process payment (stub) and create order
        [HttpPost]
        public async Task<IActionResult> ProcessPayment()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || string.IsNullOrEmpty(user.Id))
                return RedirectToAction("Login", "Account");

            //if (!TempData.TryGetValue("AddressId", out var addrObj))
            //    return RedirectToAction("EnterAddress");

            //int addressId;
            //if (addrObj is int ai) addressId = ai;
            //else if (!int.TryParse(addrObj?.ToString(), out addressId))
            //    return RedirectToAction("EnterAddress");

            //var address = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
int? couponId = null;
            if (TempData.TryGetValue("CouponId", out var cObj))
            {
                if (cObj is int ciVal) couponId = ciVal;
                else if (!string.IsNullOrEmpty(cObj?.ToString()) && int.TryParse(cObj.ToString(), out var pci)) couponId = pci;
            }

            decimal discount = 0m;
            if (TempData.TryGetValue("DiscountAmount", out var discObj))
            {
                if (discObj is decimal dd) discount = dd;
                else if (!string.IsNullOrEmpty(discObj?.ToString()) && decimal.TryParse(discObj.ToString(), out var pd)) discount = pd;
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            var cartItems = cart?.CartItems.ToList() ?? new List<CartItem>();
            var subTotal = cartItems.Sum(ci => (ci.Product?.Price ?? 0m) * ci.Quantity);
            var total = subTotal - discount;

            // Create Order
            var order = new Order
                {
                    UserId = user.Id,
                    Status = OrderStatus.Pending,
                    TotalAmount = total,
                    DiscountAmount = discount,
                    CouponId = couponId,
                    // Store a simple delivery address string for quick reference
                    DeliveryAddress = "Gaza",
                    PaymentStatus = PaymentStatus.Pending,
                    PlacedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // to generate OrderId

            // Transfer CartItems to OrderItems
            foreach (var ci in cartItems)
            {
                var oi = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price
                };
                _context.OrderItems.Add(oi);
            }
            //// Clear cart
            //_context.CartItems.RemoveRange(cartItems);
            //await _context.SaveChangesAsync();

            // Stub payment record
            var payment = new Payment
            {
                OrderId = order.OrderId,
                Amount = total,
                PaymentProvider = "StripeMock",
                ProviderReferenceId = Guid.NewGuid().ToString(),
                Status = "Succeeded",
                CreatedAt = DateTime.UtcNow
            };
            _context.Payments.Add(payment);
            // Update order payment status
            order.PaymentStatus = PaymentStatus.Succeeded;
            order.Payment = payment;
            await _context.SaveChangesAsync();

            return Ok(new { redirectUrl = Url.Action("Result", "Checkout", new { orderId = order.OrderId }) });
            //return RedirectToAction(nameof(Result), new { orderId = order.OrderId });
        }

        // 7. Show result page
        public IActionResult Result(int orderId)
        {
            var order =  _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.OrderId == orderId);
            if (order == null) return NotFound();
            return View(order);
        }
    }
}
