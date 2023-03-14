﻿using System;
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
            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.TryGetValue("zaman", out string zamancache);

            ViewBag.zaman = zamancache;

            return View();
        }
    }
}