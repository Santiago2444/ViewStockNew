using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewStockNew.Models;
using ViewStockNew.Repositories;

namespace ViewStockNew.Interfaces
{
    public interface IUnitOfWork
    {
        GenericRepository<Cuenta> CuentaRepository { get; }
        GenericRepository<Localidad> LocalidadRepository { get; }
        GenericRepository<Marca> MarcaRepository { get; }
        GenericRepository<Producto> ProductoRepository { get; }
        GenericRepository<Proveedor> ProveedorRepository { get; }
        GenericRepository<Provincia> ProvinciaRepository { get;}
        GenericRepository<SPEC> SPECRepository { get; }
        GenericRepository<TipoProducto> TipoProductoRepository { get; }
        GenericRepository<Usuario> UsuarioRepository { get;}
        GenericRepository<Venta> VentaRepository { get; }
        GenericRepository<VentaDetalle> VentaDetalleRepository { get;}
        GenericRepository<TipoDeUsuario> TipoUsuarioRepository { get; }
        GenericRepository<CodigoVenta> CodigoVentaRepository { get; }
        GenericRepository<Pago> PagoRepository { get; }
        GenericRepository<PorcentajeGanancia> PorcentajeGananciaRepository { get; }
        GenericRepository<Remito> RemitoRepository { get; }
        GenericRepository<RemitoDetalle> RemitoDetalleRepository { get; }


        public void Save();
        void Dispose();

    }
}
