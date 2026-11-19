using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Services;
using System.Text.Json;

namespace Ecommerce.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IStoreService _storeService;
        public CartController(IStoreService storeService) => _storeService = storeService;

        public IActionResult Index()
        {
            var cart = GetCartFromSession();
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _storeService.GetProductById(id);
            if (product != null)
            {
                var cart = GetCartFromSession();
                var item = cart.FirstOrDefault(c => c.ProductId == id);
                if (item != null) item.Quantity++;
                else cart.Add(new CartItemDto { ProductId = product.Id, ProductName = product.Name, Price = product.Price, Quantity = 1 });

                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            }
            return RedirectToAction("Index");
        }

        //db حفظ الطلب في 
        [HttpPost]
        public IActionResult Checkout(string customerName, string address, string phone)
        {
            var cart = GetCartFromSession();
            if (cart.Any())
            {
                var order = new Order
                {
                    CustomerName = customerName,
                    Address = address,
                    Phone = phone,
                    TotalAmount = cart.Sum(c => c.Price * c.Quantity)
                };
                _storeService.CreateOrder(order);
                HttpContext.Session.Remove("Cart"); // تفريغ السلة بعد الشراء
            }
            return View("OrderSuccess");
        }

        private List<CartItemDto> GetCartFromSession()
        {
            var sessionData = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(sessionData) ? new List<CartItemDto>() : JsonSerializer.Deserialize<List<CartItemDto>>(sessionData)!;
            //JsonSerializer:تفكيك قائمة المنتجات الى نص جيسون بسيط لحفظه بداخلها  ,,,,Deserialize:لإعادة تركيب هذا النص وتحويله إلى قائمة منتجات حقيقية
        }
    }
}
