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
            _memoryCache.Set("gun", 14);
            return View();
        }
        public IActionResult Show()
        {
            ViewBag.value = _memoryCache.Get("gun");
            return View();
        }
    }
}