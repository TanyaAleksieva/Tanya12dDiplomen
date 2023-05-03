using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FleksTanya12d.Data;
using Microsoft.AspNetCore.Authorization;

namespace FleksTanya12d.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class ProductsController : Controller
    {
        private readonly FleksDbContext _context;

        public ProductsController(FleksDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var fleksDbContext = _context.Products.Include(p => p.Categories).Include(p => p.Sports).Include(p => p.Types);
            return View(await fleksDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Categories)
                .Include(p => p.Sports)
                .Include(p => p.Types)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Id", "CategoryName");  //
            ViewData["SportName"] = new SelectList(_context.Sports, "Id", "SportName");  //
            ViewData["TypeName"] = new SelectList(_context.Types, "Id", "TypeName");  //
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,Size,Quantity,Description,CategoryId,TypeId,SportId,Price,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                //product.DateRegister = DateTime.Now;
                //product.DataCreated = DateTime.Now;
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);  //
            ViewData["SportName"] = new SelectList(_context.Sports, "Id", "SportName", product.SportId);
            ViewData["TypeName"] = new SelectList(_context.Types, "Id", "TypeName", product.TypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId); //
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "SportName", product.SportId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "TypeName", product.TypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Size,Quantity,Description,CategoryId,TypeId,SportId,Price,Image")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //product.DateRegister = DateTime.Now;
                    _context.Products.Update(product);
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);  //
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "SportName", product.SportId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "TypeName", product.TypeId);
            return View(product);
        }

        // GET: Products/Delete/5-
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Categories)
                .Include(p => p.Sports)
                .Include(p => p.Types)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'FleksDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.Id == id);
        }
    }
}
