using BulkyBook.Buisness.Services.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;
        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost(Product product)
        {
            
            if (ModelState.IsValid)
            {
                await _ProductService.CreateProduct(product);
                TempData["success"] = "Product has been created successfully";
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var product = await _ProductService.GetProductByIdAsync(id.Value);
            if (product==null)
            {
                return NotFound();
            }
            return View(product);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task<IActionResult> UpdatePOST(Product product)
        {
            
            if(ModelState.IsValid)
            {
                await _ProductService.UpdateProductAsync(product);
                TempData["success"] = "Product has been updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = await _ProductService.GetProductByIdAsync(id.Value);
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
            await _ProductService.DeleteProductAsync(id);
            TempData["success"] = "Product has been deleted successfully";
            return RedirectToAction("Index");
        }


        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var products = await _ProductService.GetAllProductsAsync(true);
            return Json(new { data = products });
        }

        #endregion
    }
}
