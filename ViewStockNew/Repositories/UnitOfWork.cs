using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewStockNew.Data;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;

namespace ViewStockNew.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DBViewStockContext context = new DBViewStockContext();
        private GenericRepository<Cuenta> cuentaRepository;

        private GenericRepository<Localidad> localidadRepository;

        private GenericRepository<Marca> marcaRepository;

        private GenericRepository<Producto> productoRepository;

        private GenericRepository<Proveedor> proveedorRepository;

        private GenericRepository<Provincia> provinciaRepository;

        private GenericRepository<SPEC> sPECRepository;

        private GenericRepository<TipoProducto> tipoProductoRepository;

        private GenericRepository<Usuario> usuarioRepository;

        private GenericRepository<Venta> ventaRepository;

        private GenericRepository<VentaDetalle> ventaDetalleRepository;

        private GenericRepository<TipoDeUsuario> tipoUsuarioRepository;


        private GenericRepository<CodigoVenta> codigoVentaRepository;

        private GenericRepository<Pago> pagoRepository;

        private GenericRepository<PorcentajeGanancia> porcentajeGananciaRepository;

        private GenericRepository<Remito> remitoRepository;

        private GenericRepository<RemitoDetalle> remitoDetalleRepository;

        public GenericRepository<RemitoDetalle> RemitoDetalleRepository
        {
            get
            {

                if (this.remitoDetalleRepository == null)
                {
                    this.remitoDetalleRepository = new GenericRepository<RemitoDetalle>(context);
                }
                return remitoDetalleRepository;
            }
        }
        public GenericRepository<Remito> RemitoRepository
        {
            get
            {

                if (this.remitoRepository == null)
                {
                    this.remitoRepository = new GenericRepository<Remito>(context);
                }
                return remitoRepository;
            }
        }
        public GenericRepository<PorcentajeGanancia> PorcentajeGananciaRepository
        {
            get
            {

                if (this.porcentajeGananciaRepository == null)
                {
                    this.porcentajeGananciaRepository = new GenericRepository<PorcentajeGanancia>(context);
                }
                return porcentajeGananciaRepository;
            }
        }
        public GenericRepository<Pago> PagoRepository
        {
            get
            {

                if (this.pagoRepository == null)
                {
                    this.pagoRepository = new GenericRepository<Pago>(context);
                }
                return pagoRepository;
            }
        }
        public GenericRepository<Cuenta> CuentaRepository
        {
            get
            {

                if (this.cuentaRepository == null)
                {
                    this.cuentaRepository = new GenericRepository<Cuenta>(context);
                }
                return cuentaRepository;
            }
        }
        public GenericRepository<Localidad> LocalidadRepository
        {
            get
            {

                if (this.localidadRepository == null)
                {
                    this.localidadRepository = new GenericRepository<Localidad>(context);
                }
                return localidadRepository;
            }
        }
        public GenericRepository<Marca> MarcaRepository
        {
            get
            {

                if (this.marcaRepository == null)
                {
                    this.marcaRepository = new GenericRepository<Marca>(context);
                }
                return marcaRepository;
            }
        }
        public GenericRepository<Producto> ProductoRepository
        {
            get
            {

                if (this.productoRepository == null)
                {
                    this.productoRepository = new GenericRepository<Producto>(context);
                }
                return productoRepository;
            }
        }
        public GenericRepository<Proveedor> ProveedorRepository
        {
            get
            {

                if (this.proveedorRepository == null)
                {
                    this.proveedorRepository = new GenericRepository<Proveedor>(context);
                }
                return proveedorRepository;
            }
        }
        public GenericRepository<Provincia> ProvinciaRepository
        {
            get
            {

                if (this.provinciaRepository == null)
                {
                    this.provinciaRepository = new GenericRepository<Provincia>(context);
                }
                return provinciaRepository;
            }
        }
        public GenericRepository<SPEC> SPECRepository
        {
            get
            {

                if (this.sPECRepository == null)
                {
                    this.sPECRepository = new GenericRepository<SPEC>(context);
                }
                return sPECRepository;
            }
        }
        public GenericRepository<TipoProducto> TipoProductoRepository
        {
            get
            {

                if (this.tipoProductoRepository == null)
                {
                    this.tipoProductoRepository = new GenericRepository<TipoProducto>(context);
                }
                return tipoProductoRepository;
            }
        }
        public GenericRepository<Usuario> UsuarioRepository
        {
            get
            {

                if (this.usuarioRepository == null)
                {
                    this.usuarioRepository = new GenericRepository<Usuario>(context);
                }
                return usuarioRepository;
            }
        }
        public GenericRepository<Venta> VentaRepository
        {
            get
            {

                if (this.ventaRepository == null)
                {
                    this.ventaRepository = new GenericRepository<Venta>(context);
                }
                return ventaRepository;
            }
        }
        public GenericRepository<VentaDetalle> VentaDetalleRepository
        {
            get
            {

                if (this.ventaDetalleRepository == null)
                {
                    this.ventaDetalleRepository = new GenericRepository<VentaDetalle>(context);
                }
                return ventaDetalleRepository;
            }
        }

       
        public GenericRepository<TipoDeUsuario> TipoUsuarioRepository
        {
            get
            {

                if (this.tipoUsuarioRepository == null)
                {
                    this.tipoUsuarioRepository = new GenericRepository<TipoDeUsuario>(context);
                }
                return tipoUsuarioRepository;
            }
        }
        public GenericRepository<CodigoVenta> CodigoVentaRepository
        {
            get
            {

                if (this.codigoVentaRepository == null)
                {
                    this.codigoVentaRepository = new GenericRepository<CodigoVenta>(context);
                }
                return codigoVentaRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
            context.ChangeTracker.Clear();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
