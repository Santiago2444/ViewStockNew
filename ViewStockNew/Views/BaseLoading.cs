using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;

namespace ViewStockNew.Views
{
    public partial class BaseLoading : Form
    {
        private IUnitOfWork unitOfWork;
        private int IdCuenta;
        private int rowsCount;
        private int rowsProductCount;
        private int cuentaId;
        private int articulos;
        private decimal vuelto;
        private decimal dinero;
        private decimal importe;
        private VentaDetalle? ventaDetalle;
        private Producto? producto;
        private bool repetido;
        private RemitoDetalle remitoDetalle;

        public BaseLoading()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
        }

        public BaseLoading(IUnitOfWork unitOfWork, int idCuenta)
        {   
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            //
            this.unitOfWork = unitOfWork;
            this.IdCuenta = idCuenta;
            //
            OpenAcccountLoading();
        }

        public BaseLoading(VentaDetalle? ventaDetalle, Producto? producto, IUnitOfWork unitOfWork, bool repetido)
        {
            InitializeComponent();
            //
            this.ventaDetalle = ventaDetalle;
            this.producto = producto;
            this.unitOfWork = unitOfWork;
            this.repetido = repetido;
            //
            AgregarProductoCarrito();
        }

        private void AgregarProductoCarrito()
        {
            var miniloading = new MiniLoading(ventaDetalle, producto, unitOfWork, repetido);
            miniloading.ShowDialog();
            //
            Close();
        }

        public BaseLoading(int rowsCount, int rowsProductCount, IUnitOfWork unitOfWork, int cuentaId, int articulos, decimal vuelto, decimal dinero, decimal importe)
        {
            InitializeComponent();
            //
            this.rowsCount = rowsCount;
            this.rowsProductCount = rowsProductCount;
            this.unitOfWork = unitOfWork;
            this.cuentaId = cuentaId;
            this.articulos = articulos;
            this.vuelto = vuelto;
            this.dinero = dinero;
            this.importe = importe;
            //
            GuardarVenta();
        }

        public BaseLoading(RemitoDetalle remitoDetalle, IUnitOfWork unitOfWork, bool repetido)
        {
            this.remitoDetalle = remitoDetalle;
            this.unitOfWork = unitOfWork;
            this.repetido = repetido;
        }

        private void GuardarVenta()
        {
            //
            Close();
        }

        private void OpenAcccountLoading()
        {
            var loadingView = new LoadingView(unitOfWork, IdCuenta);
            loadingView.ShowDialog();
            //
            var accountView = new AccountsView(unitOfWork, IdCuenta);
            accountView.ShowDialog();
            //
            Close();
        }
    }
}
