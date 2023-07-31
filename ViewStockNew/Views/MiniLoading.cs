using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;
using ViewStockNew.Repositories;
using ViewStockNew.Utils;

namespace ViewStockNew.Views
{
    public partial class MiniLoading : Form
    {
        IUnitOfWork unitOfWork;
        private Producto producto;
        private VentaDetalle ventaDetalle;
        private bool _repetido;
        private bool delete;
        private int codigoVenta;
        private int idcuenta;
        private bool FilterDC;
        private bool FilterDP;
        private bool Cfilter;
        private bool Pfilter;
        private int FilterUsuariosVenta;
        private int FilterFechaVenta;
        private int FilterTipoId;
        private int FilterMarcaId;
        private int FilterSpecId;
        private int FilterProveedorId;
        private int IdCuenta;
        private DateTime FechaDesdeSend;
        private DateTime FechaHastaSend;
        private string txtBusqueda;
        private string Grid;
        private RemitoDetalle remitoDetalle;
        private bool repetido;
        private int idRemito;

        public MiniLoading(VentaDetalle ventaDetalle, Producto producto, IUnitOfWork unitOfWork, bool _repetido)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.ventaDetalle = ventaDetalle;
            this.producto = producto;
            this._repetido = _repetido;
            //
            AgregarProductoCarrito();
        }

        public MiniLoading(bool delete, VentaDetalle ventaDetalle, Producto producto, IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            //
            this.delete = delete;
            this.ventaDetalle = ventaDetalle;
            this.producto = producto;
            this.unitOfWork = unitOfWork;
            //
            QuitarProductoCarrito();
        }

        public MiniLoading(int codigoVenta, int idcuenta, IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            //
            this.codigoVenta = codigoVenta;
            this.idcuenta = idcuenta;
            this.unitOfWork = unitOfWork;
            //
            DeudaSelected();
        }

        public MiniLoading(int codigoVenta, IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            //
            this.codigoVenta = codigoVenta;
            this.unitOfWork = unitOfWork;
            //
            VentaSelecteed();
        }

        public MiniLoading(int filterUsuariosVenta, int filterFechaVenta, int filterTipoId, int filterMarcaId, int filterSpecId, int filterProveedorId, bool filterDC, bool filterDP, int idcuenta, DateTime fechaDesdeSend, DateTime fechaHastaSend, bool cfilter, bool pfilter, IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            //
            this.FilterUsuariosVenta = filterUsuariosVenta;
            this.FilterFechaVenta = filterFechaVenta;
            this.FilterTipoId = filterTipoId;
            this.FilterMarcaId = filterMarcaId;
            this.FilterSpecId = filterSpecId;
            this.FilterProveedorId = filterProveedorId;
            this.FilterDC = filterDC;
            this.FilterDP = filterDP;
            this.Cfilter = cfilter;
            this.Pfilter = pfilter;
            this.IdCuenta = idcuenta;
            this.FechaHastaSend = fechaHastaSend;
            this.FechaDesdeSend = fechaDesdeSend;
            this.unitOfWork = unitOfWork;
            //
            FiltrarCuenta();
        }

        public MiniLoading(string txtBusqueda, string grid, IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            //
            this.txtBusqueda = txtBusqueda;
            this.Grid = grid;
            this.unitOfWork = unitOfWork;
            //
            SearchCuenta();
        }

        public MiniLoading(RemitoDetalle remitoDetalle, IUnitOfWork unitOfWork, bool repetido, Producto productoRemito)
        {
            InitializeComponent();
            //
            this.remitoDetalle = remitoDetalle;
            this.unitOfWork = unitOfWork;
            this.repetido = repetido;
            this.producto = productoRemito;
            //
            AgregarProductoRemito();
        }

