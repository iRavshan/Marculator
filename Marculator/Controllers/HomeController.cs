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
        public async Task<IActionResult> Create(Product product)
        {
            Product eProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Price = product.Price
            };

            Product res = await productRepo.GetByName(eProduct.Name);

            if (res is null)
            {  
                await productRepo.Create(eProduct);

                return RedirectToAction("all");
            }

            return View();
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
            Product product = await productRepo.GetById(Id);
            
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            Product eProduct = await productRepo.GetById(product.Id);

            if(productRepo.GetByName(product.Name) is null)
            {
                eProduct.Name = product.Name;

                eProduct.Price = product.Price;

                await productRepo.Update(eProduct);

                return RedirectToAction("all");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await productRepo.Delete(Id);
            return RedirectToAction("all");
        }
     }
}
