using System;
using InMemory.Caching.Models;
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
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            options.SlidingExpiration = TimeSpan.FromSeconds(10);
            options.Priority = CacheItemPriority.High;
            
            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                _memoryCache.Set("callback", $"{key}->{value} => sebep:{reason}");
            });

            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

            Product p = new Product()
            {
                Id = 1,
                Name = "Laptop",
                Price = 5000.27
            };
            _memoryCache.Set<Product>("product", p);

            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.TryGetValue("zaman", out string zamancache);
            _memoryCache.TryGetValue("callback", out string callback);
            _memoryCache.TryGetValue("product", out Product product);
            ViewBag.zaman = zamancache;
            ViewBag.callback = callback;
            ViewBag.product = product;

            return View();
        }
    }
}