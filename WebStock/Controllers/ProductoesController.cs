using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly DBViewStockContext _context;
        IQueryable<Producto>? productList;

        public ProductoesController(DBViewStockContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index(string txtBusqueda)
        {
            if (!string.IsNullOrWhiteSpace(txtBusqueda))
            {
                var dBViewStockContext = _context.Productos.Include(p => p.Marca).Include(p => p.Proveedor).Include(p => p.SPEC).Include(p => p.TipoProducto).Include(p => p.Usuario).Where(p=>p.TipoProducto.Nombre.Contains(txtBusqueda) && p.Visible.Equals(true) || p.Marca.Nombre.Contains(txtBusqueda) && p.Visible.Equals(true) || p.SPEC.Nombre.Contains(txtBusqueda) && p.Visible.Equals(true) || p.Proveedor.Nombre.Contains(txtBusqueda) && p.Visible.Equals(true) || p.Usuario.Nombre.Contains(txtBusqueda) && p.Visible.Equals(true));
                productList = dBViewStockContext;
                GetImagenForeach();

                return View(await dBViewStockContext.ToListAsync());
            }
            else
            {
                var dBViewStockContext = _context.Productos.Include(p => p.Marca).Include(p => p.Proveedor).Include(p => p.SPEC).Include(p => p.TipoProducto).Include(p => p.Usuario).Where(p=>p.Visible.Equals(true));
                productList = dBViewStockContext;
                GetImagenForeach();

                return View(await dBViewStockContext.ToListAsync());
            }
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Marca)
                .Include(p => p.Proveedor)
                .Include(p => p.SPEC)
                .Include(p => p.TipoProducto)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre");
            ViewData["SPECId"] = new SelectList(_context.SPECS, "Id", "Nombre");
            ViewData["TipoProductoId"] = new SelectList(_context.TipoProducto, "Id", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            //
            GetFechaActual();
            //
            return View();
        }

        private ActionResult GetFechaActual()
        {
            DateTime fecha = DateTime.Now;
            ViewBag.Fecha = Convert.ToString(fecha);
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoProductoId,MarcaId,Detalles,SPECId,Descuento,PrecioBulto,CantidadBulto,PrecioUnidad,Ganancia,PVP,Stock,ProveedorId,Imagen,Visible,Modificacion,UsuarioId,Carrito")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.MarcaId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", producto.ProveedorId);
            ViewData["SPECId"] = new SelectList(_context.SPECS, "Id", "Nombre", producto.SPECId);
            ViewData["TipoProductoId"] = new SelectList(_context.TipoProducto, "Id", "Nombre", producto.TipoProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", producto.UsuarioId);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.MarcaId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", producto.ProveedorId);
            ViewData["SPECId"] = new SelectList(_context.SPECS, "Id", "Nombre", producto.SPECId);
            ViewData["TipoProductoId"] = new SelectList(_context.TipoProducto, "Id", "Nombre", producto.TipoProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", producto.UsuarioId);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoProductoId,MarcaId,Detalles,SPECId,Descuento,PrecioBulto,CantidadBulto,PrecioUnidad,Ganancia,PVP,Stock,ProveedorId,Imagen,Visible,Modificacion,UsuarioId,Carrito")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.MarcaId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", producto.ProveedorId);
            ViewData["SPECId"] = new SelectList(_context.SPECS, "Id", "Nombre", producto.SPECId);
            ViewData["TipoProductoId"] = new SelectList(_context.TipoProducto, "Id", "Nombre", producto.TipoProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", producto.UsuarioId);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Marca)
                .Include(p => p.Proveedor)
                .Include(p => p.SPEC)
                .Include(p => p.TipoProducto)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            byte[]? Imagen = producto.Imagen;

            if (producto == null)
            {
                return NotFound();
            }

            byteArrayToImage(Imagen);
            return View(producto);
        }

        public ActionResult byteArrayToImage(byte[] imgBytes)
        {
            string imreBase64Data = Convert.ToBase64String(imgBytes);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            //Passing image data in viewbag to view
            ViewBag.ImageData = imgDataURL;
            return View();
        }

        public ActionResult GetImagenForeach()
        {
            foreach (Producto item in productList)
            {
                var producto = _context.Productos.Find(item.Id);
                string imreBase64Data = Convert.ToBase64String(producto.Imagen);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                //Passing image data in viewbag to view
                ViewBag.ImagenProducto = imgDataURL;

            }
            //
            return View();

        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'DBViewStockContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
