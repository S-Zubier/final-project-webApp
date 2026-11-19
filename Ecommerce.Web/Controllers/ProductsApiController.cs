using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Services;

namespace Ecommerce.Web.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public ProductsApiController(IStoreService storeService) => _storeService = storeService;

        [HttpGet]
        public IActionResult GetAll() => Ok(_storeService.GetProductsApi());
    }
}
