using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class SPECsController : Controller
    {
        private readonly DBViewStockContext _context;

        public SPECsController(DBViewStockContext context)
        {
            _context = context;
        }

        // GET: SPECs
        public async Task<IActionResult> Index()
        {
              return _context.SPECS != null ? 
                          View(await _context.SPECS.ToListAsync()) :
                          Problem("Entity set 'DBViewStockContext.SPECS'  is null.");
        }

        // GET: SPECs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SPECS == null)
            {
                return NotFound();
            }

            var sPEC = await _context.SPECS
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPEC == null)
            {
                return NotFound();
            }

            return View(sPEC);
        }

        // GET: SPECs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SPECs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Modificacion,Visible")] SPEC sPEC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sPEC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sPEC);
        }

        // GET: SPECs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SPECS == null)
            {
                return NotFound();
            }

            var sPEC = await _context.SPECS.FindAsync(id);
            if (sPEC == null)
            {
                return NotFound();
            }
            return View(sPEC);
        }

        // POST: SPECs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Modificacion,Visible")] SPEC sPEC)
        {
            if (id != sPEC.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sPEC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SPECExists(sPEC.Id))
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
            return View(sPEC);
        }

        // GET: SPECs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SPECS == null)
            {
                return NotFound();
            }

            var sPEC = await _context.SPECS
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPEC == null)
            {
                return NotFound();
            }

            return View(sPEC);
        }

        // POST: SPECs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SPECS == null)
            {
                return Problem("Entity set 'DBViewStockContext.SPECS'  is null.");
            }
            var sPEC = await _context.SPECS.FindAsync(id);
            if (sPEC != null)
            {
                _context.SPECS.Remove(sPEC);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SPECExists(int id)
        {
          return (_context.SPECS?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
