using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    public class ProductsController : Controller
    {
        private IMemoryCache _memoryCache;
        
        public ProductsController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}