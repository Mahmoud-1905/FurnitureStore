using FurnitureStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FurnitureStore.Data;


namespace FurnitureStore.Controllers
{

    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm, int? categoryId)
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm) || p.Category.Name.Contains(searchTerm));
            }

            var products = await productsQuery.ToListAsync();
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Categories = await _context.Categories.Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToListAsync();
            ViewBag.SelectedCategoryId = categoryId;
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            // Fetch related products from the same category (excluding the current product)
            var relatedProducts = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.CategoryId == product.CategoryId && p.ProductId != id)
                .Take(4)  
                .ToListAsync();

            // If not enough related products in the same category, fill with other active products
            if (relatedProducts.Count < 4)
            {
                var existingIds = relatedProducts.Select(p => p.ProductId).ToList();
                existingIds.Add(id);
                var moreProducts = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsActive && !existingIds.Contains(p.ProductId))
                    .Take(4 - relatedProducts.Count)
                    .ToListAsync();
                relatedProducts.AddRange(moreProducts);
            }

            ViewBag.RelatedProducts = relatedProducts;
            return View(product);
        }

    }
}