using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View("Index", categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost(Category category)
        {
            if (!string.IsNullOrEmpty(category.Name) && _context.Categories.Any(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("", "Category name is allready exists! ");
            }
            if (ModelState.IsValid)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
                return View();
        }

        public IActionResult Update()
        {
            return View();
        }
    }
}
