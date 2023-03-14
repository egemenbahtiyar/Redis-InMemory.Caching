using System;
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
        public IActionResult Index()
        {
            _memoryCache.Set("zaman", DateTime.Now.ToString());
            //1.yol
            if (String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            {
                _memoryCache.Set("zaman", DateTime.Now.ToString());
            }
            //2.yol

            if (!_memoryCache.TryGetValue("zaman", out string zamancache))
            {
                _memoryCache.Set("zaman", DateTime.Now.ToString());
            }

            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.GetOrCreate("zaman", entry =>
            {
                return DateTime.Now.ToString();
            });

            ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}