        public MiniLoading(IUnitOfWork unitOfWork, RemitoDetalle? remitoDetalle, bool delete, Producto productoRemito)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.remitoDetalle = remitoDetalle;
            this.delete = delete;
            this.producto = productoRemito;
            //
            QuitarProductoRemito();
        }

        public MiniLoading(IUnitOfWork unitOfWork, int idRemito)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.idRemito = idRemito;
            //
            DetallesRemito();
        }

        private async void DetallesRemito()
        {
            ClasesCompartidas.FilterRemito.DataSource = await unitOfWork.RemitoDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC), filter: q => q.RemitoId.Equals(idRemito));
            //
            Close();
        }

        private async void QuitarProductoRemito()
        {
            try
            {
                new ModelsValidator().Validate(remitoDetalle);
                new ModelsValidator().Validate(producto);
                //
                unitOfWork.ProductoRepository.Update(producto);
                //
                if (delete == true)
                    unitOfWork.RemitoDetalleRepository.Delete(remitoDetalle);
                else
                    unitOfWork.RemitoDetalleRepository.Update(remitoDetalle);
                unitOfWork.Save();
                //
                ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                ClasesCompartidas.RemitosDetalle.DataSource = await unitOfWork.RemitoDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC), filter: q => q.RemitoId == null);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            Close();
        }

        private async void AgregarProductoRemito()
        {
            try
            {
                new ModelsValidator().Validate(remitoDetalle);
                new ModelsValidator().Validate(producto);
                //
                unitOfWork.ProductoRepository.Update(producto);
                //
                if (repetido == false)
                    unitOfWork.RemitoDetalleRepository.Add(remitoDetalle);
                else
                    unitOfWork.RemitoDetalleRepository.Update(remitoDetalle);
                unitOfWork.Save();
                //
                ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                ClasesCompartidas.RemitosDetalle.DataSource = await unitOfWork.RemitoDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC), filter: q => q.RemitoId == null);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            Close();
        }

        private async void SearchCuenta()
        {
            if (Grid == "Deudas")
                ClasesCompartidas.ComprasDeudasListFilter.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(IdCuenta) && q.Usuario.Nombre.Contains(txtBusqueda));
            if (Grid == "Compras")
                ClasesCompartidas.ComprasListFilter.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado" && q.CuentaId.Equals(IdCuenta) && q.Usuario.Nombre.Contains(txtBusqueda));
            if (Grid == "ProductosDeudas")
                ClasesCompartidas.ProductosDeudasListFilter.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.TipoProducto.Nombre.Contains(txtBusqueda) || q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.Marca.Nombre.Contains(txtBusqueda) || q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.SPEC.Nombre.Contains(txtBusqueda) || q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.Proveedor.Nombre.Contains(txtBusqueda) || q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.Usuario.Nombre.Contains(txtBusqueda));
            if (Grid == "ProductosPagados")
            {
                ClasesCompartidas.ProductosListFilter.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.TipoProducto.Nombre.Contains(txtBusqueda) || q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.Marca.Nombre.Contains(txtBusqueda) || q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.SPEC.Nombre.Contains(txtBusqueda) || q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.Proveedor.Nombre.Contains(txtBusqueda) || q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.Usuario.Nombre.Contains(txtBusqueda));
            }
            //
            Close();
        }

        private async void FiltrarCuenta()
        {
            if (FilterDC == true)
            {
                #region Código
                ClasesCompartidas.ComprasDeudasListFilter.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter:
                /*♥Filtrado por CUENTA, USUARIO y FECHA*/  FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.Fecha >= Convert.ToDateTime(FechaDesdeSend) && q.Fecha <= Convert.ToDateTime(FechaHastaSend) && q.Estado == "Deuda" && q.CuentaId.Equals(IdCuenta) :
                /*Filtrado por FECHA y Usuario*/ FilterFechaVenta != 0 && FilterUsuariosVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.Fecha >= Convert.ToDateTime(FechaDesdeSend) && q.Fecha <= Convert.ToDateTime(FechaHastaSend) && q.Estado == "Deuda" && q.CuentaId.Equals(IdCuenta) :
                /*♥Filtrado por FECHA*/ FilterFechaVenta != 0 ? q => q.Fecha >= Convert.ToDateTime(FechaDesdeSend) && q.Fecha <= Convert.ToDateTime(FechaHastaSend) && q.Estado == "Deuda" && q.CuentaId.Equals(IdCuenta) :
                /*♥Filtrado por USUARIO*/ FilterUsuariosVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(IdCuenta) : q => q.Estado == "Deuda" && q.CuentaId.Equals(IdCuenta));
                #endregion
            }
            //
            if (FilterDP == true)
            {
                #region FiltradoDataProductosDeudas

                ClasesCompartidas.ProductosDeudasListFilter.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter:
                /*♥Con Todos los Valores*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Rango de Fecha*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Usuario*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Tipo*/  FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin RDF y Usuario*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin RDF y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin RDF Y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin RDF y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin RDF y Tipo*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin USUARIO y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin PROVEEDOR y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin PROVEEDOR y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin PROVEEDOR y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin SPEC y Marca*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin SPEC y Tipo*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin MARCA y TIPO*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin FECHA, USUARIO y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin FECHA, USUARIO y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin FECHA, USUARIO y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin FECHA, USUARIO y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin USUARIO, PROVEEDOR y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO, PROVEEDOR y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO, PROVEEDOR y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin PROVEEDOR, SPEC y Fecha*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin PROVEEDOR, SPEC y Marca*/ FilterTipoId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin PROVEEDOR, SPEC y Tipo*/ FilterMarcaId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin SPEC, MARCA y Fecha*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin SPEC, MARCA y Usuario*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin SPEC, MARCA y Tipo*/  FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin MARCA, TIPO y Fecha*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin MARCA, TIPO y Usuario*/  FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin MARCA, TIPO y Proveedor*/ FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin TIPO, FECHA, y Proveedor*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin TIPO, FECHA y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin TIPO, USUARIO y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando FECHA y Usuario*/ FilterFechaVenta != 0 && FilterUsuariosVenta != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando FECHA y Proveedor*/ FilterFechaVenta != 0 && FilterProveedorId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando FECHA y Spec*/ FilterFechaVenta != 0 && FilterSpecId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando FECHA y Marca*/ FilterFechaVenta != 0 && FilterMarcaId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando FECHA y Tipo*/ FilterFechaVenta != 0 && FilterTipoId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por USUARIO y Proveedor*/ FilterUsuariosVenta != 0 && FilterProveedorId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por USUARIO y Spec*/ FilterUsuariosVenta != 0 && FilterSpecId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por USUARIO y Marca*/ FilterUsuariosVenta != 0 && FilterMarcaId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por USUARIO y Tipo*/ FilterUsuariosVenta != 0 && FilterTipoId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por PROVEEDOR y Spec*/ FilterProveedorId != 0 && FilterSpecId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por PROVEEDOR y Marca*/ FilterProveedorId != 0 && FilterMarcaId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por PROVEEDOR y Tipo*/ FilterProveedorId != 0 && FilterTipoId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por SPEC y Marca*/ FilterSpecId != 0 && FilterMarcaId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por SPEC y Tipo*/ FilterSpecId != 0 && FilterTipoId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por MARCA y Tipo*/ FilterMarcaId != 0 && FilterTipoId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por FECHA*/ FilterFechaVenta != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtranndo por USUARIO*/ FilterUsuariosVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por SPEC*/ FilterSpecId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por PROVEEDOR*/ FilterProveedorId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por MARCA*/ FilterMarcaId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por TIPO ~ TODO*/ FilterTipoId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) : q => q.Pagado == "No" && q.CuentaId.Equals(IdCuenta));
                #endregion
            }
            //
            if (Cfilter == true)
            {
                #region Código
                ClasesCompartidas.ComprasList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter:
                /*♥Filtrado por CUENTA, USUARIO y FECHA*/  FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.Fecha >= Convert.ToDateTime(FechaDesdeSend) && q.Fecha <= Convert.ToDateTime(FechaHastaSend) && q.Estado == "Pagado" && q.CuentaId.Equals(IdCuenta) :
                /*Filtrado por FECHA y Usuario*/ FilterFechaVenta != 0 && FilterUsuariosVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.Fecha >= Convert.ToDateTime(FechaDesdeSend) && q.Fecha <= Convert.ToDateTime(FechaHastaSend) && q.Estado == "Pagado" && q.CuentaId.Equals(IdCuenta) :
                /*♥Filtrado por FECHA*/ FilterFechaVenta != 0 ? q => q.Fecha >= Convert.ToDateTime(FechaDesdeSend) && q.Fecha <= Convert.ToDateTime(FechaHastaSend) && q.Estado == "Pagado" && q.CuentaId.Equals(IdCuenta) :
                /*♥Filtrado por USUARIO*/ FilterUsuariosVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(IdCuenta) : q => q.Estado == "Pagado" && q.CuentaId.Equals(IdCuenta));
                #endregion
            }
            //
            if (Pfilter == true)
            {
                #region FiltradoDataProductosComprados

                ClasesCompartidas.ProductosDeudasListFilter.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter:
                /*♥Con Todos los Valores*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Rango de Fecha*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Usuario*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin Tipo*/  FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin RDF y Usuario*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin RDF y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin RDF Y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin RDF y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin RDF y Tipo*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin USUARIO y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin PROVEEDOR y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin PROVEEDOR y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin PROVEEDOR y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin SPEC y Marca*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin SPEC y Tipo*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin MARCA y TIPO*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin FECHA, USUARIO y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin FECHA, USUARIO y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin FECHA, USUARIO y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin FECHA, USUARIO y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin USUARIO, PROVEEDOR y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO, PROVEEDOR y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin USUARIO, PROVEEDOR y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin PROVEEDOR, SPEC y Fecha*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin PROVEEDOR, SPEC y Marca*/ FilterTipoId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin PROVEEDOR, SPEC y Tipo*/ FilterMarcaId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin SPEC, MARCA y Fecha*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin SPEC, MARCA y Usuario*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin SPEC, MARCA y Tipo*/  FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin MARCA, TIPO y Fecha*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin MARCA, TIPO y Usuario*/  FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin MARCA, TIPO y Proveedor*/ FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Sin TIPO, FECHA, y Proveedor*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin TIPO, FECHA y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Sin TIPO, USUARIO y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando FECHA y Usuario*/ FilterFechaVenta != 0 && FilterUsuariosVenta != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando FECHA y Proveedor*/ FilterFechaVenta != 0 && FilterProveedorId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando FECHA y Spec*/ FilterFechaVenta != 0 && FilterSpecId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando FECHA y Marca*/ FilterFechaVenta != 0 && FilterMarcaId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando FECHA y Tipo*/ FilterFechaVenta != 0 && FilterTipoId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por USUARIO y Proveedor*/ FilterUsuariosVenta != 0 && FilterProveedorId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por USUARIO y Spec*/ FilterUsuariosVenta != 0 && FilterSpecId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por USUARIO y Marca*/ FilterUsuariosVenta != 0 && FilterMarcaId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por USUARIO y Tipo*/ FilterUsuariosVenta != 0 && FilterTipoId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por PROVEEDOR y Spec*/ FilterProveedorId != 0 && FilterSpecId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por PROVEEDOR y Marca*/ FilterProveedorId != 0 && FilterMarcaId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por PROVEEDOR y Tipo*/ FilterProveedorId != 0 && FilterTipoId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por SPEC y Marca*/ FilterSpecId != 0 && FilterMarcaId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por SPEC y Tipo*/ FilterSpecId != 0 && FilterTipoId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*Filtrando por MARCA y Tipo*/ FilterMarcaId != 0 && FilterTipoId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por FECHA*/ FilterFechaVenta != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesdeSend) && q.FechaDePago <= Convert.ToDateTime(FechaHastaSend) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtranndo por USUARIO*/ FilterUsuariosVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por SPEC*/ FilterSpecId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por PROVEEDOR*/ FilterProveedorId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por MARCA*/ FilterMarcaId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) :
                //
                /*♥Filtrando por TIPO ~ TODO*/ FilterTipoId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) : q => q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta));
                #endregion

            }
            Close();
        }

        private async void VentaSelecteed()
        {
            ClasesCompartidas.CompraSelected.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CodigoDeVenta.Equals(codigoVenta));
            ClasesCompartidas.VentaSelected.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CodigoDeVenta.Equals(codigoVenta));
            //
            Close();
        }

        private async void DeudaSelected()
        {
            ClasesCompartidas.DeudaSelected.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(idcuenta) && q.CodigoDeVenta.Equals(codigoVenta));
            //
            Close();
        }

        private async void QuitarProductoCarrito()
        {
            try
            {
                new ModelsValidator().Validate(ventaDetalle);
                new ModelsValidator().Validate(producto);
                //
                if (delete == true)
                    unitOfWork.VentaDetalleRepository.Delete(ventaDetalle);
                else
                    unitOfWork.VentaDetalleRepository.Update(ventaDetalle);
                //
                unitOfWork.ProductoRepository.Update(producto);
                unitOfWork.Save();
                //
                ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                ClasesCompartidas.VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            Close();
        }

        private async void AgregarProductoCarrito()
        {
            try
            {
                new ModelsValidator().Validate(producto);
                new ModelsValidator().Validate(ventaDetalle);
                //
                unitOfWork.ProductoRepository.Update(producto);
                if (_repetido == false)
                    unitOfWork.VentaDetalleRepository.Add(ventaDetalle);
                else
                    unitOfWork.VentaDetalleRepository.Update(ventaDetalle);
                unitOfWork.Save();
                //
                ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                ClasesCompartidas.VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");
                //
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.InnerException?.Message);
                MessageBox.Show(ex.Message);
            }
            //
            Close();
        }
    }
}
