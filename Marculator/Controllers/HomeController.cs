using Marculator.Models;
using Marculator.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Marculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository productRepo;

        public HomeController(ILogger<HomeController> logger,
                              IProductRepository productRepo)
        {
            _logger = logger;
            this.productRepo = productRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            productRepo.Create(product);

            return RedirectToAction("getall");
        }
     }
}
