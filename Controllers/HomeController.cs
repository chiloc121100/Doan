using AuctionProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AuctionProject.Data;
using Microsoft.EntityFrameworkCore;

namespace AuctionProject.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment Environment;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment _environment, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            Environment = _environment;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var Products = (from e in _context.Product
                            join a in _context.User on e.User.Id equals a.Id
                            select new Product
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Description = e.Description,
                                DateEnd = e.DateEnd,
                                DateStart = e.DateStart,
                                Image = e.Image,
                                PriceStart = e.PriceStart,
                                PriceEnd = e.PriceEnd,
                                PriceFinish = e.PriceFinish,
                                PriceJump = e.PriceJump,
                                User = e.User,
                                UserGet = e.UserGet,

                            });
            return View(Products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
{
    var products = await (from e in _context.Product
                          join a in _context.User on e.User.Id equals a.Id
                          select new Product
                          {
                              Id = e.Id,
                              Title = e.Title,
                              Description = e.Description,
                              DateEnd = e.DateEnd,
                              DateStart = e.DateStart,
                              Image = e.Image,
                              PriceStart = e.PriceStart,
                              PriceEnd = e.PriceEnd,
                              PriceFinish = e.PriceFinish,
                              PriceJump = e.PriceJump,
                              User = e.User,
                              UserGet = e.UserGet,

                          }).ToListAsync(); // Await the asynchronous operation
/*            var UpComingEvent = products.Where(e => e.DateEnd > DateTime.Now);
            var PastEvent = products.Where(e => e.DateEnd < DateTime.Now);
            ViewBag.UpComingEvents = UpComingEvent;
            ViewBag.PastEvents = PastEvent;*/
            return Json(products);
}

        [HttpGet]
        public async Task<IActionResult> PastEvent()
        {
            var products = await (from e in _context.Product
                                  join a in _context.User on e.User.Id equals a.Id
                                  select new Product
                                  {
                                      Id = e.Id,
                                      Title = e.Title,
                                      Description = e.Description,
                                      DateEnd = e.DateEnd,
                                      DateStart = e.DateStart,
                                      Image = e.Image,
                                      PriceStart = e.PriceStart,
                                      PriceEnd = e.PriceEnd,
                                      PriceFinish = e.PriceFinish,
                                      PriceJump = e.PriceJump,
                                      User = e.User,
                                      UserGet = e.UserGet,

                                  }).ToListAsync(); // Await the asynchronous operation
           /* var UpComingEvent = products.Where(e => e.DateEnd > DateTime.Now);*/
            var PastEvent = products.Where(e => e.DateEnd < DateTime.Now);
            /*ViewBag.UpComingEvents = UpComingEvent;
            ViewBag.PastEvents = PastEvent;*/
            return Json(PastEvent);
        }

        [HttpGet]
        public async Task<IActionResult> NowEvent()
        {
            var products = await (from e in _context.Product
                                  join a in _context.User on e.User.Id equals a.Id
                                  select new Product
                                  {
                                      Id = e.Id,
                                      Title = e.Title,
                                      Description = e.Description,
                                      DateEnd = e.DateEnd,
                                      DateStart = e.DateStart,
                                      Image = e.Image,
                                      PriceStart = e.PriceStart,
                                      PriceEnd = e.PriceEnd,
                                      PriceFinish = e.PriceFinish,
                                      PriceJump = e.PriceJump,
                                      User = e.User,
                                      UserGet = e.UserGet,

                                  }).ToListAsync(); // Await the asynchronous operation
             var UpComingEvent = products.Where(e => e.DateEnd > DateTime.Now);
            //var PastEvent = products.Where(e => e.DateEnd < DateTime.Now);
            /*ViewBag.UpComingEvents = UpComingEvent;
            ViewBag.PastEvents = PastEvent;*/
            return Json(UpComingEvent);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            var Products = (from e in _context.Product
                            join a in _context.User on e.User.Id equals a.Id
                            select new Product
                            {
                                Id = e.Id,
                                Title = e.Title,
                                Description = e.Description,
                                DateEnd = e.DateEnd,
                                DateStart = e.DateStart,
                                Image = e.Image,
                                PriceStart = e.PriceStart,
                                PriceEnd = e.PriceEnd,
                                PriceFinish = e.PriceFinish,
                                PriceJump = e.PriceJump,
                                User = e.User,
                                UserGet = e.UserGet,

                            });
            var UpComingEvent = Products.Where(e => e.DateEnd > DateTime.Now);
            var PastEvent = Products.Where(e => e.DateEnd < DateTime.Now);
            ViewBag.UpComingEvents = UpComingEvent;
            ViewBag.PastEvents = PastEvent;
            return View(Products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /* public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
         {
             string wwwPath = this.Environment.WebRootPath;
             string contentPath = this.Environment.ContentRootPath;
             long size = files.Sum(f => f.Length);
             var folderPath = Path.Combine(wwwPath,"Upload"); // duong dan den thu muc + file

             var time = DateAndTime.Now;
             foreach (var formFile in files)
             {
                 if (formFile.Length > 0)
                 {
                     var filePath = Path.Combine(folderPath, formFile.FileName);
                     using (var stream = System.IO.File.Create(filePath))
                     {                 
                         await formFile.CopyToAsync(stream);
                     }
                 }
             }

             // Process uploaded files
             // Don't rely on or trust the FileName property without validation.
             *//*return View();*//*
             return Ok(new { count = files.Count, size });
         }*/
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            // nhan moi truong tu server hoac may tinh
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            // kiem tra size
            long size = files.Sum(f => f.Length);
            // duong dan den thu muc + file -> wwwPath se dan den file wwwroot
            var folderPath = Path.Combine(contentPath, "Upload");

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // add time vao file anh de khong bi trung
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    // nhan ten minh dat ko bao gom duoi
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formFile.FileName);
                    // lay lai cai duoi file
                    var fileExtension = Path.GetExtension(formFile.FileName);
                    // ket hop lai
                    var newFileName = $"{fileNameWithoutExtension}_{timestamp}{fileExtension}";
                    // file name + cai ten moi
                    var filePath = Path.Combine(folderPath, newFileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.
            /*return View();*/
            return Ok(new { count = files.Count, size });
        }
        //offer
        [HttpPost]
        public async Task<IActionResult> Offer(int? productId, float? priceFinish)
        {
            var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == productId);
            ApplicationUser author = await _userManager.GetUserAsync(HttpContext.User) ?? new ApplicationUser();
            if(author.Email == null || string.IsNullOrEmpty(author.Id))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            { 
            product.UserGet = author;
            //add author to list
            
            if (product.PriceFinish  != priceFinish)
            {
                TempData["ErrorMessage"] = "Someone has offered a higher price than the current one.";
            }
            else
            {
                var tempPriceFinish = product.PriceFinish;
                if (tempPriceFinish != null && tempPriceFinish >= 0)
                {
                    product.PriceFinish = product.PriceJump + tempPriceFinish;
                }
                else
                {
                    product.PriceFinish = product.PriceStart + product.PriceJump + tempPriceFinish;
                }
                _context.Update(product);
                await _context.SaveChangesAsync();
                /*return View("~/Products");*/
            }
            return RedirectToAction(nameof(Index));
                }
        }
        
    }
}