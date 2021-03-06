﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScrumWebShop.Data;
using ScrumWebShop.Services;

namespace ScrumWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cart;

        public ProductsController(ApplicationDbContext context, ICartService cart)
        {
            _context = context;
            _cart = cart;
        }

        [AllowAnonymous]
        //GET: Products + SEARCH BY KEYWORD in ProductName or ProductDescription
        public IActionResult Index(string productBrand, string productSex, string productColor, string searchString)
        {
            ViewBag.CartItems = _cart.GetItems();

            var product = from p in _context.Products
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(p => p.ProductName.ToLower().Contains(searchString.ToLower()) || p.ProductDescription.ToLower().Contains(searchString.ToLower()));
            }

            if (!String.IsNullOrEmpty(productBrand))
            {
                product = product.Where(p => p.Brand == productBrand);
            }

            if (!String.IsNullOrEmpty(productSex))
            {
                product = product.Where(p => p.Sex == productSex);
            }

            if (!String.IsNullOrEmpty(productColor))
            {
                product = product.Where(p => p.Color == productColor);
            }

            var brands = (from p in _context.Products
                          select p.Brand).Distinct().ToList();
            ViewBag.Brands = brands;

            var sexes = (from p in _context.Products
                         select p.Sex).Distinct().ToList();
            ViewBag.Sexes = sexes;

            var colors = (from p in _context.Products
                          select p.Color).Distinct().ToList();
            ViewBag.Colors = colors;

            return View(product.ToList());
        }

        [AllowAnonymous]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProductPage(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
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
            ViewBag.Brands = new string[] { "Oakley", "Rayban", "Polaroid", "Police" };
            ViewBag.Sex = new string[] { "Kvinnor", "Män", "Barn" };
            ViewBag.Colors = new string[] { "Grå", "Grön", "Blå" };

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductNumber,ProductName,ProductPrice,ProductDescription,Brand,Sex,Color,Photo")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Brands = new string[] { "Oakley", "Rayban", "Polaroid", "Police" };
            ViewBag.Sex = new string[] { "Kvinnor", "Män", "Barn" };
            ViewBag.Colors = new string[] { "Grå", "Grön", "Blå" };

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductNumber,ProductName,ProductPrice,ProductDescription,Brand,Sex,Color,Photo")] Product product)
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        [AllowAnonymous]
        public IActionResult AddItem(Guid id)
        {
            _cart.AddItem(id);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public IActionResult RemoveItem(Guid id)
        {
            _cart.RemoveItem(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
