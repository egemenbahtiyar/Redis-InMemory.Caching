using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _distributedCache;

        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            
            await _distributedCache.SetStringAsync("surname", "Bahtiyar",cacheEntryOptions);
            return View();
        }
        
        public async Task<IActionResult> Show()
        {
            ViewBag.Value = await _distributedCache.GetStringAsync("surname");
            return View();
        }
        public async Task<IActionResult> Remove()
        {
            await _distributedCache.RemoveAsync("surname");
            return View();
        }
    }
}