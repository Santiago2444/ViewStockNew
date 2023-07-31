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
using ViewStockNew.Repositories;

namespace ViewStockNew.Views
{
    public partial class MainMenuView : Form
    {
        public MainMenuView()
        {
            InitializeComponent();
            //
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.IcoBtnRealizarVenta, "Realizar Venta");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.IcoBtnCrearUsuario, "Crear Usuario");
            //
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.IcoBtnCuenta, "Lista de Cuentas");
            //
            System.Windows.Forms.ToolTip ToolTip4 = new System.Windows.Forms.ToolTip();
            ToolTip4.SetToolTip(this.IcoBtnRemito, "Realizar Remito");
            //
            System.Windows.Forms.ToolTip ToolTip5 = new System.Windows.Forms.ToolTip();
            ToolTip5.SetToolTip(this.IcoBtnProducto, "Crear Producto");
        }

        private void IcoItemProductos_Click(object sender, EventArgs e)
        {
            var dataValue = "productos";
            IUnitOfWork unitOfWork = new UnitOfWork();
            var masterDataView = new MasterDataView(unitOfWork, dataValue);
            masterDataView.Show();
        }

        private void IcoItemExit_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void IcoMenuItemProveedor_Click(object sender, EventArgs e)
        {

        }

        private void IcoMenuItemUser_Click(object sender, EventArgs e)
        {
            var dataValue = "users";
            IUnitOfWork unitOfWork = new UnitOfWork();
            var masterDataView = new MasterDataView(unitOfWork, dataValue);
            masterDataView.Show();
        }

        private void IcoMenuItemCuentas_Click(object sender, EventArgs e)
        {
            var dataValue = "cuentas";
            IUnitOfWork unitOfWork = new UnitOfWork();
            var masterDataView = new MasterDataView(unitOfWork, dataValue);
            masterDataView.Show();
        }

        private void RealizarVentaMenuItem_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var makaSaleView = new MakeSaleView(unitOfWork);
            makaSaleView.Show();
        }

        private void iconMenuItem2_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var salesMadeView = new SalesMadeView(unitOfWork);
            salesMadeView.Show();
        }

        private void MainMenuView_Activated(object sender, EventArgs e)
        {
            if (ClasesCompartidas.TipoUsuarioId == 1)
            {
                // ToolBar
                IcoItemVentas.Enabled = true;
                //
                IcoMenuItemUser.Enabled = true;
                //
                IcoItemProductos.Enabled = true;
                //
                IcoMenuItemCuentas.Enabled = true;
                //
                IcoMenuItemProveedor.Enabled = true;
                //
                // Acciones rápidas
                IcoBtnRealizarVenta.Enabled = true;
                //
                IcoBtnProducto.Enabled = true;
                //
                IcoBtnCrearUsuario.Enabled = true;
                //
                IcoBtnRemito.Enabled = true;
                //
                IcoBtnCuenta.Enabled = true;
                //
                IcoBtnTerminarTurno.Enabled = true;
            }
            else if (ClasesCompartidas.TipoUsuarioId == 2)
            {
                // ToolBar
                IcoItemVentas.Enabled = true;
                //
                IcoMenuItemUser.Enabled = false;
                //
                IcoItemProductos.Enabled = true;
                //
                IcoMenuItemCuentas.Enabled = true;
                //
                IcoMenuItemProveedor.Enabled = true;
                //
                // Acciones rápidas
                IcoBtnRealizarVenta.Enabled = true;
                //
                IcoBtnProducto.Enabled = true;
                //
                IcoBtnCrearUsuario.Enabled = false;
                //
                IcoBtnRemito.Enabled = true;
                //
                IcoBtnCuenta.Enabled = true;
                //
                IcoBtnTerminarTurno.Enabled = true;
            }
            else
            {
                // ToolBar
                IcoItemVentas.Enabled = false;
                //
                IcoMenuItemUser.Enabled = false;
                //
                IcoItemProductos.Enabled = false;
                //
                IcoMenuItemCuentas.Enabled = true;
                //
                IcoMenuItemProveedor.Enabled = false;
                //
                // Acciones rápidas
                IcoBtnRealizarVenta.Enabled = true;
                //
                IcoBtnProducto.Enabled = false;
                //
                IcoBtnCrearUsuario.Enabled = false;
                //
                IcoBtnRemito.Enabled = false;
                //
                IcoBtnCuenta.Enabled = true;
                //
                IcoBtnTerminarTurno.Enabled = true;
            }
            //
            this.FormClosing += delegate
            {
                Application.Exit();
            };
        }

        private void ProductosList_Click(object sender, EventArgs e)
        {
            var dataValue = "proveedores";
            IUnitOfWork unitOfWork = new UnitOfWork();
            var masterDataView = new MasterDataView(unitOfWork, dataValue);
            masterDataView.Show();
        }

        private void iconMenuItem4_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            bool editando = false;
            var createProovedorView = new CreateProveedorView(unitOfWork, editando);
            createProovedorView.ShowDialog();
        }

        private void MenuRealizarRemito_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var remitoView = new RemitoView(unitOfWork);
            remitoView.ShowDialog();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var makaSaleView = new MakeSaleView(unitOfWork);
            makaSaleView.Show();
        }

        private void IcoBtnRemito_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var remitoView = new RemitoView(unitOfWork);
            remitoView.ShowDialog();
        }

        private void IcoBtnProducto_Click(object sender, EventArgs e)
        {
            bool editando = false;
            IUnitOfWork unitOfWork = new UnitOfWork();
            //
            var createProductView = new CreateProductView(unitOfWork, editando);
            createProductView.ShowDialog();
        }

        private void IcoBtnCrearUsuario_Click(object sender, EventArgs e)
        {
            bool editando = false;
            IUnitOfWork unitOfWork = new UnitOfWork();
            //
            var createUserView = new CreateUserView(unitOfWork, editando);
            createUserView.ShowDialog();
        }

        private void MainMenuView_Load(object sender, EventArgs e)
        {

        }

        private void IcoBtnCuenta_Click(object sender, EventArgs e)
        {
            var dataValue = "cuentas";
            IUnitOfWork unitOfWork = new UnitOfWork();
            var masterDataView = new MasterDataView(unitOfWork, dataValue);
            masterDataView.Show();
        }
    }
}
