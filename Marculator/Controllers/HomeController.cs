using Marculator.Models;
using Marculator.Repositories;
using Marculator.ViewModels;
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (productRepo.GetByName(product.Name) is null)
            {
                productRepo.Create(product);

                return RedirectToAction("all");
            }

            else return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            AllViewModel model = new AllViewModel
            {
                AllProduct = await productRepo.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> All(AllViewModel model)
        {
            AllViewModel NewModel = new AllViewModel();

            if (model.SearchText is null)
            {
                NewModel.AllProduct = await productRepo.GetAll();
            }

            else
            {
                NewModel.AllProduct = await productRepo.GetByShortName(model.SearchText);
                NewModel.SearchText = model.SearchText;
            }

            return View(NewModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        private readonly ILogger<HomeController> _logger;

        private readonly IProductRepository productRepo;

        public HomeController(ILogger<HomeController> logger,
                              IProductRepository productRepo)
        {
            _logger = logger;
            this.productRepo = productRepo;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            return View();
        }

     }
}
