using Microsoft.AspNetCore.Mvc;
using FurnitureStore.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Data;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult Dashboard()
        {
            return View();
        }

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
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Products));
            }
            ViewBag.Categories = _context.Categories.Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item != null)
            {
                _context.Products.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Products));
        }
    }

}
