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
using System.Windows.Media.Imaging;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;
using ViewStockNew.Utils;

namespace ViewStockNew.Views
{
    public partial class LoadingView : Form
    {
        private IUnitOfWork unitOfWork;
        private int IdCuenta;
        bool DataAccountComplete = false;
        private Producto producto;
        private VentaDetalle ventaDetalle;
        private int rowsCount;
        private int rowsProductCount;
        private int RunMethod = 0;
        public LoadingView()
        {
            InitializeComponent();
        }

        public LoadingView(int rowsCount, int rowsProductCount, IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            //      
            this.rowsCount = rowsCount;
            this.rowsProductCount = rowsProductCount;
            this.unitOfWork = unitOfWork;
            //
            CancelarVenta();
        }

        private async void CancelarVenta()
        {
            // Variables Importantes
            int IdProducto = 0;
            //
            int IdVentaDetalle = 0;
            //
            int StockDevuelto = 0;
            //
            int TipoId = 0;
            int MarcaId = 0;
            string Detalles = "-";
            int SpecId = 0;
            //
            var ventaDetalleList = ClasesCompartidas.VentaDetalle;
            var productoList = ClasesCompartidas.productosList;
            //
            foreach (VentaDetalle item in ventaDetalleList)
            {
                if (Convert.ToString(item.Pagado) == "Carrito")
                {
                    IdVentaDetalle = item.Id;
                    //
                    TipoId = (int)item.TipoProductoId;
                    MarcaId = (int)item.MarcaId;
                    SpecId = (int)item.SPECId;
                    Detalles = Convert.ToString(item.Detalles);
                    //
                    StockDevuelto = (int)item.Cantidad;
                }
                //
                if (IdVentaDetalle != 0)
                {
                    var ventaDetalle = unitOfWork.VentaDetalleRepository.GetByID(IdVentaDetalle);
                    //
                    unitOfWork.VentaDetalleRepository.Delete(ventaDetalle);
                    unitOfWork.Save();
                    IdVentaDetalle = 0;
                }
                //
                foreach (Producto producto in productoList)
                {
                    if (Convert.ToInt32(TipoId) == Convert.ToInt32(producto.TipoProductoId) && Convert.ToInt32(MarcaId) == Convert.ToInt32(producto.MarcaId) && Convert.ToInt32(SpecId) == Convert.ToInt32(producto.SPECId) && Convert.ToString(Detalles) == Convert.ToString(producto.Detalles))
                    {
                        if (Convert.ToInt32(producto.Carrito) > 0)
                        {
                            IdProducto = producto.Id;
                        }
                        //
                        if (IdProducto != 0)
                        {
                            var productoUpdate = unitOfWork.ProductoRepository.GetByID(IdProducto);
                            productoUpdate.Stock = producto.Stock + StockDevuelto;
                            productoUpdate.Carrito = 0;
                            //
                            try
                            {
                                new ModelsValidator().Validate(productoUpdate);
                                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                //if (respuesta == DialogResult.Yes)
                                //{
                                unitOfWork.ProductoRepository.Update(productoUpdate);
                                unitOfWork.Save();
                                IdProducto = 0;
                            }
                            catch (Exception ex)
                            {
                                Debug.Print(ex.Message);
                                Debug.Print(ex.InnerException?.Message);
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
            //
            ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
            ClasesCompartidas.VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");
            //
            Close();
        }

        public LoadingView(IUnitOfWork unitOfWork, int idCuenta)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.IdCuenta = idCuenta;
            //
            GetDataAccount();
        }

        private async void GetDataAccount()
        {
            ClasesCompartidas.ComprasList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado" && q.CuentaId.Equals(IdCuenta));
            ClasesCompartidas.ProductosList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta));
            ClasesCompartidas.PagosList.DataSource = await unitOfWork.PagoRepository.GetAllAsync(include: q => q.Include(q => q.Cuenta).Include(q => q.Usuario), filter: q => q.CuentaId.Equals(IdCuenta));
            //
            ClasesCompartidas.ComprasDeudaList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(IdCuenta));
            ClasesCompartidas.ProductosDeudaList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(IdCuenta));
            // PagosDeudaList.DataSource PENDIENTE
            DataAccountComplete = true;
            //
            if (DataAccountComplete == true)
            {
                Close();
            }
        }

        private void LoadingView_Activated(object sender, EventArgs e)
        {

        }

        private void LoadingView_Load(object sender, EventArgs e)
        {

        }
    }
}
