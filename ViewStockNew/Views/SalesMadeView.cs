using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;

namespace ViewStockNew.Views
{
    public partial class SalesMadeView : Form
    {
        IUnitOfWork unitOfWork;
        //
        BindingSource SearchFilterVentas = new BindingSource();
        BindingSource SearchFilterProductos = new BindingSource();
        //
        #region VariablesParaElFiltrado
        private int FilterCuentaVenta = 0;
        private int FilterUsuariosVenta = 0;
        private int FilterFechaVenta = 0;
        private int FilterTipoId = 0;
        private int FilterMarcaId = 0;
        private int FilterSpecId = 0;
        private int FilterProveedorId = 0;
        #endregion

        public SalesMadeView(Interfaces.IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            //
            GetGridsDatas();
            GetCombosDataVentas();
        }

        private void GetCombosDataVentas()
        {
            // CargaDeComboUsuarios
            ComboUsuarios.DisplayMember = "Nombre";
            ComboUsuarios.ValueMember = "Id";
            ComboUsuarios.DataSource = ClasesCompartidas.usuariosList;
            // CargaDeComboCuentas
            ComboCuentas.DisplayMember = "Nombre";
            ComboCuentas.ValueMember = "Id";
            ComboCuentas.DataSource = ClasesCompartidas.cuentasList;
            // CargaDeComboTipos
            ComboTipoProducto.DisplayMember = "Nombre";
            ComboTipoProducto.ValueMember = "Id";
            ComboTipoProducto.DataSource = ClasesCompartidas.tiposProductosList;
            // CargaDeMarcas
            ComboMarcas.DisplayMember = "Nombre";
            ComboMarcas.ValueMember = "Id";
            ComboMarcas.DataSource = ClasesCompartidas.marcasList;
            // CargaDeSpec's
            ComboSpec.DisplayMember = "Nombre";
            ComboSpec.ValueMember = "Id";
            ComboSpec.DataSource = ClasesCompartidas.specsList;
            // CargaDeProveedor
            ComboProveedor.DisplayMember = "Nombre";
            ComboProveedor.ValueMember = "Id";
            ComboProveedor.DataSource = ClasesCompartidas.proveedoresList;

        }

        private void GetGridsDatas()
        {
            GridVentas.DataSource = ClasesCompartidas.ventasList;
            //
            GridDetalles.DataSource = ClasesCompartidas.ventaDetallesList;
        }

        private void SalesMadeView_Load(object sender, EventArgs e)
        {

        }

