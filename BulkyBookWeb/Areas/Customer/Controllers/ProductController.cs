using BulkyBook.Buisness.Services.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService ProductService, ICategoryService CategoryService)
        {
            _productService = ProductService;
            _categoryService = CategoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> Upsert()
        {
            IEnumerable<SelectListItem> categoryList = (await _categoryService.GetAllCategoriesAsync()).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            ViewData["categoryList"] = categoryList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> UpsertPost(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(product);
                TempData["success"] = "Product has been created successfully";
                return RedirectToAction("Index");
            }
            else
                return View();
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int id)
        {
            await _productService.DeleteProductAsync(id);
            TempData["success"] = "Product has been deleted successfully";
            return RedirectToAction("Index");
        }


        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync(true);
            return Json(new { data = products });
        }

        #endregion
    }
}
