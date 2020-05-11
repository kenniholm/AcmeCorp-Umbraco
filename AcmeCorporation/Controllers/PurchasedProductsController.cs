using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcmeCorporation.Models;

namespace AcmeCorporation.Controllers
{
    public class PurchasedProductsController : Controller
    {
        private readonly AcmeCorporationContext _context;

        public PurchasedProductsController(AcmeCorporationContext context)
        {
            _context = context;
        }

        // GET: PurchasedProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.PurchasedProduct.ToListAsync());
        }

        // GET: PurchasedProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchasedProduct = await _context.PurchasedProduct
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (purchasedProduct == null)
            {
                return NotFound();
            }

            return View(purchasedProduct);
        }

        // GET: PurchasedProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurchasedProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductSerial")] PurchasedProduct purchasedProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchasedProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchasedProduct);
        }

        // GET: PurchasedProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchasedProduct = await _context.PurchasedProduct.FindAsync(id);
            if (purchasedProduct == null)
            {
                return NotFound();
            }
            return View(purchasedProduct);
        }

        // POST: PurchasedProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductSerial")] PurchasedProduct purchasedProduct)
        {
            if (id != purchasedProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchasedProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchasedProductExists(purchasedProduct.ProductId))
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
            return View(purchasedProduct);
        }

        // GET: PurchasedProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchasedProduct = await _context.PurchasedProduct
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (purchasedProduct == null)
            {
                return NotFound();
            }

            return View(purchasedProduct);
        }

        // POST: PurchasedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchasedProduct = await _context.PurchasedProduct.FindAsync(id);
            _context.PurchasedProduct.Remove(purchasedProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchasedProductExists(int id)
        {
            return _context.PurchasedProduct.Any(e => e.ProductId == id);
        }
    }
}
