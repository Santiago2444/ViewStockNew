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
    public class TipoProductoesController : Controller
    {
        private readonly DBViewStockContext _context;

        public TipoProductoesController(DBViewStockContext context)
        {
            _context = context;
        }

        // GET: TipoProductoes
        public async Task<IActionResult> Index()
        {
              return _context.TipoProducto != null ? 
                          View(await _context.TipoProducto.ToListAsync()) :
                          Problem("Entity set 'DBViewStockContext.TipoProducto'  is null.");
        }

        // GET: TipoProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoProducto == null)
            {
                return NotFound();
            }

            var tipoProducto = await _context.TipoProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoProducto == null)
            {
                return NotFound();
            }

            return View(tipoProducto);
        }

        // GET: TipoProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Modificacion,Visible")] TipoProducto tipoProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoProducto);
        }

        // GET: TipoProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoProducto == null)
            {
                return NotFound();
            }

            var tipoProducto = await _context.TipoProducto.FindAsync(id);
            if (tipoProducto == null)
            {
                return NotFound();
            }
            return View(tipoProducto);
        }

        // POST: TipoProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Modificacion,Visible")] TipoProducto tipoProducto)
        {
            if (id != tipoProducto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoProductoExists(tipoProducto.Id))
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
            return View(tipoProducto);
        }

        // GET: TipoProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoProducto == null)
            {
                return NotFound();
            }

            var tipoProducto = await _context.TipoProducto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoProducto == null)
            {
                return NotFound();
            }

            return View(tipoProducto);
        }

        // POST: TipoProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoProducto == null)
            {
                return Problem("Entity set 'DBViewStockContext.TipoProducto'  is null.");
            }
            var tipoProducto = await _context.TipoProducto.FindAsync(id);
            if (tipoProducto != null)
            {
                _context.TipoProducto.Remove(tipoProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProductoExists(int id)
        {
          return (_context.TipoProducto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
