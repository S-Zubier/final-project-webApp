using Ecommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Services;
using System.Diagnostics;

namespace Ecommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreService _storeService;
        public HomeController(IStoreService storeService) => _storeService = storeService;

        public IActionResult Index()
        {
            var products = _storeService.GetProducts();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
