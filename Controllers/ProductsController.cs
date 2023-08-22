using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuctionProject.Data;
using AuctionProject.Models;
using Microsoft.AspNetCore.Identity;

namespace AuctionProject.Controllers
{
    public class ProductsController : Controller
    {
        private IWebHostEnvironment Environment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Environment = environment;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index()
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

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public async Task<IActionResult> Create([Bind("Id,Title,Description,IsPublic,DateStart,DateEnd,Image,PriceStart,PriceEnd,PriceJump,PriceFinish")] Product @product, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                // kiem tra size
                long size = files.Sum(f => f.Length);
                // duong dan den thu muc + file -> wwwPath se dan den file wwwroot
                var folderPath = Path.Combine(wwwPath, "Upload");

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
                        product.Image = newFileName;
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                *//* product.User = User.Identity.IsAuthenticated;*//*
                ApplicationUser author = await _userManager.GetUserAsync(HttpContext.User) ?? new ApplicationUser();
                @product.User = author;

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }*/
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DateStart,DateEnd,Image,PriceStart,PriceEnd,PriceJump,PriceFinish")] Product @product, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                // kiem tra size
                long size = files.Sum(f => f.Length);
                // duong dan den thu muc + file -> wwwPath se dan den file wwwroot
                var folderPath = Path.Combine(wwwPath, "Upload");

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
                        product.Image = newFileName;
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                /* product.User = User.Identity.IsAuthenticated;*/
                ApplicationUser author = await _userManager.GetUserAsync(HttpContext.User) ?? new ApplicationUser();
                @product.User = author;
                product.UserGet = author;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsPublic,DateStart,DateEnd,Image,PriceStart,PriceEnd,PriceJump,PriceFinish")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> Offer(int? id)
        {
            var product1 = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            ApplicationUser author = await _userManager.GetUserAsync(HttpContext.User) ?? new ApplicationUser();
            product1.UserGet = author;
            product1.PriceFinish = product1.PriceStart + product1.PriceJump;
            _context.Update(product1);
            await _context.SaveChangesAsync();
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
            /*return View("~/Products");*/
                return RedirectToAction(nameof(Index));
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
