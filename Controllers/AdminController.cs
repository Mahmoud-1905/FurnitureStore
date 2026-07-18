using Microsoft.AspNetCore.Mvc;
using FurnitureStore.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Data;
using Microsoft.AspNetCore.Authorization;
using FurnitureStore.ViewModels;
using System.Security.Claims;

namespace FurnitureStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

        // --- DASHBOARD OVERVIEW ---
        public async Task<IActionResult> Dashboard()
        {
            var totalOrders = await _context.Orders.CountAsync();
            var lowStockCount = await _context.Products.CountAsync(p => p.StockQuantity < 10 && p.IsActive);
            
            // Mocking analytics since it requires JS tracking
            var vm = new DashboardOverviewViewModel
            {
                TotalVisitors = 14500,
                TotalPageViews = 45000,
                BounceRate = 35.5,
                TotalOrders = totalOrders,
                LowStockProductsCount = lowStockCount,
                TotalRevenue = await _context.Orders.Where(o => o.PaymentStatus == PaymentStatus.Succeeded).SumAsync(o => o.TotalAmount),
                
                // Mock chart data (last 7 days revenue)
                ChartLabels = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" },
                ChartData = new List<decimal> { 1200m, 1900m, 3000m, 500m, 2000m, 3000m, 4500m }
            };

            return View(vm);
        }

        // --- ORDERS PANEL ---
        public async Task<IActionResult> Orders(OrderStatus? status)
        {
            var query = _context.Orders.Include(o => o.User).Include(o => o.Address).AsQueryable();
            
            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            var orders = await query.OrderByDescending(o => o.PlacedAt).ToListAsync();
            if (!orders.Any() && status.HasValue)
            {
                // fallback to all orders if filter returns none
                orders = await _context.Orders.Include(o => o.User).Include(o => o.Address)
                    .OrderByDescending(o => o.PlacedAt).ToListAsync();
            }
            var vm = new AdminOrdersViewModel
            {
                Orders = orders,
                FilterStatus = status
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                order.UpdatedAt = DateTime.UtcNow;
                
                // Audit Log
                _context.AuditLogs.Add(new AuditLog 
                { 
                    ActorUserId = GetUserId(),
                    ActionType = "Update Order Status", 
                    TargetEntityType = "Order",
                    TargetEntityId = orderId.ToString(),
                    Description = $"Order {orderId} status changed to {newStatus}", 
                    CreatedAt = DateTime.UtcNow 
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Orders));
        }

        // --- PAYMENT QUEUE ---
        public async Task<IActionResult> Payments()
        {
            var unpaidOrders = await _context.Orders
                .Include(o => o.User)
                .Where(o => o.PaymentStatus == PaymentStatus.Pending)
                .OrderBy(o => o.PlacedAt)
                .ToListAsync();

            var vm = new AdminPaymentsViewModel { UnpaidOrders = unpaidOrders };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsPaid(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.PaymentStatus = PaymentStatus.Succeeded;
                order.UpdatedAt = DateTime.UtcNow;

                _context.AuditLogs.Add(new AuditLog 
                { 
                    ActorUserId = GetUserId(),
                    ActionType = "Payment Processed", 
                    TargetEntityType = "Order",
                    TargetEntityId = orderId.ToString(),
                    Description = $"Order {orderId} marked as Succeeded", 
                    CreatedAt = DateTime.UtcNow 
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Payments));
        }

        // --- PRODUCT MANAGEMENT ---
        public async Task<IActionResult> Products()
        {
            return View(await _context.Products.Include(p => p.Category).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync(); // save first to get ID
                
                _context.AuditLogs.Add(new AuditLog 
                { 
                    ActorUserId = GetUserId(),
                    ActionType = "Create Product", 
                    TargetEntityType = "Product",
                    TargetEntityId = product.ProductId.ToString(),
                    Description = $"Product {product.Name} created.", 
                    CreatedAt = DateTime.UtcNow 
                });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Products));
            }
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToList();
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    
                    _context.AuditLogs.Add(new AuditLog 
                    { 
                        ActorUserId = GetUserId(),
                        ActionType = "Edit Product", 
                        TargetEntityType = "Product",
                        TargetEntityId = product.ProductId.ToString(),
                        Description = $"Product {product.Name} updated.", 
                        CreatedAt = DateTime.UtcNow 
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.ProductId == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Products));
            }
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleProductStatus(int id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;

                _context.AuditLogs.Add(new AuditLog 
                { 
                    ActorUserId = GetUserId(),
                    ActionType = "Toggle Product Status", 
                    TargetEntityType = "Product",
                    TargetEntityId = id.ToString(),
                    Description = $"Product {item.ProductId} active status set to {item.IsActive}", 
                    CreatedAt = DateTime.UtcNow 
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Products));
        }

        // --- INVENTORY ALERTS ---
        public async Task<IActionResult> Inventory()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.StockQuantity)
                .ToListAsync();

            var vm = new AdminInventoryViewModel { Products = products };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStock(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.StockQuantity = quantity;

                _context.AuditLogs.Add(new AuditLog 
                { 
                    ActorUserId = GetUserId(),
                    ActionType = "Update Stock", 
                    TargetEntityType = "Product",
                    TargetEntityId = productId.ToString(),
                    Description = $"Product {productId} stock updated to {quantity}", 
                    CreatedAt = DateTime.UtcNow 
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Inventory));
        }

        // --- CUSTOMER MANAGEMENT ---
        public async Task<IActionResult> Customers()
        {
            var customers = await _context.Users.ToListAsync();
            var vm = new AdminCustomersViewModel { Customers = customers };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleCustomerStatus(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsActive = !user.IsActive;

                _context.AuditLogs.Add(new AuditLog 
                { 
                    ActorUserId = GetUserId(),
                    ActionType = "Toggle Customer Status", 
                    TargetEntityType = "User",
                    TargetEntityId = userId,
                    Description = $"Customer {user.Email} active status set to {user.IsActive}", 
                    CreatedAt = DateTime.UtcNow 
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Customers));
        }

        // --- REVIEWS MODERATION ---
        public async Task<IActionResult> Reviews()
        {
            var reviews = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            var vm = new AdminReviewsViewModel { Reviews = reviews };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleReviewVisibility(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                review.IsVisible = !review.IsVisible;

                _context.AuditLogs.Add(new AuditLog 
                { 
                    ActorUserId = GetUserId(),
                    ActionType = "Toggle Review Visibility", 
                    TargetEntityType = "Review",
                    TargetEntityId = id.ToString(),
                    Description = $"Review {id} visibility set to {review.IsVisible}", 
                    CreatedAt = DateTime.UtcNow 
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Reviews));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);

                _context.AuditLogs.Add(new AuditLog 
                { 
                    ActorUserId = GetUserId(),
                    ActionType = "Delete Review", 
                    TargetEntityType = "Review",
                    TargetEntityId = id.ToString(),
                    Description = $"Review {id} deleted", 
                    CreatedAt = DateTime.UtcNow 
                });

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Reviews));
        }

        // --- AUDIT LOG ---
        public async Task<IActionResult> AuditLogs()
        {
            var logs = await _context.AuditLogs
                .Include(a => a.ActorUser)
                .OrderByDescending(a => a.CreatedAt)
                .Take(100) // limit to recent 100 for performance
                .ToListAsync();

            return View(logs);
        }
    }
}
