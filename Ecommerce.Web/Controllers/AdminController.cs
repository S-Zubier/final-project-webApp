using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ecommerce.Application.Services;

namespace Ecommerce.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IStoreService storeService, IWebHostEnvironment webHostEnvironment)
        {
            _storeService = storeService;
            _webHostEnvironment = webHostEnvironment; 
        }
        public IActionResult CreateCategory() => View();

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _storeService.AddCategory(category);
                return RedirectToAction("CreateProduct");
            }
            return View(category);
        }
        public IActionResult CreateProduct()
        {
            ViewBag.Categories = new SelectList(_storeService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _storeService.AddProduct(product);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = new SelectList(_storeService.GetCategories(), "Id", "Name");
            return View(product);
        }
        public IActionResult EditProduct(int id)
        {
            var product = _storeService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_storeService.GetCategories(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                _storeService.UpdateProduct(product);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Categories = new SelectList(_storeService.GetCategories(), "Id", "Name", product.CategoryId);
            return View(product);
        }


       [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var product = _storeService.GetProductById(id);
            if (product != null)
            {
                // حذف ملف الصورة من الجهاز إذا كانت صورة محلية
                if (!string.IsNullOrEmpty(product.ImageUrl) && product.ImageUrl.StartsWith("/images/"))
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _storeService.DeleteProduct(id);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
    
