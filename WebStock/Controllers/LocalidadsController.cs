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
    public class LocalidadsController : Controller
    {
        private readonly DBViewStockContext _context;

        public LocalidadsController(DBViewStockContext context)
        {
            _context = context;
        }

        // GET: Localidads
        public async Task<IActionResult> Index()
        {
              return _context.Localidades != null ? 
                          View(await _context.Localidades.ToListAsync()) :
                          Problem("Entity set 'DBViewStockContext.Localidades'  is null.");
        }

        // GET: Localidads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Localidades == null)
            {
                return NotFound();
            }

            var localidad = await _context.Localidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localidad == null)
            {
                return NotFound();
            }

            return View(localidad);
        }

        // GET: Localidads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Localidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Modificacion,Visible")] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(localidad);
        }

        // GET: Localidads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Localidades == null)
            {
                return NotFound();
            }

            var localidad = await _context.Localidades.FindAsync(id);
            if (localidad == null)
            {
                return NotFound();
            }
            return View(localidad);
        }

        // POST: Localidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Modificacion,Visible")] Localidad localidad)
        {
            if (id != localidad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalidadExists(localidad.Id))
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
            return View(localidad);
        }

        // GET: Localidads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Localidades == null)
            {
                return NotFound();
            }

            var localidad = await _context.Localidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localidad == null)
            {
                return NotFound();
            }

            return View(localidad);
        }

        // POST: Localidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Localidades == null)
            {
                return Problem("Entity set 'DBViewStockContext.Localidades'  is null.");
            }
            var localidad = await _context.Localidades.FindAsync(id);
            if (localidad != null)
            {
                _context.Localidades.Remove(localidad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalidadExists(int id)
        {
          return (_context.Localidades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
