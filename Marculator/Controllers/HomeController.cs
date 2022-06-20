using iTextSharp.text;
using iTextSharp.text.pdf;
using Marculator.Models;
using Marculator.Repositories;
using Marculator.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            Product product = await productRepo.GetById(Id);

            DetailsViewModel model = new DetailsViewModel
            {
                Product = product
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Details(DetailsViewModel model)
        {
            Product eProduct = await productRepo.GetById(model.Product.Id);

            if (eProduct.Name == model.Product.Name)
            {
                eProduct.Price = model.Product.Price;
                await productRepo.Update(eProduct);
                return RedirectToAction("all");
            }

            else
            {
                if ((await productRepo.GetByName(model.Product.Name)) is null)
                {
                    eProduct.Name = model.Product.Name;
                    eProduct.Price = model.Product.Price;
                    await productRepo.Update(eProduct);
                    return RedirectToAction("all");
                }

                return View();
            }
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            await productRepo.Delete(Id);
            return RedirectToAction("all");
        }

        private readonly ILogger<HomeController> _logger;

        private readonly IProductRepository productRepo;
        private readonly IWebHostEnvironment webHost;

        public HomeController(ILogger<HomeController> logger,
                              IProductRepository productRepo,
                              IWebHostEnvironment webHost)
        {
            _logger = logger;
            this.productRepo = productRepo;
            this.webHost = webHost;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddViewModel model = new AddViewModel
            {
                Thing = new List<Thing>()
            };

            IEnumerable<Product> allProducts = await productRepo.GetAll();

            foreach(Product item in allProducts)
            {
                model.Thing.Add(new Thing { Name = item.Name, Count = 0 });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<FileResult> Add(AddViewModel model)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 60, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                PdfPTable table = new PdfPTable(5);
                doc.Open();

                var logo = Image.GetInstance("wwwroot/img/Moz-Logo.png");
                logo.Alignment = Element.ALIGN_CENTER;
                doc.Add(logo);

                PdfPCell trCell = new PdfPCell(new Phrase("#", new Font(Font.FontFamily.HELVETICA, 10)));
                trCell.MinimumHeight = 20;
                trCell.BorderWidthLeft = 0f;
                trCell.BorderWidthRight = 0;
                trCell.BorderWidthTop = 0;
                trCell.BorderWidthBottom = 1f;
                trCell.HorizontalAlignment = Element.ALIGN_CENTER;
                trCell.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(trCell);

                PdfPCell nameCell = new PdfPCell(new Phrase("NOMI", new Font(Font.FontFamily.HELVETICA, 10)));
                nameCell.MinimumHeight = 20;
                nameCell.BorderWidthLeft = 0;
                nameCell.BorderWidthRight = 0;
                nameCell.BorderWidthTop = 0;
                nameCell.BorderWidthBottom = 1f;
                nameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                nameCell.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(nameCell);

                PdfPCell priceCell = new PdfPCell(new Phrase("NARXI", new Font(Font.FontFamily.HELVETICA, 10)));
                priceCell.MinimumHeight = 20;
                priceCell.BorderWidthLeft = 0f;
                priceCell.BorderWidthRight = 0;
                priceCell.BorderWidthTop = 0;
                priceCell.BorderWidthBottom = 1f;
                priceCell.HorizontalAlignment = Element.ALIGN_CENTER;
                priceCell.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(priceCell);

                PdfPCell countCell = new PdfPCell(new Phrase("SONI", new Font(Font.FontFamily.HELVETICA, 10)));
                countCell.MinimumHeight = 20;
                countCell.BorderWidthLeft = 0f;
                countCell.BorderWidthRight = 0;
                countCell.BorderWidthTop = 0;
                countCell.BorderWidthBottom = 1f;
                countCell.HorizontalAlignment = Element.ALIGN_LEFT;
                countCell.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(countCell);

                PdfPCell umumCell = new PdfPCell(new Phrase("QIYMATI", new Font(Font.FontFamily.HELVETICA, 10)));
                umumCell.MinimumHeight = 25;
                umumCell.BorderWidthLeft = 0f;
                umumCell.BorderWidthRight = 0;
                umumCell.BorderWidthTop = 0;
                umumCell.BorderWidthBottom = 1f;
                umumCell.HorizontalAlignment = Element.ALIGN_LEFT;
                umumCell.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(umumCell);

                int count = 1;
                int sum = 0;

                for (int i = 0; i < model.Thing.Count; i++)
                {
                    if(model.Thing[i].Count != 0)
                    {
                        Product product = await productRepo.GetByName(model.Thing[i].Name);

                        PdfPCell _trCell = new PdfPCell(new Phrase(count.ToString()));
                        _trCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        _trCell.VerticalAlignment = Element.ALIGN_CENTER;
                        _trCell.BorderWidthLeft = 0;
                        _trCell.BorderWidthRight = 0;
                        _trCell.BorderWidthTop = 0;
                        _trCell.BorderWidthBottom = 1f;
                        _trCell.MinimumHeight = 18;

                        PdfPCell _nameCell = new PdfPCell(new Phrase(product.Name));
                        _nameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        _nameCell.VerticalAlignment = Element.ALIGN_CENTER;
                        _nameCell.BorderWidthLeft = 0;
                        _nameCell.BorderWidthRight = 0;
                        _nameCell.BorderWidthTop = 0;
                        _nameCell.BorderWidthBottom = 1f;
                        _nameCell.MinimumHeight = 18;

                        PdfPCell _priceCell = new PdfPCell(new Phrase(CustomSumm(product.Price)));
                        _priceCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        _priceCell.VerticalAlignment = Element.ALIGN_CENTER;
                        _priceCell.PaddingLeft = 10;
                        _priceCell.BorderWidthLeft = 0;
                        _priceCell.BorderWidthRight = 0;
                        _priceCell.BorderWidthTop = 0;
                        _priceCell.BorderWidthBottom = 1f;
                        _priceCell.MinimumHeight = 18;

                        PdfPCell _countCell = new PdfPCell(new Phrase(model.Thing[i].Count.ToString()));
                        _countCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        _countCell.VerticalAlignment = Element.ALIGN_CENTER;
                        _countCell.BorderWidthLeft = 0;
                        _countCell.BorderWidthRight = 0;
                        _countCell.BorderWidthTop = 0;
                        _countCell.BorderWidthBottom = 1f;
                        _countCell.MinimumHeight = 18;

                        PdfPCell _umumCell = new PdfPCell(new Phrase(CustomSumm(model.Thing[i].Count * product.Price)));
                        _umumCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        _umumCell.VerticalAlignment = Element.ALIGN_CENTER;
                        _umumCell.BorderWidthLeft = 0;
                        _umumCell.BorderWidthRight = 0;
                        _umumCell.BorderWidthTop = 0;
                        _umumCell.BorderWidthBottom = 1f;
                        _umumCell.MinimumHeight = 18;

                        table.AddCell(_trCell);
                        table.AddCell(_nameCell);
                        table.AddCell(_priceCell);
                        table.AddCell(_countCell);
                        table.AddCell(_umumCell);

                        sum += model.Thing[i].Count * product.Price;
                        count++;
                    }
                }

                doc.Add(table);

                Paragraph dateTime = new Paragraph("Sana va vaqt: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                doc.Add(dateTime);

                Paragraph p = new Paragraph("Jami summa: " + CustomSumm(sum) + " UZS");
                doc.Add(p);

                doc.Close();
                writer.Close();

                var constant = ms.ToArray();
                return File(constant, "application/vnd", "Invoice.pdf");
            }
        }

        private string CustomSumm(int sum)
        {
            int r = 0;
            string customSum = string.Empty;

            while (sum != 0)
            {
                r = sum % 1000;

                if (r == 0) customSum = customSum.Insert(0, "000");
                else customSum = customSum.Insert(0, r.ToString() + " ");

                sum /= 1000;
            }

            return customSum;
        }
     }
}