        private void GridVentas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn columna in GridVentas.Columns)
            {
                #region VisibilidadDeColumnas
                if (columna.Name == "CuentaId")
                    columna.Visible = false;
                if (columna.Name == "UsuarioId")
                    columna.Visible = false;
                if (columna.Name == "Estado")
                    columna.Visible = false;
                #endregion
                //
                #region AjusteDeColumnas
                if (columna.Name == "Id")
                    columna.Width = 40;
                //
                if (columna.Name == "Importe")
                {
                    columna.Width = 90;
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                }
                //
                if (columna.Name == "Dinero")
                {
                    columna.Width = 90;
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                }
                //
                if (columna.Name == "Vuelto")
                {
                    columna.Width = 90;
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                }
                //
                if (columna.Name == "Articulos")
                    columna.Width = 80;
                //
                if (columna.Name == "Codigo")
                {
                    columna.Width = 55;
                    columna.HeaderText = "Código";
                }
                //
                if (columna.Name == "Usuario")
                    columna.Width = 90;
                //
                #endregion
            }
        }

        private void GridDetalles_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridDetalles.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn columnaD in GridDetalles.Columns)
                {
                    #region VisibilidadDeColumnas
                    if (columnaD.Name == "CuentaId")
                        columnaD.Visible = false;
                    if (columnaD.Name == "UsuarioId")
                        columnaD.Visible = false;
                    if (columnaD.Name == "Pagado")
                        columnaD.Visible = false;
                    if (columnaD.Name == "MarcaId")
                        columnaD.Visible = false;
                    if (columnaD.Name == "TipoProductoId")
                        columnaD.Visible = false;
                    if (columnaD.Name == "SPECId")
                        columnaD.Visible = false;
                    if (columnaD.Name == "ProveedorId")
                        columnaD.Visible = false;
                    if (columnaD.Name == "ProveedorId")
                        columnaD.Visible = false;
                    if (columnaD.Name == "Imagen")
                        columnaD.Visible = false;
                    #endregion
                    //
                    #region AjusteDeColumnas
                    if (columnaD.Name == "Id")
                        columnaD.Width = 40;
                    //
                    if (columnaD.Name == "PVP")
                    {
                        columnaD.Width = 90;
                        columnaD.DefaultCellStyle.Format = "$" + "0.00";
                        columnaD.HeaderText = "Precio";
                    }
                    //
                    if (columnaD.Name == "Cantidad")
                        columnaD.Width = 70;
                    //
                    if (columnaD.Name == "FechaDePago")
                        columnaD.HeaderText = "Fecha";
                    //
                    if (columnaD.Name == "TipoProducto")
                        columnaD.HeaderText = "Tipo";
                    //
                    if (columnaD.Name == "CodigoDeVenta")
                    {
                        columnaD.HeaderText = "Código";
                        columnaD.Width = 65;
                    }
                    #endregion
                }
            };
        }

        private async void BtnSearchVentas_Click(object sender, EventArgs e)
        {
            string TxtBusqueda = TxtBuscarVentas.Text;
            //
            SearchFilterVentas.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Cuenta.Nombre.Contains(TxtBusqueda) && q.Estado == "Pagado" || q.Usuario.Nombre.Contains(TxtBusqueda) && q.Estado == "Pagado");
            GridVentas.DataSource = SearchFilterVentas;
        }

        private void TxtBuscarVentas_TextChanged(object sender, EventArgs e)
        {
            if (TxtBuscarVentas.Text.Length < 1)
            {
                GridVentas.DataSource = ClasesCompartidas.ventasList;
            }
        }

        private async void BtnSearchProductos_Click(object sender, EventArgs e)
        {
            string TxtBusqueda = TxtBuscarProductos.Text;
            //
            SearchFilterProductos.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.TipoProducto.Nombre.Contains(TxtBusqueda) && q.Pagado == "Si" || q.Marca.Nombre.Contains(TxtBusqueda) && q.Pagado == "Si" || q.Detalles.Contains(TxtBusqueda) && q.Pagado == "Si" || q.SPEC.Nombre.Contains(TxtBusqueda) && q.Pagado == "Si" || q.Proveedor.Nombre.Contains(TxtBusqueda) && q.Pagado == "Si" || q.Cuenta.Nombre.Contains(TxtBusqueda) && q.Pagado == "Si" || q.Usuario.Nombre.Contains(TxtBusqueda) && q.Pagado == "Si");
            GridDetalles.DataSource = SearchFilterProductos;
        }

        private void TxtBuscarProductos_TextChanged(object sender, EventArgs e)
        {
            if (TxtBuscarProductos.Text.Length < 1)
            {
                GridDetalles.DataSource = ClasesCompartidas.ventaDetallesList;
            }
        }

        private void CheckUsuarios_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckUsuarios.Checked == true)
            {
                ComboUsuarios.Enabled = true;
            }
            else if (CheckUsuarios.Checked == false)
            {
                ComboUsuarios.Enabled = false;
            }
        }

        private void CheckCuentas_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckCuentas.Checked == true)
            {
                ComboCuentas.Enabled = true;
            }
            else if (CheckCuentas.Checked == false)
            {
                ComboCuentas.Enabled = false;
            }
        }

        private void CheckFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckFecha.Enabled == true)
            {
                FechaDesde.Enabled = true;
                FechaHasta.Enabled = true;
            }
            else if (CheckFecha.Enabled == false)
            {
                FechaDesde.Enabled = false;
                FechaHasta.Enabled = false;
            }
        }

        private async void BtnFiltrarVentas_Click(object sender, EventArgs e)
        {

            // Elementos por los cuales filtrar
            // Cuentas
            if (CheckCuentas.Checked == true)
            {
                FilterCuentaVenta = (int)ComboCuentas.SelectedValue;
            }
            // Usuarios
            if (CheckUsuarios.Checked == true)
            {
                FilterUsuariosVenta = (int)ComboUsuarios.SelectedValue;
            }
            // Fecha
            if (CheckFecha.Checked == true)
            {
                FilterFechaVenta = 1;
            }
            // Tipo de Productos
            if (CheckCboTipo.Checked == true)
            {
                FilterTipoId = (int)ComboTipoProducto.SelectedValue;
            }
            // Marcas 
            if (CheckCboMarca.Checked == true)
            {
                FilterMarcaId = (int)ComboMarcas.SelectedValue;
            }
            // Spec's
            if (CheckCboSPEC.Checked == true)
            {
                FilterSpecId = (int)ComboSpec.SelectedValue;
            }
            // Proveedor
            if (CheckCboProveedor.Checked == true)
            {
                FilterProveedorId = (int)(ComboProveedor.SelectedValue);
            }
            //
            #region FiltradorVentas
            if (CheckVentasFilter.Checked == true)
            {
                #region Código
                SearchFilterVentas.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter:
                /*♥Filtrado por CUENTA, USUARIO y FECHA*/FilterCuentaVenta != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Fecha >= Convert.ToDateTime(FechaDesde.Text) && q.Fecha <= Convert.ToDateTime(FechaHasta.Text) && q.Estado == "Pagado" :
                /*♥Filtrado por FECHA y Cuenta*/FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.Fecha >= Convert.ToDateTime(FechaDesde.Text) && q.Fecha <= Convert.ToDateTime(FechaHasta.Text) && q.Estado == "Pagado" :
                /*Filtrado por FECHA y Usuario*/ FilterFechaVenta != 0 && FilterUsuariosVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.Fecha >= Convert.ToDateTime(FechaDesde.Text) && q.Fecha <= Convert.ToDateTime(FechaHasta.Text) && q.Estado == "Pagado" :
                /*♥Filtrado por CUENTA y Usuario*/ FilterCuentaVenta != 0 && FilterUsuariosVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Estado == "Pagado" :
                /*♥Filtrado por FECHA*/ FilterFechaVenta != 0 ? q => q.Fecha >= Convert.ToDateTime(FechaDesde.Text) && q.Fecha <= Convert.ToDateTime(FechaHasta.Text) && q.Estado == "Pagado" :
                /*♥Filtrado por CUENTA*/ FilterCuentaVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.Estado == "Pagado" :
                /*♥Filtrado por USUARIO*/ FilterUsuariosVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) : q => q.Estado == "Pagado");
                #endregion
                //
                GridVentas.DataSource = SearchFilterVentas;
            }
            #endregion
            //
            #region FiltradorProductos
            if (CheckProductosFilter.Checked == true)
            {
                #region Código
                SearchFilterProductos.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter:
                /*♥Con Todos los Valores*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin Rango de Fecha*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin Usuario*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin Tipo*/  FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin Cuenta*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*♥Sin RDF y Usuario*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin RDF y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin RDF Y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin RDF y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin RDF y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin RDF y Cuenta*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" :
                //
                /*♥Sin USUARIO y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin USUARIO y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin USUARIO y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin USUARIO y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin USUARIO y Cuenta*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                // 
                /*♥Sin PROVEEDOR y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin PROVEEDOR y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin PROVEEDOR y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin PROVEEDOR y Cuenta*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterTipoId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Sin SPEC y Marca*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin SPEC y Tipo*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin SPEC y Cuenta*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterTipoId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Sin MARCA y TIPO*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*♥Sin MARCA y CUENTA*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterTipoId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Sin CUENTA y TIPO*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterMarcaId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" :
                //
                /*♥Sin FECHA, USUARIO y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin FECHA, USUARIO y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin FECHA, USUARIO y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin FECHA, USUARIO y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin FECHA, USUARIO y Cuenta*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterTipoId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Sin USUARIO, PROVEEDOR y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin USUARIO, PROVEEDOR y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin USUARIO, PROVEEDOR y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin USUARIO, PROVEEDOR y Cuenta*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 && FilterTipoId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Sin PROVEEDOR, SPEC y Fecha*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin PROVEEDOR, SPEC y Marca*/ FilterTipoId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin PROVEEDOR, SPEC y Tipo*/ FilterMarcaId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin PROVEEDOR, SPEC y Cuenta*/ FilterMarcaId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterTipoId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Sin SPEC, MARCA y Fecha*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin SPEC, MARCA y Usuario*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin SPEC, MARCA y Tipo*/  FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin SPEC, MARCA y Cuenta*/  FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterTipoId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Sin MARCA, TIPO y Fecha*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin MARCA, TIPO y Usuario*/  FilterSpecId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin MARCA, TIPO y Proveedor*/ FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin MARCA, TIPO y Cuenta*/ FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 && FilterProveedorId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" :
                //
                /*♥Sin TIPO, FECHA, y Proveedor*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin TIPO, FECHA y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin TIPO, USUARIO y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin TIPO, USUARIO y Cuenta*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 && FilterSpecId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.SPECId.Equals(FilterSpecId) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" :
                //
                /*Sin TIPO, MARCA y CUENTA*/ FilterCuentaVenta != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 && FilterSpecId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.SPECId.Equals(FilterSpecId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                // A VER QUE SALE :(
                //
                //
                /*♥Sin FECHA, CUENTA, USUARIO y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" :
                //
                /*Sin FECHA, CUENTA, USUARIO y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" :
                //
                /*Sin FECHA, CUENTA, USUARIO y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" :
                //
                /*Sin FECHA, CUENTA, USUARIO y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" :
                //
                /*♥Sin CUENTA, USUARIO, PROVEEDOR y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*Sin CUENTA, USUARIO, PROVEEDOR y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*Sin CUENTA, USUARIO, PROVEEDOR y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*♥Sin USUARIO, PROVEEDOR, SPEC y Fecha*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Sin USUARIO, PROVEEDOR, SPEC y Marca*/ FilterTipoId != 0 && FilterCuentaVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.CuentaId.Equals(FilterCuentaVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*Sin USUARIO, PROVEEDOR, SPEC y Tipo*/ FilterMarcaId != 0 && FilterCuentaVenta != 0 && FilterFechaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.CuentaId.Equals(FilterCuentaVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*♥Sin PROVEEDOR, SPEC, MARCA y Fecha*/ FilterTipoId != 0 && FilterCuentaVenta != 0 && FilterUsuariosVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.CuentaId.Equals(FilterCuentaVenta) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" :
                //
                /*Sin PROVEEDOR, SPEC, MARCA y Usuario*/ FilterTipoId != 0 && FilterCuentaVenta != 0 && FilterFechaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.CuentaId.Equals(FilterCuentaVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*Sin PROVEEDOR, SPEC, MARCA y Tipo*/  FilterCuentaVenta != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*♥Sin SPEC, MARCA, TIPO y Fecha*/ FilterCuentaVenta != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" :
                //
                /*Sin SPEC, MARCA, TIPO y Usuario*/  FilterCuentaVenta != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.ProveedorId.Equals(FilterProveedorId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*Sin SPEC, MARCA, TIPO y Proveedor*/ FilterCuentaVenta != 0 && FilterUsuariosVenta != 0 && FilterFechaVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*♥Sin MARCA, TIPO, FECHA, y Proveedor*/ FilterCuentaVenta != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" :
                //
                /*Sin MARCA, TIPO, FECHA y Spec*/ FilterCuentaVenta != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" :
                //
                /*Sin MARCA, TIPO, USUARIO y Spec*/ FilterCuentaVenta != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.CuentaId.Equals(FilterCuentaVenta) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*♥Sin CUENTA, TIPO, FECHA, y Proveedor*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuariosVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" :
                //
                /*Sin CUENTA, TIPO, FECHA y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuariosVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" :
                //
                /*Sin CUENTA, TIPO, USUARIO y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFechaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.MarcaId.Equals(FilterMarcaId) && q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*♥Filtrando FECHA y Usuario*/ FilterFechaVenta != 0 && FilterUsuariosVenta != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" :
                //
                /*Filtrando FECHA y Cuenta*/ FilterFechaVenta != 0 && FilterCuentaVenta != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Filtrando FECHA y Proveedor*/ FilterFechaVenta != 0 && FilterProveedorId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" :
                //
                /*Filtrando FECHA y Spec*/ FilterFechaVenta != 0 && FilterSpecId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" :
                //
                /*Filtrando FECHA y Marca*/ FilterFechaVenta != 0 && FilterMarcaId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" :
                //
                /*Filtrando FECHA y Tipo*/ FilterFechaVenta != 0 && FilterTipoId != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Filtrando por USUARIO y Proveedor*/ FilterUsuariosVenta != 0 && FilterProveedorId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" :
                //
                /*Filtrando por USUARIO y Cuenta*/ FilterUsuariosVenta != 0 && FilterCuentaVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Filtrando por USUARIO y Spec*/ FilterUsuariosVenta != 0 && FilterSpecId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" :
                //
                /*Filtrando por USUARIO y Marca*/ FilterUsuariosVenta != 0 && FilterMarcaId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" :
                //
                /*Filtrando por USUARIO y Tipo*/ FilterUsuariosVenta != 0 && FilterTipoId != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Filtrando por PROVEEDOR y Spec*/ FilterProveedorId != 0 && FilterSpecId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" :
                //
                /*Filtrando por PROVEEDOR y Marca*/ FilterProveedorId != 0 && FilterMarcaId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*Filtrando por PROVEEDOR y Cuenta*/ FilterProveedorId != 0 && FilterCuentaVenta != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Filtrando por PROVEEDOR y Tipo*/ FilterProveedorId != 0 && FilterTipoId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*♥Filtrando por SPEC y Marca*/ FilterSpecId != 0 && FilterMarcaId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" :
                //
                /*Filtrando por SPEC y Tipo*/ FilterSpecId != 0 && FilterTipoId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*Filtrando por SPEC y Cuenta*/ FilterSpecId != 0 && FilterCuentaVenta != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Filtrando por MARCA y Tipo*/ FilterMarcaId != 0 && FilterTipoId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" :
                //
                /*Filtrando por MARCA y Cuenta*/ FilterMarcaId != 0 && FilterCuentaVenta != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*Filtrando por TIPO y Cuenta*/ FilterTipoId != 0 && FilterCuentaVenta != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*♥Filtrando por FECHA*/ FilterFechaVenta != 0 ? q => q.FechaDePago >= Convert.ToDateTime(FechaDesde.Text) && q.FechaDePago <= Convert.ToDateTime(FechaHasta.Text) && q.Pagado == "Si" :
                //
                /*♥Filtrando por USUARIO*/ FilterUsuariosVenta != 0 ? q => q.UsuarioId.Equals(FilterUsuariosVenta) && q.Pagado == "Si" :
                //
                /*♥Filtrando por CUENTA*/ FilterCuentaVenta != 0 ? q => q.CuentaId.Equals(FilterCuentaVenta) && q.Pagado == "Si" :
                //
                /*♥Filtrando por SPEC*/ FilterSpecId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.Pagado == "Si" :
                //
                /*♥Filtrando por PROVEEDOR*/ FilterProveedorId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.Pagado == "Si" :
                //
                /*♥Filtrando por MARCA*/ FilterMarcaId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.Pagado == "Si" :
                //
                /*♥Filtrando por TIPO ~ TODO*/ FilterTipoId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.Pagado == "Si" : q => q.Pagado == "Si");
                #endregion
                //
                GridDetalles.DataSource = SearchFilterProductos;
            }
            #endregion
        }

        private void GridVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int CodigoVenta = Convert.ToInt32(GridVentas.CurrentRow.Cells["Codigo"].Value);
            //
            var miniloading = new MiniLoading(CodigoVenta, unitOfWork);
            miniloading.ShowDialog();
            //
            GridDetalles.DataSource = ClasesCompartidas.VentaSelected;
        }

        private async void GetGridsDatas(int codigoVenta)
        {
            int CodigoVenta = codigoVenta;
            // Se muestran los detalles, es decir los productos que componen la Venta seleccionada
            SearchFilterProductos.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CodigoDeVenta.Equals(CodigoVenta));
        }

        private void BtnRefrescarVentas_Click(object sender, EventArgs e)
        {
            if (CheckVentasFilter.Checked == true)
            {
                TxtBuscarVentas.Text = " ";
                GridVentas.DataSource = ClasesCompartidas.ventasList;
            }
            //
            if (CheckProductosFilter.Checked == true)
            {
                TxtBuscarProductos.Text = " ";
                GridDetalles.DataSource = ClasesCompartidas.ventaDetallesList;
            }
            GetCombosDataVentas();
            //
        }

        private void CheckCboTipo_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckCboTipo.Checked == true)
            {
                ComboTipoProducto.Enabled = true;
            }
            else if (CheckCboTipo.Checked == false)
            {
                ComboTipoProducto.Enabled = false;
            }
        }

        private void CheckCboSPEC_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckCboSPEC.Checked == true)
            {
                ComboSpec.Enabled = true;
            }
            else if (CheckCboSPEC.Checked == false)
            {
                ComboSpec.Enabled = false;
            }
        }

        private void CheckCboMarca_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckCboMarca.Checked == true)
            {
                ComboMarcas.Enabled = true;
            }
            else if (CheckCboMarca.Checked == false)
            {
                ComboMarcas.Enabled = false;
            }
        }

        private void CheckProveedoresProductos_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckCboProveedor.Checked == true)
            {
                ComboProveedor.Enabled = true;
            }
            else if (CheckCboProveedor.Checked == false)
            {
                ComboProveedor.Enabled = false;
            }
        }

        private void CheckVentasFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckVentasFilter.Checked)
            {
                CheckFecha.Enabled = true;
                CheckCuentas.Enabled = true;
                CheckUsuarios.Enabled = true;
            }
            //
            if (CheckProductosFilter.Checked == true || CheckVentasFilter.Checked == true)
            {
                BtnFiltrarVentas.Enabled = true;
            }
            else if (CheckProductosFilter.Checked == false && CheckVentasFilter.Checked == false)
            {
                BtnFiltrarVentas.Enabled = false;
                //
                CheckFecha.Enabled = false;
                CheckCuentas.Enabled = false;
                CheckUsuarios.Enabled = false;
                //
                CheckCboTipo.Enabled = false;
                CheckCboMarca.Enabled = false;
                CheckCboSPEC.Enabled = false;
                CheckCboProveedor.Enabled = false;
            }
        }

        private void CheckProductosFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckProductosFilter.Checked == true)
            {
                CheckFecha.Enabled = true;
                CheckCuentas.Enabled = true;
                CheckUsuarios.Enabled = true;
                //
                CheckCboTipo.Enabled = true;
                CheckCboMarca.Enabled = true;
                CheckCboSPEC.Enabled = true;
                CheckCboProveedor.Enabled = true;
            }
            //
            if (CheckProductosFilter.Checked == true || CheckVentasFilter.Checked == true)
            {
                BtnFiltrarVentas.Enabled = true;
            }
            else if (CheckProductosFilter.Checked == false && CheckVentasFilter.Checked == false)
            {
                BtnFiltrarVentas.Enabled = false;
                //
                CheckFecha.Enabled = false;
                CheckCuentas.Enabled = false;
                CheckUsuarios.Enabled = false;
                //
                CheckCboTipo.Enabled = true;
                CheckCboMarca.Enabled = true;
                CheckCboSPEC.Enabled = true;
                CheckCboProveedor.Enabled = false;
            }
        }
    }
}
