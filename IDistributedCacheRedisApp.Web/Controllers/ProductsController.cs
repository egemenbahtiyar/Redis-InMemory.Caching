using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IDistributedCacheRedisApp.Web.Models;
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

            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(30);

            Product product = new Product { Id = 1, Name = "Kalem", Price = 100 };

            string jsonproduct = JsonSerializer.Serialize(product);

            Byte[] byteproduct = Encoding.UTF8.GetBytes(jsonproduct);

            _distributedCache.Set("product:1", byteproduct);

            //await _distributedCache.SetStringAsync("product:1", jsonproduct, cacheEntryOptions);

            return View();
        }

        public IActionResult Show()
        {
            Byte[] byteProduct = _distributedCache.Get("product:1");

            string jsonproduct = Encoding.UTF8.GetString(byteProduct);

            Product p = JsonSerializer.Deserialize<Product>(jsonproduct);

            ViewBag.product = p;
            return View();
        }
        public async Task<IActionResult> Remove()
        {
            await _distributedCache.RemoveAsync("surname");
            return View();
        }
    }
}