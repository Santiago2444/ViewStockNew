using FontAwesome.Sharp;
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
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;
using ViewStockNew.Repositories;
using ViewStockNew.Utils;
using ViewStockNew.ViewReport;

namespace ViewStockNew.Views
{
    public partial class MasterDataView : Form
    {
        IUnitOfWork unitOfWork;
        //
        BindingSource productos = new BindingSource();
        BindingSource proveedores = new BindingSource();
        BindingSource cuentas = new BindingSource();
        BindingSource usuarios = new BindingSource();
        //
        #region Variables
        string DataValue;
        private int DatosCargados;
        bool combosCargados = false;
        private int? idSeleccionado = null;
        // Variables para el filtrado
        private int FilterTipoId = 0;
        private int FilterMarcaId = 0;
        private int FilterSpecId = 0;
        private int FilterProveedorId = 0;
        private int FilterUsuarioId = 0;
        private int FilterTipoUsuarioId = 0;
        private int FilterProvinciaId = 0;
        private int FilterLocalidadId = 0;
        private int FilterFecha = 0;
        #endregion
        //
        BindingSource listData = new BindingSource();
        BindingSource Filter = new BindingSource();
        private bool _filter;

        //
        public MasterDataView(IUnitOfWork unitOfWork, string dataValue)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            this.DataValue = dataValue;
            //GetAll();
            GetComboData();
            GridData.DataSource = listData;
            //
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.BtnNuevo, "Agregar");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.BtnEditar, "Editar");
            //
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.BtnDetalles, "Detalles");
            //
            System.Windows.Forms.ToolTip ToolTip4 = new System.Windows.Forms.ToolTip();
            ToolTip4.SetToolTip(this.BtnRemito, "Realizar Remito");
            //
            System.Windows.Forms.ToolTip ToolTip5 = new System.Windows.Forms.ToolTip();
            ToolTip5.SetToolTip(this.BtnDis, "Deshabilitar");
            //
            System.Windows.Forms.ToolTip ToolTip6 = new System.Windows.Forms.ToolTip();
            ToolTip6.SetToolTip(this.BtnEna, "Habilitar");
            //
            System.Windows.Forms.ToolTip ToolTip7 = new System.Windows.Forms.ToolTip();
            ToolTip7.SetToolTip(this.BtnEliminar, "Eliminar");
            //
            System.Windows.Forms.ToolTip ToolTip8 = new System.Windows.Forms.ToolTip();
            ToolTip8.SetToolTip(this.BtnImprimir, "Imprimir");
            //
            System.Windows.Forms.ToolTip ToolTip9 = new System.Windows.Forms.ToolTip();
            ToolTip9.SetToolTip(this.IcoBtnCalcular, "Calcular PVP");
            //
            System.Windows.Forms.ToolTip ToolTip10 = new System.Windows.Forms.ToolTip();
            ToolTip10.SetToolTip(this.BtnCalcularBultoPVP, "Calcular precio por bulto");
            //
            System.Windows.Forms.ToolTip ToolTip11 = new System.Windows.Forms.ToolTip();
            ToolTip11.SetToolTip(this.BtnGuardarPorcentaje, "Guardar % de Ganancia");
            //
            System.Windows.Forms.ToolTip ToolTip12 = new System.Windows.Forms.ToolTip();
            ToolTip12.SetToolTip(this.BtnGuardarDescuento, "Guardar % de Descuento");
            //
            System.Windows.Forms.ToolTip ToolTip13 = new System.Windows.Forms.ToolTip();
            ToolTip13.SetToolTip(this.BtnRestar, "Restar Stock");
            //
            System.Windows.Forms.ToolTip ToolTip14 = new System.Windows.Forms.ToolTip();
            ToolTip14.SetToolTip(this.BtnRestar, "Sumar Stock");
            //
            System.Windows.Forms.ToolTip ToolTip15 = new System.Windows.Forms.ToolTip();
            ToolTip15.SetToolTip(this.BtnFiltro, "Filtrar");
            //
            System.Windows.Forms.ToolTip ToolTip16 = new System.Windows.Forms.ToolTip();
            ToolTip16.SetToolTip(this.BtnBuscar, "Buscar");
            //
            System.Windows.Forms.ToolTip ToolTip17 = new System.Windows.Forms.ToolTip();
            ToolTip17.SetToolTip(this.BtnRecargar, "Recargar");

        }

        private void GetPorcentaje()
        {
            var porcentajeGanancia = unitOfWork.PorcentajeGananciaRepository.GetByID(1);
            TxtPVP.Text = porcentajeGanancia.Porcentaje.ToString("0.00");
            //
            var porcentajeDescuento = unitOfWork.PorcentajeGananciaRepository.GetByID(2);
            TxtDescuento.Text = porcentajeDescuento.Porcentaje.ToString("0.00");
        }

        //private async void CargarComboTipoProducto()
        //{
        //    var tipoProducto = await unitOfWork.TipoProductoRepository.GetAllAsync();
        //    ComboTipo.DisplayMember = "Nombre";
        //    ComboTipo.ValueMember = "Id";
        //    ComboTipo.DataSource = tipoProducto.ToList();

        //    AutoCompleteStringCollection autoCompletado = new AutoCompleteStringCollection();
        //    foreach (TipoProducto item in tipoProducto)
        //    {
        //        //agrega el texto encontrado en la segunda columna del datatable
        //        //si el datatable no tiene 2da columna va a dar error
        //        autoCompletado.Add(item.Nombre.ToString());
        //    }
        //    ComboTipo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    ComboTipo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    ComboTipo.AutoCompleteCustomSource = autoCompletado;
        //    CargarComboMarca();

        //}
        //private async void CargarComboMarca()
        //{
        //    var marca = await unitOfWork.MarcaRepository.GetAllAsync();
        //    ComboMarca.DisplayMember = "Nombre";
        //    ComboMarca.ValueMember = "Id";
        //    ComboMarca.DataSource = marca.ToList();

        //    AutoCompleteStringCollection autoCompletado = new AutoCompleteStringCollection();
        //    foreach (Marca item in marca)
        //    {
        //        //agrega el texto encontrado en la segunda columna del datatable
        //        //si el datatable no tiene 2da columna va a dar error
        //        autoCompletado.Add(item.Nombre.ToString());
        //    }
        //    ComboMarca.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    ComboMarca.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    ComboMarca.AutoCompleteCustomSource = autoCompletado;
        //    CargarComboProveedor();

        //}
        //private async void CargarComboProveedor()
        //{
        //    var proveedor = await unitOfWork.ProveedorRepository.GetAllAsync();
        //    ComboProveedor.DisplayMember = "Nombre";
        //    ComboProveedor.ValueMember = "Id";
        //    ComboProveedor.DataSource = proveedor.ToList();

        //    AutoCompleteStringCollection autoCompletado = new AutoCompleteStringCollection();
        //    foreach (Proveedor item in proveedor)
        //    {
        //        //agrega el texto encontrado en la segunda columna del datatable
        //        //si el datatable no tiene 2da columna va a dar error
        //        autoCompletado.Add(item.Nombre.ToString());
        //    }
        //    ComboProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    ComboProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    ComboProveedor.AutoCompleteCustomSource = autoCompletado;
        //    CargarComboEspecificacion();
        //}
        //private async void CargarComboEspecificacion()
        //{
        //    var spec = await unitOfWork.SPECRepository.GetAllAsync();
        //    ComboSpec.DisplayMember = "Nombre";
        //    ComboSpec.ValueMember = "Id";
        //    ComboSpec.DataSource = spec.ToList();

        //    AutoCompleteStringCollection autoCompletado = new AutoCompleteStringCollection();
        //    foreach (SPEC item in spec)
        //    {
        //        //agrega el texto encontrado en la segunda columna del datatable
        //        //si el datatable no tiene 2da columna va a dar error
        //        autoCompletado.Add(item.Nombre.ToString());
        //    }
        //    ComboSpec.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    ComboSpec.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    ComboSpec.AutoCompleteCustomSource = autoCompletado;
        //    CargarComboProvincia();
        //}
        //private async void CargarComboProvincia()
        //{
        //    var provincia = await unitOfWork.ProvinciaRepository.GetAllAsync();
        //    ComboProvincia.DisplayMember = "Nombre";
        //    ComboProvincia.ValueMember = "Id";
        //    ComboProvincia.DataSource = provincia.ToList();

        //    AutoCompleteStringCollection autoCompletado = new AutoCompleteStringCollection();
        //    foreach (Provincia item in provincia)
        //    {
        //        //agrega el texto encontrado en la segunda columna del datatable
        //        //si el datatable no tiene 2da columna va a dar error
        //        autoCompletado.Add(item.Nombre.ToString());
        //    }
        //    ComboProvincia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    ComboProvincia.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    ComboProvincia.AutoCompleteCustomSource = autoCompletado;
        //    CargarComboLocalidad();
        //}
        //private async void CargarComboLocalidad()
        //{
        //    var localidad = await unitOfWork.LocalidadRepository.GetAllAsync();
        //    ComboLocalidad.DisplayMember = "Nombre";
        //    ComboLocalidad.ValueMember = "Id";
        //    ComboLocalidad.DataSource = localidad.ToList();

        //    AutoCompleteStringCollection autoCompletado = new AutoCompleteStringCollection();
        //    foreach (Localidad item in localidad)
        //    {
        //        //agrega el texto encontrado en la segunda columna del datatable
        //        //si el datatable no tiene 2da columna va a dar error
        //        autoCompletado.Add(item.Nombre.ToString());
        //    }
        //    ComboLocalidad.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    ComboLocalidad.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    ComboLocalidad.AutoCompleteCustomSource = autoCompletado;
        //}
        private void GetComboData()
        {
            ComboData.DataSource = Enum.GetValues(typeof(DatosEnum));
            if (DataValue == "productos")
            {
                ComboData.SelectedIndex = 0;
            }
            else if (DataValue == "proveedores")
            {
                ComboData.SelectedIndex = 1;
            }
            else if (DataValue == "cuentas")
            {
                ComboData.SelectedIndex = 2;
            }
            else if (DataValue == "users")
            {
                ComboData.SelectedIndex = 3;
            }
            else if (DataValue == "remitos")
            {
                ComboData.SelectedIndex = 9;
            }
            DatosCargados = 1;

        }

        private async void GetAll()
        {
            if (RadioEnabled.Checked == true)
            {
                if (DataValue == "productos")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    //
                    LblTitleDatos.Text = "Productos";
                    listData.DataSource = ClasesCompartidas.productosList;
                    GridData.DataSource = listData;
                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = true;
                    //
                    CheckCboMarca.Enabled = true;
                    //
                    CheckCboSPEC.Enabled = true;
                    //
                    CheckCboProveedor.Enabled = true;
                    //
                    CheckCboProvincia.Enabled = false;
                    //
                    CheckCboLocalidad.Enabled = false;
                    //
                    CheckUsuarios.Enabled = true;
                    //
                    CheckTipoUsuario.Enabled = false;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    CheckFecha.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion

                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnNuevo.Enabled = true;
                    BtnEditar.Enabled = true;
                    BtnDetalles.Enabled = false;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    BtnEditar.Enabled = true;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = true;
                    IcoBtnCalcular.Enabled = true;
                    BtnSumar.Enabled = true;
                    BtnRestar.Enabled = true;
                    TxtStock.Enabled = true;
                    BtnGuardarPorcentaje.Enabled = true;
                    BtnGuardarPorcentaje.Enabled = true;
                    BtnCalcularBultoPVP.Enabled = true;
                    IcoBtnCalcular.Enabled = true;
                    TxtDescuento.Enabled = true;
                    BtnGuardarDescuento.Enabled = true;

                    // Remito
                    BtnRemito.Enabled = false;
                    #endregion
                }
                else if (DataValue == "proveedores")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //
                    LblTitleDatos.Text = "Proveedores";
                    listData.DataSource = ClasesCompartidas.proveedoresList;
                    GridData.DataSource = listData;

                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = false;
                    //
                    CheckCboMarca.Enabled = false;
                    //
                    CheckCboSPEC.Enabled = false;
                    //
                    CheckCboProveedor.Enabled = false;
                    //
                    CheckCboProvincia.Enabled = true;
                    //
                    CheckCboLocalidad.Enabled = true;
                    //
                    CheckUsuarios.Enabled = true;
                    //
                    CheckTipoUsuario.Enabled = false;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion

                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnEditar.Enabled = true;
                    BtnNuevo.Enabled = true;
                    BtnEditar.Enabled = true;
                    BtnDetalles.Enabled = false;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    // Funciones de Stock y de Precio
                    //-.Enabled = false;
                    //.Enabled = false;

                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnCalcularBultoPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    TxtDescuento.Enabled = false;
                    BtnGuardarDescuento.Enabled = false;
                    //
                    BtnRemito.Enabled = true;

                    #endregion
                }
                else if (DataValue == "users")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //
                    LblTitleDatos.Text = "Usuarios";
                    listData.DataSource = ClasesCompartidas.usuariosList;
                    GridData.DataSource = listData;

                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = false;
                    //
                    CheckCboMarca.Enabled = false;
                    //
                    CheckCboSPEC.Enabled = false;
                    //
                    CheckCboProveedor.Enabled = false;
                    //
                    CheckCboProvincia.Enabled = false;
                    //
                    CheckCboLocalidad.Enabled = false;
                    //
                    CheckUsuarios.Enabled = false;
                    //
                    CheckTipoUsuario.Enabled = true;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion
                    CheckFecha.Checked = false;

                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnNuevo.Enabled = true;
                    BtnEditar.Enabled = true;
                    BtnDetalles.Enabled = false;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    // Funciones de Stock y de Precio
                    //-.Enabled = false;
                    //.Enabled = false;
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnRemito.Enabled = false;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnCalcularBultoPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    TxtDescuento.Enabled = false;
                    BtnGuardarDescuento.Enabled = false;

                    //
                    #endregion
                }
                else if (DataValue == "cuentas")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    //
                    LblTitleDatos.Text = "Cuentas";
                    listData.DataSource = ClasesCompartidas.cuentasList;
                    GridData.DataSource = listData;

                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = false;
                    //
                    CheckCboMarca.Enabled = false;
                    //
                    CheckCboSPEC.Enabled = false;
                    //
                    CheckCboProveedor.Enabled = false;
                    //
                    CheckCboProvincia.Enabled = true;
                    //
                    CheckCboLocalidad.Enabled = true;
                    //
                    CheckUsuarios.Enabled = true;
                    //
                    CheckTipoUsuario.Enabled = false;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion
                    CheckFecha.Checked = false;

                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnNuevo.Enabled = true;
                    BtnEditar.Enabled = true;
                    BtnDetalles.Enabled = true;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    // Funciones de Stock y de Precio
                    //-.Enabled = false;
                    //.Enabled = false;
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnRemito.Enabled = false;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnCalcularBultoPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    TxtDescuento.Enabled = false;
                    BtnGuardarDescuento.Enabled = false;

                    //
                    #endregion
                }
                else if (DataValue == "tipoDeProductos")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //
                    listData.DataSource = ClasesCompartidas.tiposProductosList;
                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = false;
                    //
                    CheckCboMarca.Enabled = false;
                    //
                    CheckCboSPEC.Enabled = false;
                    //
                    CheckCboProveedor.Enabled = false;
                    //
                    CheckCboProvincia.Enabled = false;
                    //
                    CheckCboLocalidad.Enabled = false;
                    //
                    CheckUsuarios.Enabled = false;
                    //
                    CheckTipoUsuario.Enabled = false;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion
                    CheckFecha.Checked = false;
                    GridData.DataSource = listData;

                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnNuevo.Enabled = true;
                    BtnEditar.Enabled = false;
                    BtnDetalles.Enabled = false;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    // Funciones de Stock y de Precio
                    //-.Enabled = false;
                    //.Enabled = false;
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnCalcularBultoPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    TxtDescuento.Enabled = false;
                    BtnGuardarDescuento.Enabled = false;

                    //
                    BtnRemito.Enabled = false;
                    #endregion
                }
                else if (DataValue == "marcas")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //
                    listData.DataSource = ClasesCompartidas.marcasList;
                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = false;
                    //
                    CheckCboMarca.Enabled = false;
                    //
                    CheckCboSPEC.Enabled = false;
                    //
                    CheckCboProveedor.Enabled = false;
                    //
                    CheckCboProvincia.Enabled = false;
                    //
                    CheckCboLocalidad.Enabled = false;
                    //
                    CheckUsuarios.Enabled = false;
                    //
                    CheckTipoUsuario.Enabled = false;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion
                    CheckFecha.Checked = false;
                    GridData.DataSource = listData;

                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnNuevo.Enabled = true;
                    BtnEditar.Enabled = false;
                    BtnDetalles.Enabled = false;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnRemito.Enabled = false;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnCalcularBultoPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    TxtDescuento.Enabled = false;
                    BtnGuardarDescuento.Enabled = false;

                    //
                    #endregion
                }
                else if (DataValue == "specs")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //
                    listData.DataSource = ClasesCompartidas.specsList;
                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = false;
                    //
                    CheckCboMarca.Enabled = false;
                    //
                    CheckCboSPEC.Enabled = false;
                    //
                    CheckCboProveedor.Enabled = false;
                    //
                    CheckCboProvincia.Enabled = false;
                    //
                    CheckCboLocalidad.Enabled = false;
                    //
                    CheckUsuarios.Enabled = false;
                    //
                    CheckTipoUsuario.Enabled = false;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion
                    CheckFecha.Checked = false;
                    GridData.DataSource = listData;

                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnNuevo.Enabled = true;
                    BtnEditar.Enabled = false;
                    BtnDetalles.Enabled = false;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    // Funciones de Stock y de Precio
                    //-.Enabled = false;
                    //.Enabled = false;
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnRemito.Enabled = false;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnCalcularBultoPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    TxtDescuento.Enabled = false;
                    BtnGuardarDescuento.Enabled = false;
                    //
                    #endregion
                }
                else if (DataValue == "provincias")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //
                    listData.DataSource = ClasesCompartidas.provinciasList;
                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = false;
                    //
                    CheckCboMarca.Enabled = false;
                    //
                    CheckCboSPEC.Enabled = false;
                    //
                    CheckCboProveedor.Enabled = false;
                    //
                    CheckCboProvincia.Enabled = false;
                    //
                    CheckCboLocalidad.Enabled = false;
                    //
                    CheckUsuarios.Enabled = false;
                    //
                    CheckTipoUsuario.Enabled = false;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion
                    CheckFecha.Checked = false;
                    GridData.DataSource = listData;

                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnNuevo.Enabled = true;
                    BtnEditar.Enabled = false;
                    BtnDetalles.Enabled = false;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    // Funciones de Stock y de Precio
                    //-.Enabled = false;
                    //.Enabled = false;
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnRemito.Enabled = false;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnCalcularBultoPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    TxtDescuento.Enabled = false;
                    BtnGuardarDescuento.Enabled = false;
                    //
                    #endregion
                }
                else if (DataValue == "localidades")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //
                    listData.DataSource = ClasesCompartidas.localidadList;
                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = false;
                    //
                    CheckCboMarca.Enabled = false;
                    //
                    CheckCboSPEC.Enabled = false;
                    //
                    CheckCboProveedor.Enabled = false;
                    //
                    CheckCboProvincia.Enabled = false;
                    //
                    CheckCboLocalidad.Enabled = false;
                    //
                    CheckUsuarios.Enabled = false;
                    //
                    CheckTipoUsuario.Enabled = false;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion
                    CheckFecha.Checked = false;
                    GridData.DataSource = listData;

                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnNuevo.Enabled = true;
                    BtnEditar.Enabled = false;
                    BtnDetalles.Enabled = false;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    // Funciones de Stock y de Precio
                    //-.Enabled = false;
                    //.Enabled = false;
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnRemito.Enabled = false;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnCalcularBultoPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    TxtDescuento.Enabled = false;
                    BtnGuardarDescuento.Enabled = false;
                    //
                    #endregion
                }
                else if (DataValue == "remitos")
                {
                    GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //
                    //listData.DataSource = ClasesCompartidas.localidadList;
                    GridRemitos.DataSource = ClasesCompartidas.remitos;
                    GridRemitosDetalle.DataSource = ClasesCompartidas.remitosDetalle;
                    LblTitleDatos.Text = "Remitos";
                    //
                    GridData.Visible = false;
                    GridRemitos.Visible = true;
                    GridRemitosDetalle.Visible = true;
                    RemitosDivider.Visible = true;
                    //
                    #region CombosAndChecksParaElFiltrado
                    //
                    // Check's Enabled or Disabled
                    //
                    CheckCboTipo.Enabled = true;
                    //
                    CheckCboMarca.Enabled = true;
                    //
                    CheckCboSPEC.Enabled = true;
                    //
                    CheckCboProveedor.Enabled = true;
                    //
                    CheckCboProvincia.Enabled = false;
                    //
                    CheckCboLocalidad.Enabled = false;
                    //
                    CheckUsuarios.Enabled = true;
                    //
                    CheckTipoUsuario.Enabled = false;
                    //
                    // Check's Checked or Dischecked
                    //
                    CheckCboTipo.Checked = false;
                    //
                    CheckCboMarca.Checked = false;
                    //
                    CheckCboSPEC.Checked = false;
                    //
                    CheckCboProveedor.Checked = false;
                    //
                    CheckCboProvincia.Checked = false;
                    //
                    CheckCboLocalidad.Checked = false;
                    //
                    CheckUsuarios.Checked = false;
                    //
                    CheckTipoUsuario.Checked = false;
                    //
                    // ComboBox's Disabled
                    //
                    ComboTipoProducto.Enabled = false;
                    //
                    ComboMarcas.Enabled = false;
                    //
                    ComboSpec.Enabled = false;
                    //
                    ComboProvincia.Enabled = false;
                    //
                    ComboLocalidad.Enabled = false;
                    //
                    ComboUsuarios.Enabled = false;
                    //
                    ComboTipoUsuario.Enabled = false;
                    #endregion
                    CheckFecha.Checked = false;
                    #region Enable&DisabledButtons
                    // AccionesButtons
                    BtnNuevo.Enabled = false;
                    BtnEditar.Enabled = false;
                    BtnDetalles.Enabled = false;
                    BtnEna.Enabled = false;
                    BtnDis.Enabled = true;
                    BtnEliminar.Enabled = false;
                    BtnImprimir.Enabled = true;
                    // Funciones de Stock y de Precio
                    //-.Enabled = false;
                    //.Enabled = false;
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnRemito.Enabled = true;
                    // Funciones de Stock y de Precio
                    TxtPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    BtnSumar.Enabled = false;
                    BtnRestar.Enabled = false;
                    TxtStock.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnGuardarPorcentaje.Enabled = false;
                    BtnCalcularBultoPVP.Enabled = false;
                    IcoBtnCalcular.Enabled = false;
                    TxtDescuento.Enabled = false;
                    BtnGuardarDescuento.Enabled = false;
                    //
                    #endregion
                }
            }
            else
            {
                if (RadioDisabled.Checked == true)
                {
                    if (DataValue == "productos")
                    {
                        GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                        //
                        LblTitleDatos.Text = "Productos";
                        //listData.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario), filter: v => v.Visible.Equals(false));
                        listData.DataSource = ClasesCompartidas.DesProductList;
                        #region CombosAndChecksParaElFiltrado
                        //
                        // Check's Enabled or Disabled
                        //
                        CheckCboTipo.Enabled = true;
                        //
                        CheckCboMarca.Enabled = true;
                        //
                        CheckCboSPEC.Enabled = true;
                        //
                        CheckCboProveedor.Enabled = true;
                        //
                        CheckCboProvincia.Enabled = false;
                        //
                        CheckCboLocalidad.Enabled = false;
                        //
                        CheckUsuarios.Enabled = true;
                        //
                        CheckTipoUsuario.Enabled = false;
                        //
                        // Check's Checked or Dischecked
                        //
                        CheckCboTipo.Checked = false;
                        //
                        CheckCboMarca.Checked = false;
                        //
                        CheckCboSPEC.Checked = false;
                        //
                        CheckCboProveedor.Checked = false;
                        //
                        CheckCboProvincia.Checked = false;
                        //
                        CheckCboLocalidad.Checked = false;
                        //
                        CheckUsuarios.Checked = false;
                        //
                        CheckTipoUsuario.Checked = false;
                        //
                        // ComboBox's Disabled
                        //
                        ComboTipoProducto.Enabled = false;
                        //
                        ComboMarcas.Enabled = false;
                        //
                        ComboSpec.Enabled = false;
                        //
                        ComboProvincia.Enabled = false;
                        //
                        ComboLocalidad.Enabled = false;
                        //
                        ComboUsuarios.Enabled = false;
                        //
                        ComboTipoUsuario.Enabled = false;
                        #endregion
                        CheckFecha.Checked = false;
                        GridData.DataSource = listData;

                        #region Enable&DisabledButtons
                        // AccionesButtons
                        BtnNuevo.Enabled = false;
                        //
                        BtnEditar.Enabled = false;
                        //
                        BtnDetalles.Enabled = false;
                        //
                        BtnEna.Enabled = true;
                        //
                        BtnDis.Enabled = false;
                        //
                        BtnEliminar.Enabled = true;
                        //
                        BtnImprimir.Enabled = true;
                        // Funciones de Stock y de Precio
                        //-.Enabled = false;
                        //.Enabled = false;
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnRemito.Enabled = false;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnCalcularBultoPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        TxtDescuento.Enabled = false;
                        BtnGuardarDescuento.Enabled = false;

                        //
                        #endregion

                    }
                    else if (DataValue == "proveedores")
                    {
                        GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        //
                        LblTitleDatos.Text = "Proveedores";
                        //listData.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(false));
                        listData.DataSource = ClasesCompartidas.DesProveedorList;
                        #region CombosAndChecksParaElFiltrado
                        //
                        // Check's Enabled or Disabled
                        //
                        CheckCboTipo.Enabled = false;
                        //
                        CheckCboMarca.Enabled = false;
                        //
                        CheckCboSPEC.Enabled = false;
                        //
                        CheckCboProveedor.Enabled = false;
                        //
                        CheckCboProvincia.Enabled = true;
                        //
                        CheckCboLocalidad.Enabled = true;
                        //
                        CheckUsuarios.Enabled = true;
                        //
                        CheckTipoUsuario.Enabled = false;
                        //
                        // Check's Checked or Dischecked
                        //
                        CheckCboTipo.Checked = false;
                        //
                        CheckCboMarca.Checked = false;
                        //
                        CheckCboSPEC.Checked = false;
                        //
                        CheckCboProveedor.Checked = false;
                        //
                        CheckCboProvincia.Checked = false;
                        //
                        CheckCboLocalidad.Checked = false;
                        //
                        CheckUsuarios.Checked = false;
                        //
                        CheckTipoUsuario.Checked = false;
                        //
                        // ComboBox's Disabled
                        //
                        ComboTipoProducto.Enabled = false;
                        //
                        ComboMarcas.Enabled = false;
                        //
                        ComboSpec.Enabled = false;
                        //
                        ComboProvincia.Enabled = false;
                        //
                        ComboLocalidad.Enabled = false;
                        //
                        ComboUsuarios.Enabled = false;
                        //
                        ComboTipoUsuario.Enabled = false;
                        #endregion
                        CheckFecha.Checked = false;
                        GridData.DataSource = listData;

                        #region Enable&DisabledButtons
                        // AccionesButtons
                        BtnNuevo.Enabled = false;
                        //
                        BtnEditar.Enabled = false;
                        //
                        BtnDetalles.Enabled = false;
                        //
                        BtnEna.Enabled = true;
                        //
                        BtnDis.Enabled = false;
                        //
                        BtnEliminar.Enabled = true;
                        //
                        BtnImprimir.Enabled = true;
                        // Funciones de Stock y de Precio
                        //-.Enabled = false;
                        //.Enabled = false;
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnRemito.Enabled = false;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnCalcularBultoPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        TxtDescuento.Enabled = false;
                        BtnGuardarDescuento.Enabled = false;
                        //
                        #endregion
                    }
                    else if (DataValue == "users")
                    {
                        GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        //
                        LblTitleDatos.Text = "Usuarios";
                        listData.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(false));
                        CheckFecha.Checked = false;
                        #region CombosAndChecksParaElFiltrado
                        //
                        // Check's Enabled or Disabled
                        //
                        CheckCboTipo.Enabled = false;
                        //
                        CheckCboMarca.Enabled = false;
                        //
                        CheckCboSPEC.Enabled = false;
                        //
                        CheckCboProveedor.Enabled = false;
                        //
                        CheckCboProvincia.Enabled = false;
                        //
                        CheckCboLocalidad.Enabled = false;
                        //
                        CheckUsuarios.Enabled = false;
                        //
                        CheckTipoUsuario.Enabled = true;
                        //
                        // Check's Checked or Dischecked
                        //
                        CheckCboTipo.Checked = false;
                        //
                        CheckCboMarca.Checked = false;
                        //
                        CheckCboSPEC.Checked = false;
                        //
                        CheckCboProveedor.Checked = false;
                        //
                        CheckCboProvincia.Checked = false;
                        //
                        CheckCboLocalidad.Checked = false;
                        //
                        CheckUsuarios.Checked = false;
                        //
                        CheckTipoUsuario.Checked = false;
                        //
                        // ComboBox's Disabled
                        //
                        ComboTipoProducto.Enabled = false;
                        //
                        ComboMarcas.Enabled = false;
                        //
                        ComboSpec.Enabled = false;
                        //
                        ComboProvincia.Enabled = false;
                        //
                        ComboLocalidad.Enabled = false;
                        //
                        ComboUsuarios.Enabled = false;
                        //
                        ComboTipoUsuario.Enabled = false;
                        #endregion
                        GridData.DataSource = listData;

                        #region Enable&DisabledButtons
                        // AccionesButtons
                        BtnNuevo.Enabled = false;
                        //
                        BtnEditar.Enabled = false;
                        //
                        BtnDetalles.Enabled = false;
                        //
                        BtnEna.Enabled = true;
                        //
                        BtnDis.Enabled = false;
                        //
                        BtnEliminar.Enabled = true;
                        //
                        BtnImprimir.Enabled = true;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnRemito.Enabled = false;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnCalcularBultoPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        TxtDescuento.Enabled = false;
                        BtnGuardarDescuento.Enabled = false;
                        //
                        #endregion
                    }
                    else if (DataValue == "cuentas")
                    {
                        GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                        //
                        LblTitleDatos.Text = "Cuentas";
                        listData.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(false));
                        CheckFecha.Checked = false;
                        #region CombosAndChecksParaElFiltrado
                        //
                        // Check's Enabled or Disabled
                        //
                        CheckCboTipo.Enabled = false;
                        //
                        CheckCboMarca.Enabled = false;
                        //
                        CheckCboSPEC.Enabled = false;
                        //
                        CheckCboProveedor.Enabled = false;
                        //
                        CheckCboProvincia.Enabled = true;
                        //
                        CheckCboLocalidad.Enabled = true;
                        //
                        CheckUsuarios.Enabled = true;
                        //
                        CheckTipoUsuario.Enabled = false;
                        //
                        // Check's Checked or Dischecked
                        //
                        CheckCboTipo.Checked = false;
                        //
                        CheckCboMarca.Checked = false;
                        //
                        CheckCboSPEC.Checked = false;
                        //
                        CheckCboProveedor.Checked = false;
                        //
                        CheckCboProvincia.Checked = false;
                        //
                        CheckCboLocalidad.Checked = false;
                        //
                        CheckUsuarios.Checked = false;
                        //
                        CheckTipoUsuario.Checked = false;
                        //
                        // ComboBox's Disabled
                        //
                        ComboTipoProducto.Enabled = false;
                        //
                        ComboMarcas.Enabled = false;
                        //
                        ComboSpec.Enabled = false;
                        //
                        ComboProvincia.Enabled = false;
                        //
                        ComboLocalidad.Enabled = false;
                        //
                        ComboUsuarios.Enabled = false;
                        //
                        ComboTipoUsuario.Enabled = false;
                        #endregion
                        GridData.DataSource = listData;

                        #region Enable&DisabledButtons
                        // AccionesButtons
                        BtnNuevo.Enabled = false;
                        //
                        BtnEditar.Enabled = false;
                        //
                        BtnDetalles.Enabled = true;
                        //
                        BtnEna.Enabled = true;
                        //
                        BtnDis.Enabled = false;
                        //
                        BtnEliminar.Enabled = true;
                        //
                        BtnImprimir.Enabled = true;
                        // Funciones de Stock y de Precio
                        //-.Enabled = false;
                        //.Enabled = false;
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnRemito.Enabled = false;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnCalcularBultoPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        TxtDescuento.Enabled = false;
                        BtnGuardarDescuento.Enabled = false;
                        //
                        #endregion
                    }
                    else if (DataValue == "tipoDeProductos")
                    {
                        GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        //
                        listData.DataSource = await unitOfWork.TipoProductoRepository.GetAllAsync(filter: v => v.Visible.Equals(false));
                        CheckFecha.Checked = false;
                        #region CombosAndChecksParaElFiltrado
                        //
                        // Check's Enabled or Disabled
                        //
                        CheckCboTipo.Enabled = false;
                        //
                        CheckCboMarca.Enabled = false;
                        //
                        CheckCboSPEC.Enabled = false;
                        //
                        CheckCboProveedor.Enabled = false;
                        //
                        CheckCboProvincia.Enabled = false;
                        //
                        CheckCboLocalidad.Enabled = false;
                        //
                        CheckUsuarios.Enabled = false;
                        //
                        CheckTipoUsuario.Enabled = false;
                        //
                        // Check's Checked or Dischecked
                        //
                        CheckCboTipo.Checked = false;
                        //
                        CheckCboMarca.Checked = false;
                        //
                        CheckCboSPEC.Checked = false;
                        //
                        CheckCboProveedor.Checked = false;
                        //
                        CheckCboProvincia.Checked = false;
                        //
                        CheckCboLocalidad.Checked = false;
                        //
                        CheckUsuarios.Checked = false;
                        //
                        CheckTipoUsuario.Checked = false;
                        //
                        // ComboBox's Disabled
                        //
                        ComboTipoProducto.Enabled = false;
                        //
                        ComboMarcas.Enabled = false;
                        //
                        ComboSpec.Enabled = false;
                        //
                        ComboProvincia.Enabled = false;
                        //
                        ComboLocalidad.Enabled = false;
                        //
                        ComboUsuarios.Enabled = false;
                        //
                        ComboTipoUsuario.Enabled = false;
                        #endregion
                        GridData.DataSource = listData;

                        #region Enable&DisabledButtons
                        // AccionesButtons
                        BtnNuevo.Enabled = false;
                        //
                        BtnEditar.Enabled = false;
                        //
                        BtnDetalles.Enabled = false;
                        //
                        BtnEna.Enabled = true;
                        //
                        BtnDis.Enabled = false;
                        //
                        BtnEliminar.Enabled = true;
                        //
                        BtnImprimir.Enabled = true;
                        // Funciones de Stock y de Precio
                        //-.Enabled = false;
                        //.Enabled = false;
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnRemito.Enabled = false;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnCalcularBultoPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        TxtDescuento.Enabled = false;
                        BtnGuardarDescuento.Enabled = false;
                        //
                        #endregion

                    }
                    else if (DataValue == "marcas")
                    {
                        GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        //
                        listData.DataSource = await unitOfWork.MarcaRepository.GetAllAsync(filter: v => v.Visible.Equals(false));
                        CheckFecha.Checked = false;
                        #region CombosAndChecksParaElFiltrado
                        //
                        // Check's Enabled or Disabled
                        //
                        CheckCboTipo.Enabled = false;
                        //
                        CheckCboMarca.Enabled = false;
                        //
                        CheckCboSPEC.Enabled = false;
                        //
                        CheckCboProveedor.Enabled = false;
                        //
                        CheckCboProvincia.Enabled = false;
                        //
                        CheckCboLocalidad.Enabled = false;
                        //
                        CheckUsuarios.Enabled = false;
                        //
                        CheckTipoUsuario.Enabled = false;
                        //
                        // Check's Checked or Dischecked
                        //
                        CheckCboTipo.Checked = false;
                        //
                        CheckCboMarca.Checked = false;
                        //
                        CheckCboSPEC.Checked = false;
                        //
                        CheckCboProveedor.Checked = false;
                        //
                        CheckCboProvincia.Checked = false;
                        //
                        CheckCboLocalidad.Checked = false;
                        //
                        CheckUsuarios.Checked = false;
                        //
                        CheckTipoUsuario.Checked = false;
                        //
                        // ComboBox's Disabled
                        //
                        ComboTipoProducto.Enabled = false;
                        //
                        ComboMarcas.Enabled = false;
                        //
                        ComboSpec.Enabled = false;
                        //
                        ComboProvincia.Enabled = false;
                        //
                        ComboLocalidad.Enabled = false;
                        //
                        ComboUsuarios.Enabled = false;
                        //
                        ComboTipoUsuario.Enabled = false;
                        #endregion
                        GridData.DataSource = listData;

                        #region Enable&DisabledButtons
                        // AccionesButtons
                        BtnNuevo.Enabled = false;
                        //
                        BtnEditar.Enabled = false;
                        //
                        BtnDetalles.Enabled = false;
                        //
                        BtnEna.Enabled = true;
                        //
                        BtnDis.Enabled = false;
                        //
                        BtnEliminar.Enabled = true;
                        //
                        BtnImprimir.Enabled = true;
                        // Funciones de Stock y de Precio
                        //-.Enabled = false;
                        //.Enabled = false;
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnRemito.Enabled = false;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnCalcularBultoPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        TxtDescuento.Enabled = false;
                        BtnGuardarDescuento.Enabled = false;
                        //
                        #endregion
                    }
                    else if (DataValue == "specs")
                    {
                        GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        //
                        listData.DataSource = await unitOfWork.SPECRepository.GetAllAsync(filter: v => v.Visible.Equals(false));
                        CheckFecha.Checked = false;
                        #region CombosAndChecksParaElFiltrado
                        //
                        // Check's Enabled or Disabled
                        //
                        CheckCboTipo.Enabled = false;
                        //
                        CheckCboMarca.Enabled = false;
                        //
                        CheckCboSPEC.Enabled = false;
                        //
                        CheckCboProveedor.Enabled = false;
                        //
                        CheckCboProvincia.Enabled = false;
                        //
                        CheckCboLocalidad.Enabled = false;
                        //
                        CheckUsuarios.Enabled = false;
                        //
                        CheckTipoUsuario.Enabled = false;
                        //
                        // Check's Checked or Dischecked
                        //
                        CheckCboTipo.Checked = false;
                        //
                        CheckCboMarca.Checked = false;
                        //
                        CheckCboSPEC.Checked = false;
                        //
                        CheckCboProveedor.Checked = false;
                        //
                        CheckCboProvincia.Checked = false;
                        //
                        CheckCboLocalidad.Checked = false;
                        //
                        CheckUsuarios.Checked = false;
                        //
                        CheckTipoUsuario.Checked = false;
                        //
                        // ComboBox's Disabled
                        //
                        ComboTipoProducto.Enabled = false;
                        //
                        ComboMarcas.Enabled = false;
                        //
                        ComboSpec.Enabled = false;
                        //
                        ComboProvincia.Enabled = false;
                        //
                        ComboLocalidad.Enabled = false;
                        //
                        ComboUsuarios.Enabled = false;
                        //
                        ComboTipoUsuario.Enabled = false;
                        #endregion

                        #region Enable&DisabledButtons
                        GridData.DataSource = listData;

                        // AccionesButtons
                        BtnNuevo.Enabled = false;
                        //
                        BtnEditar.Enabled = false;
                        //
                        BtnDetalles.Enabled = false;
                        //
                        BtnEna.Enabled = true;
                        //
                        BtnDis.Enabled = false;
                        //
                        BtnEliminar.Enabled = true;
                        //
                        BtnImprimir.Enabled = true;
                        // Funciones de Stock y de Precio
                        //-.Enabled = false;
                        //.Enabled = false;
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnRemito.Enabled = false;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnCalcularBultoPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        TxtDescuento.Enabled = false;
                        BtnGuardarDescuento.Enabled = false;
                        //
                        #endregion
                    }
                    else if (DataValue == "provincias")
                    {
                        GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        //
                        listData.DataSource = await unitOfWork.ProvinciaRepository.GetAllAsync(filter: v => v.Visible.Equals(false));
                        CheckFecha.Checked = false;
                        #region CombosAndChecksParaElFiltrado
                        //
                        // Check's Enabled or Disabled
                        //
                        CheckCboTipo.Enabled = false;
                        //
                        CheckCboMarca.Enabled = false;
                        //
                        CheckCboSPEC.Enabled = false;
                        //
                        CheckCboProveedor.Enabled = false;
                        //
                        CheckCboProvincia.Enabled = false;
                        //
                        CheckCboLocalidad.Enabled = false;
                        //
                        CheckUsuarios.Enabled = false;
                        //
                        CheckTipoUsuario.Enabled = false;
                        //
                        // Check's Checked or Dischecked
                        //
                        CheckCboTipo.Checked = false;
                        //
                        CheckCboMarca.Checked = false;
                        //
                        CheckCboSPEC.Checked = false;
                        //
                        CheckCboProveedor.Checked = false;
                        //
                        CheckCboProvincia.Checked = false;
                        //
                        CheckCboLocalidad.Checked = false;
                        //
                        CheckUsuarios.Checked = false;
                        //
                        CheckTipoUsuario.Checked = false;
                        //
                        // ComboBox's Disabled
                        //
                        ComboTipoProducto.Enabled = false;
                        //
                        ComboMarcas.Enabled = false;
                        //
                        ComboSpec.Enabled = false;
                        //
                        ComboProvincia.Enabled = false;
                        //
                        ComboLocalidad.Enabled = false;
                        //
                        ComboUsuarios.Enabled = false;
                        //
                        ComboTipoUsuario.Enabled = false;
                        #endregion
                        GridData.DataSource = listData;

                        #region Enable&DisabledButtons
                        // AccionesButtons
                        BtnNuevo.Enabled = false;
                        //
                        BtnEditar.Enabled = false;
                        //
                        BtnDetalles.Enabled = false;
                        //
                        BtnEna.Enabled = true;
                        //
                        BtnDis.Enabled = false;
                        //
                        BtnEliminar.Enabled = true;
                        //
                        BtnImprimir.Enabled = true;
                        // Funciones de Stock y de Precio
                        //-.Enabled = false;
                        //-.Enabled = false;
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnRemito.Enabled = false;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnCalcularBultoPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        TxtDescuento.Enabled = false;
                        BtnGuardarDescuento.Enabled = false;
                        //
                        #endregion
                    }
                    else if (DataValue == "localidades")
                    {
                        GridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        //
                        listData.DataSource = await unitOfWork.LocalidadRepository.GetAllAsync(filter: v => v.Visible.Equals(false));
                        CheckFecha.Checked = false;
                        #region CombosAndChecksParaElFiltrado
                        //
                        // Check's Enabled or Disabled
                        //
                        CheckCboTipo.Enabled = false;
                        //
                        CheckCboMarca.Enabled = false;
                        //
                        CheckCboSPEC.Enabled = false;
                        //
                        CheckCboProveedor.Enabled = false;
                        //
                        CheckCboProvincia.Enabled = false;
                        //
                        CheckCboLocalidad.Enabled = false;
                        //
                        CheckUsuarios.Enabled = false;
                        //
                        CheckTipoUsuario.Enabled = false;
                        //
                        // Check's Checked or Dischecked
                        //
                        CheckCboTipo.Checked = false;
                        //
                        CheckCboMarca.Checked = false;
                        //
                        CheckCboSPEC.Checked = false;
                        //
                        CheckCboProveedor.Checked = false;
                        //
                        CheckCboProvincia.Checked = false;
                        //
                        CheckCboLocalidad.Checked = false;
                        //
                        CheckUsuarios.Checked = false;
                        //
                        CheckTipoUsuario.Checked = false;
                        //
                        // ComboBox's Disabled
                        //
                        ComboTipoProducto.Enabled = false;
                        //
                        ComboMarcas.Enabled = false;
                        //
                        ComboSpec.Enabled = false;
                        //
                        ComboProvincia.Enabled = false;
                        //
                        ComboLocalidad.Enabled = false;
                        //
                        ComboUsuarios.Enabled = false;
                        //
                        ComboTipoUsuario.Enabled = false;
                        #endregion
                        GridData.DataSource = listData;

                        #region Enable&DisabledButtons
                        // AccionesButtons
                        BtnNuevo.Enabled = false;
                        //
                        BtnEditar.Enabled = false;
                        //
                        BtnDetalles.Enabled = false;
                        //
                        BtnEna.Enabled = true;
                        //
                        BtnDis.Enabled = false;
                        //
                        BtnEliminar.Enabled = true;
                        //
                        BtnImprimir.Enabled = true;
                        // Funciones de Stock y de Precio
                        //-.Enabled = false;
                        //-.Enabled = false;
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnRemito.Enabled = false;
                        // Funciones de Stock y de Precio
                        TxtPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        BtnSumar.Enabled = false;
                        BtnRestar.Enabled = false;
                        TxtStock.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnGuardarPorcentaje.Enabled = false;
                        BtnCalcularBultoPVP.Enabled = false;
                        IcoBtnCalcular.Enabled = false;
                        TxtDescuento.Enabled = false;
                        BtnGuardarDescuento.Enabled = false;
                        //
                        #endregion
                    }
                }

            }
            DatosCargados = 1;
            // Carga de los ComboBox
            //CargarComboTipoProducto();
        }
        private async void GetAll(string txtBusqueda)
        {
            string recibedSearch = txtBusqueda;

            if (RadioEnabled.Checked == true)
            {
                if (DataValue == "productos")
                {
                    listData.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario),
                        filter: c => c.TipoProducto.Nombre.Contains(recibedSearch) && c.Visible.Equals(true) || c.Marca.Nombre.Contains(recibedSearch) && c.Visible.Equals(true) || c.SPEC.Nombre.Contains(recibedSearch) &&
                        c.Visible.Equals(true) || c.Detalles.Contains(recibedSearch) && c.Visible.Equals(true) || c.Usuario.Nombre.Contains(recibedSearch) && c.Visible.Equals(true));
                }
                else if (DataValue == "proveedores")
                {
                    listData.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario),
                        filter: p => p.Provincia.Nombre.Contains(recibedSearch) && p.Visible.Equals(true) || p.Localidad.Nombre.Contains(recibedSearch) && p.Visible.Equals(true) || p.Nombre.Contains(recibedSearch) && p.Visible.Equals(true) || p.Usuario.Nombre.Contains(recibedSearch) && p.Visible.Equals(true));
                }
                else if (DataValue == "users")
                {
                    listData.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario),
                        filter: u => u.Nombre.Contains(recibedSearch) && u.Visible.Equals(true) || u.User.Contains(recibedSearch) && u.Visible.Equals(true) || u.TipoDeUsuario.Nombre.Contains(recibedSearch) && u.Visible.Equals(true));
                }
                else if (DataValue == "cuentas")
                {
                    listData.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Localidad.Nombre).Include(c => c.Localidad),
                        filter: c => c.Nombre.Contains(recibedSearch) && c.Visible.Equals(true) || c.Provincia.Nombre.Contains(recibedSearch) && c.Visible.Equals(true) || c.Localidad.Nombre.Contains(recibedSearch) && c.Visible.Equals(true) || c.DNI.Contains(recibedSearch) && c.Visible.Equals(true));
                }
                else if (DataValue == "tipoDeProductos")
                {
                    listData.DataSource = await unitOfWork.SPECRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(true));
                }
                else if (DataValue == "marcas")
                {
                    listData.DataSource = await unitOfWork.MarcaRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(true));
                }
                else if (DataValue == "specs")
                {
                    listData.DataSource = await unitOfWork.SPECRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(true));
                }
                else if (DataValue == "provincias")
                {
                    listData.DataSource = await unitOfWork.ProvinciaRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(true));
                }
                else if (DataValue == "localidades")
                {
                    listData.DataSource = await unitOfWork.LocalidadRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(true));
                }
                else if (DataValue == "remitos")
                {
                    listData.DataSource = await unitOfWork.RemitoRepository.GetAllAsync(include: q => q.Include(q => q.Proveedor).Include(q => q.Usuario), filter: q => q.Usuario.Nombre.Contains(recibedSearch) || q.TipoComprobante.Contains(recibedSearch) || q.Proveedor.Nombre.Contains(recibedSearch));
                }
            }
            else if (RadioDisabled.Checked == true)
            {
                if (DataValue == "productos")
                {
                    listData.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario),
                        filter: c => c.TipoProducto.Nombre.Contains(recibedSearch) && c.Visible.Equals(false) || c.Marca.Nombre.Contains(recibedSearch) && c.Visible.Equals(false) || c.SPEC.Nombre.Contains(recibedSearch) &&
                        c.Visible.Equals(false) || c.Detalles.Contains(recibedSearch) && c.Visible.Equals(false) || c.Usuario.Nombre.Contains(recibedSearch) && c.Visible.Equals(false));
                }
                else if (DataValue == "proveedores")
                {
                    listData.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario),
                        filter: p => p.Provincia.Nombre.Contains(recibedSearch) && p.Visible.Equals(false) || p.Localidad.Nombre.Contains(recibedSearch) && p.Visible.Equals(false) || p.Nombre.Contains(recibedSearch) && p.Visible.Equals(false) || p.Usuario.Nombre.Contains(recibedSearch) && p.Visible.Equals(false));
                }
                else if (DataValue == "users")
                {
                    listData.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario),
                        filter: u => u.Nombre.Contains(recibedSearch) && u.Visible.Equals(false) || u.User.Contains(recibedSearch) && u.Visible.Equals(false) || u.TipoDeUsuario.Nombre.Contains(recibedSearch) && u.Visible.Equals(false));
                }
                else if (DataValue == "cuentas")
                {
                    listData.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Localidad.Nombre).Include(c => c.Localidad),
                        filter: c => c.Nombre.Contains(recibedSearch) && c.Visible.Equals(false) || c.Provincia.Nombre.Contains(recibedSearch) && c.Visible.Equals(false) || c.Localidad.Nombre.Contains(recibedSearch) && c.Visible.Equals(false) || c.DNI.Contains(recibedSearch) && c.Visible.Equals(false));
                }
                else if (DataValue == "tipoDeProductos")
                {
                    listData.DataSource = await unitOfWork.SPECRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(false));
                }
                else if (DataValue == "marcas")
                {
                    listData.DataSource = await unitOfWork.MarcaRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(false));
                }
                else if (DataValue == "specs")
                {
                    listData.DataSource = await unitOfWork.SPECRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(false));
                }
                else if (DataValue == "provincias")
                {
                    listData.DataSource = await unitOfWork.ProvinciaRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(false));
                }
                else if (DataValue == "localidades")
                {
                    listData.DataSource = await unitOfWork.LocalidadRepository.GetAllAsync(filter: n => n.Nombre.Contains(recibedSearch) && n.Visible.Equals(false));
                }
            }


        }

        private void MasterDataView_Load(object sender, EventArgs e)
        {
            CargarDatosComboBox();
        }

        private void CargarDatosComboBox()
        {
            // Carga de los datos
            //await Task.Run(async () =>
            //{
            //    //localidadList.DataSource = await unitOfWork.LocalidadRepository.GetAllAsync();
            //    marcasList.DataSource = await unitOfWork.MarcaRepository.GetAllAsync();
            //    //proveedoresList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync();
            //    //provinciasList.DataSource = await unitOfWork.ProvinciaRepository.GetAllAsync();
            //    //specsList.DataSource = await unitOfWork.SPECRepository.GetAllAsync();
            //    //tiposProductosList.DataSource = await unitOfWork.TipoProductoRepository.GetAllAsync();
            //    //usuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync();
            //    //tiposdeUsuarioList.DataSource = await unitOfWork.TipoUsuarioRepository.GetAllAsync();
            //    combosCargados = true;
            //});

            // Una vez cargados se los muestra en los ComboBox

            // Se llama al método de la lista principal
            GetAll();
            GetPorcentaje();
            // CargaDeCombosMarcas
            ComboMarcas.DisplayMember = "Nombre";
            ComboMarcas.ValueMember = "Id";
            ComboMarcas.DataSource = ClasesCompartidas.marcasList;

            // CargaDeComboLocalidad
            ComboLocalidad.DisplayMember = "Nombre";
            ComboLocalidad.ValueMember = "Id";
            ComboLocalidad.DataSource = ClasesCompartidas.localidadList;

            // CargaDeComboTipoDeProducto
            ComboTipoProducto.DisplayMember = "Nombre";
            ComboTipoProducto.ValueMember = "Id";
            ComboTipoProducto.DataSource = ClasesCompartidas.tiposProductosList;

            // CargaDeComboSPECS
            ComboSpec.DisplayMember = "Nombre";
            ComboSpec.ValueMember = "Id";
            ComboSpec.DataSource = ClasesCompartidas.specsList;

            // CargaDeComboSPECS
            ComboProveedor.DisplayMember = "Nombre";
            ComboProveedor.ValueMember = "Id";
            ComboProveedor.DataSource = ClasesCompartidas.proveedoresList;

            // CargaDeComboProvincia
            ComboProvincia.DisplayMember = "Nombre";
            ComboProvincia.ValueMember = "Id";
            ComboProvincia.DataSource = ClasesCompartidas.provinciasList;

            // CargaDeComboUsuario
            ComboUsuarios.DisplayMember = "Nombre";
            ComboUsuarios.ValueMember = "Id";
            ComboUsuarios.DataSource = ClasesCompartidas.usuariosList;

            // CargaDeComboTipoUsuario
            ComboTipoUsuario.DisplayMember = "Nombre";
            ComboTipoUsuario.ValueMember = "Id";
            ComboTipoUsuario.DataSource = ClasesCompartidas.tiposdeUsuarioList;

        }

        private void GridData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (DataValue == "productos")
            {
                foreach (DataGridViewRow row in GridData.Rows)
                {
                    for (int i = 0; i < GridData.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(row.Cells["Stock"].Value) < 1)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(205, 69, 69);
                            row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 66, 66);
                        }
                    }
                }
            }

            foreach (DataGridViewColumn columna in GridData.Columns)
            {
                //columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //columna.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                #region VisibilidadDeColumnas
                if (columna.Name == "TipoProductoId")
                    columna.Visible = false;
                if (columna.Name == "MarcaId")
                    columna.Visible = false;
                if (columna.Name == "SPECId")
                    columna.Visible = false;
                if (columna.Name == "ProveedorId")
                    columna.Visible = false;
                if (columna.Name == "UsuarioId")
                    columna.Visible = false;
                if (columna.Name == "Shop")
                    columna.Visible = false;
                if (columna.Name == "ShopId")
                    columna.Visible = false;
                if (columna.Name == "Imagen")
                    columna.Visible = false;
                if (columna.Name == "Visible")
                    columna.Visible = false;
                if (columna.Name == "ProvinciaId")
                    columna.Visible = false;
                if (columna.Name == "LocalidadId")
                    columna.Visible = false;
                if (columna.Name == "TipoDeUsuarioId")
                    columna.Visible = false;
                if (columna.Name == "Password")
                    columna.Visible = false;

                #endregion
                //-------------------
                #region AjustesDeColumnas
                if (columna.Name == "Id")
                    columna.Width = 40;
                if (columna.Name == "TipoProducto")
                    columna.HeaderText = "Tipo";
                if (columna.Name == "PrecioBulto")
                {
                    columna.HeaderText = "P. Bulto";
                    columna.Width = 90;
                    this.GridData.Columns["PrecioBulto"].DefaultCellStyle.Format = "$" + "0.00";
                }
                if (columna.Name == "CantidadBulto")
                {
                    columna.HeaderText = "Cantidad";
                    columna.Width = 90;
                }
                if (columna.Name == "Saldo")
                {
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                }
                if (columna.Name == "PrecioUnidad")
                {
                    columna.HeaderText = "P. Unidad";
                    columna.Width = 90;
                    this.GridData.Columns["PrecioUnidad"].DefaultCellStyle.Format = "#";
                }
                if (columna.Name == "PVP")
                {
                    columna.HeaderText = "PVP";
                    columna.Width = 90;
                    this.GridData.Columns["PVP"].DefaultCellStyle.Format = "$" + "0.00";
                }
                if (columna.Name == "Telefono")
                    columna.HeaderText = "Teléfono";
                if (columna.Name == "Direccion")
                    columna.HeaderText = "Dirección";
                if (columna.Name == "Modificacion")
                    columna.HeaderText = "Modificación";
                if (columna.Name == "Genero")
                    columna.HeaderText = "Género";
                if (columna.Name == "TipoDeUsuario")
                {
                    columna.HeaderText = "Tipo";


                }
                if (columna.Name == "TelefonoDos")
                    columna.HeaderText = "2° Teléfono";
                if (columna.Name == "Deuda")
                {
                    columna.Width = 90;
                    this.GridData.Columns["Deuda"].DefaultCellStyle.Format = "$" + "0.00";
                }
                if (columna.Name == "Ganancia")
                {
                    columna.Width = 80;
                    this.GridData.Columns["Ganancia"].DefaultCellStyle.Format = "0.00";
                }
                if (columna.Name == "Descuento")
                {
                    columna.Width = 80;
                    this.GridData.Columns["Descuento"].DefaultCellStyle.Format = "0.00";
                }
                if (columna.Name == "Stock")
                {
                    columna.Width = 70;
                }
                if (columna.Name == "SPEC")
                {
                    columna.Width = 70;
                }
                #endregion
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string txtBusqueda = TxtBuscar.Text;
            GetAll(txtBusqueda);
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (TxtBuscar.Text.Length < 1)
            {
                _filter = false;
                GetAll();
            }
        }

        private void ComboData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DatosCargados == 1)
            {
                if (ComboData.SelectedIndex == 0)
                {
                    DataValue = "productos";
                    LblTitleDatos.Text = "Productos";
                    // Ocultar o aparecer cosas
                    GridData.Visible = true;
                    GridRemitos.Visible = false;
                    GridRemitosDetalle.Visible = false;
                    RemitosDivider.Visible = false;
                    //
                    GetAll();
                }
                else if (ComboData.SelectedIndex == 1)
                {
                    DataValue = "proveedores";
                    LblTitleDatos.Text = "Proveedores";
                    // Ocultar o aparecer cosas
                    GridData.Visible = true;
                    GridRemitos.Visible = false;
                    GridRemitosDetalle.Visible = false;
                    RemitosDivider.Visible = false;
                    //
                    GetAll();
                }
                else if (ComboData.SelectedIndex == 2)
                {
                    DataValue = "cuentas";
                    LblTitleDatos.Text = "Cuentas";
                    // Ocultar o aparecer cosas
                    GridData.Visible = true;
                    GridRemitos.Visible = false;
                    GridRemitosDetalle.Visible = false;
                    RemitosDivider.Visible = false;
                    //
                    GetAll();
                }
                else if (ComboData.SelectedIndex == 3)
                {
                    DataValue = "users";
                    LblTitleDatos.Text = "Usuarios";
                    // Ocultar o aparecer cosas
                    GridData.Visible = true;
                    GridRemitos.Visible = false;
                    GridRemitosDetalle.Visible = false;
                    RemitosDivider.Visible = false;
                    //
                    GetAll();
                }
                else if (ComboData.SelectedIndex == 4)
                {
                    DataValue = "tipoDeProductos";
                    LblTitleDatos.Text = "Tipo de Productos";
                    // Ocultar o aparecer cosas
                    GridData.Visible = true;
                    GridRemitos.Visible = false;
                    GridRemitosDetalle.Visible = false;
                    RemitosDivider.Visible = false;
                    //
                    GetAll();
                }
                else if (ComboData.SelectedIndex == 5)
                {
                    DataValue = "marcas";
                    LblTitleDatos.Text = "Marcas";
                    // Ocultar o aparecer cosas
                    GridData.Visible = true;
                    GridRemitos.Visible = false;
                    GridRemitosDetalle.Visible = false;
                    RemitosDivider.Visible = false;
                    //
                    GetAll();
                }
                else if (ComboData.SelectedIndex == 6)
                {
                    DataValue = "specs";
                    LblTitleDatos.Text = "Especificaciones";
                    // Ocultar o aparecer cosas
                    GridData.Visible = true;
                    GridRemitos.Visible = false;
                    GridRemitosDetalle.Visible = false;
                    RemitosDivider.Visible = false;
                    //
                    GetAll();
                }
                else if (ComboData.SelectedIndex == 7)
                {
                    DataValue = "provincias";
                    LblTitleDatos.Text = "Provincias";
                    // Ocultar o aparecer cosas
                    GridData.Visible = true;
                    GridRemitos.Visible = false;
                    GridRemitosDetalle.Visible = false;
                    RemitosDivider.Visible = false;
                    //
                    GetAll();
                }
                else if (ComboData.SelectedIndex == 8)
                {
                    DataValue = "localidades";
                    LblTitleDatos.Text = "Localidades";
                    // Ocultar o aparecer cosas
                    GridData.Visible = true;
                    GridRemitos.Visible = false;
                    GridRemitosDetalle.Visible = false;
                    RemitosDivider.Visible = false;
                    //
                    GetAll();
                }
                else if (ComboData.SelectedIndex == 9)
                {
                    DataValue = "remitos";
                    LblTitleDatos.Text = "Remitos";
                    // Ocultar o aparecer cosas
                    GridData.Visible = false;
                    GridRemitos.Visible = true;
                    GridRemitosDetalle.Visible = true;
                    RemitosDivider.Visible = true;
                    //
                    GetAll();
                }
            }
        }

        private void RadioEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioEnabled.Checked == true)
            {
                GetAll();
            }
        }

        private void RadioDisabled_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioDisabled.Checked == true)
            {
                GetAll();
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            bool editando = false;
            IUnitOfWork unitOfWork = new UnitOfWork();

            if (DataValue == "productos")
            {
                var createProductView = new CreateProductView(unitOfWork, editando);
                createProductView.ShowDialog();
            }
            else if (DataValue == "proveedores")
            {
                var createProovedorView = new CreateProveedorView(unitOfWork, editando);
                createProovedorView.ShowDialog();
            }
            else if (DataValue == "users")
            {
                var createUserView = new CreateUserView(unitOfWork, editando);
                createUserView.ShowDialog();
            }
            else if (DataValue == "cuentas")
            {
                var createAccount = new CreateAccount(unitOfWork, editando);
                createAccount.ShowDialog();
            }
            else if (DataValue == "specs")
            {
                var dataSend = "specs";
                var createDataView = new CreateDataView(dataSend, unitOfWork);
                createDataView.ShowDialog();
            }
            else if (DataValue == "marcas")
            {
                var dataSend = "marcas";
                var createDataView = new CreateDataView(dataSend, unitOfWork);
                createDataView.ShowDialog();
            }
            else if (DataValue == "tipoDeProductos")
            {
                var dataSend = "tipoDeProductos";
                var createDataView = new CreateDataView(dataSend, unitOfWork);
                createDataView.ShowDialog();
            }
            else if (DataValue == "provincias")
            {
                var dataSend = "provincia";
                var createDataView = new CreateDataView(dataSend, unitOfWork);
                createDataView.ShowDialog();
            }
            else if (DataValue == "localidades")
            {
                var dataSend = "localidad";
                var createDataView = new CreateDataView(dataSend, unitOfWork);
                createDataView.ShowDialog();
            }
            GetAll();
        }

        private void MasterDataView_Activated(object sender, EventArgs e)
        {
           
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

        private void CheckCboProveedor_CheckedChanged(object sender, EventArgs e)
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

        private void CheckCboProvincia_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckCboProvincia.Checked == true)
            {
                ComboProvincia.Enabled = true;
            }
            else if (CheckCboProvincia.Checked == false)
            {
                ComboProvincia.Enabled = false;
            }
        }

        private void CheckCboLocalidad_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckCboLocalidad.Checked == true)
            {
                ComboLocalidad.Enabled = true;
            }
            else if (CheckCboLocalidad.Checked == false)
            {
                ComboLocalidad.Enabled = false;
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

        private void CheckTipoUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckTipoUsuario.Checked == true)
            {
                ComboTipoUsuario.Enabled = true;
            }
            else if (CheckUsuarios.Checked == false)
            {
                ComboTipoUsuario.Enabled = false;
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            bool editando = true;
            IUnitOfWork unitOfWork = new UnitOfWork();
            var idSeleccionado = Convert.ToInt32(GridData.CurrentRow.Cells[0].Value);
            //
            if (DataValue == "productos")
            {
                var createProductViewEdit = new CreateProductView(unitOfWork, editando, idSeleccionado);
                createProductViewEdit.ShowDialog();
            }
            else if (DataValue == "proveedores")
            {
                var createProveedorViewEdit = new CreateProveedorView(unitOfWork, editando, idSeleccionado);
                createProveedorViewEdit.ShowDialog();
            }
            else if (DataValue == "users")
            {
                var createUserView = new CreateUserView(unitOfWork, editando, idSeleccionado);
                createUserView.ShowDialog();
            }
            else if (DataValue == "cuentas")
            {
                var createAccount = new CreateAccount(unitOfWork, editando, idSeleccionado);
                createAccount.ShowDialog();
            }
        }

        private void GridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //idSeleccionado = (int)GridData.CurrentRow.Cells["Id"].Value;
        }

        private async void BtnEna_Click(object sender, EventArgs e)
        {
            var idSeleccionado = Convert.ToInt32(GridData.CurrentRow.Cells[0].Value);
            if (DataValue == "productos")
            {
                #region ProductoHabilitado
                var productoEdit = unitOfWork.ProductoRepository.GetByID(idSeleccionado);
                Producto producto = new Producto()
                {
                    // Al modificar un producto, se le debe mantener su id
                    Id = (int)idSeleccionado,
                    TipoProductoId = (int)GridData.CurrentRow.Cells["TipoProductoId"].Value,
                    MarcaId = (int)GridData.CurrentRow.Cells["MarcaId"].Value,
                    SPECId = (int)GridData.CurrentRow.Cells["SPECId"].Value,
                    ProveedorId = (int)GridData.CurrentRow.Cells["ProveedorId"].Value,
                    Detalles = (string)GridData.CurrentRow.Cells["Detalles"].Value,
                    PrecioBulto = (decimal)GridData.CurrentRow.Cells["PrecioBulto"].Value,
                    CantidadBulto = (int)GridData.CurrentRow.Cells["CantidadBulto"].Value,
                    Ganancia = (int)GridData.CurrentRow.Cells["Ganancia"].Value,
                    PVP = (decimal)GridData.CurrentRow.Cells["PVP"].Value,
                    Stock = (int)GridData.CurrentRow.Cells["Stock"].Value,
                    Modificacion = DateTime.Now,
                    Visible = true,
                    UsuarioId = ClasesCompartidas.UserId,
                    Imagen = (byte[]?)GridData.CurrentRow.Cells["Imagen"].Value,
                };
                try
                {
                    new ModelsValidator().Validate(producto);

                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea Habilitar este producto?", "Habilitar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.ProductoRepository.Update(producto);
                        unitOfWork.Save();
                    }

                    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    ClasesCompartidas.DesProductList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(false));

                    GetAll();

                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
            else if (DataValue == "proveedores")
            {
                #region ProveedorHabilitar
                var proveedorHab = unitOfWork.ProveedorRepository.GetByID(idSeleccionado);
                proveedorHab.Visible = true;
                proveedorHab.Modificacion = DateTime.Now;
                proveedorHab.UsuarioId = ClasesCompartidas.UserId;
                try
                {
                    new ModelsValidator().Validate(proveedorHab);

                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea Habilitar al proveedor {proveedorHab.Nombre}?", "Habilitar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.ProveedorRepository.Update(proveedorHab);
                        unitOfWork.Save();
                    }

                    ClasesCompartidas.proveedoresList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(true));
                    ClasesCompartidas.DesProveedorList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(false));

                    GetAll();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
            else if (DataValue == "users")
            {
                #region UsuarioHabilitar
                var usuarioHab = unitOfWork.UsuarioRepository.GetByID(idSeleccionado);
                usuarioHab.Visible = true;
                usuarioHab.Modificacion = DateTime.Now;
                try
                {
                    new ModelsValidator().Validate(usuarioHab);

                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea Habilitar al proveedor {usuarioHab.User}?", "Habilitar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.UsuarioRepository.Update(usuarioHab);
                        unitOfWork.Save();
                    }

                    ClasesCompartidas.usuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(true));
                    ClasesCompartidas.DesUsuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(false));

                    GetAll();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
            else if (DataValue == "cuentas")
            {
                #region UsuarioHabilitar
                var cuentaHab = unitOfWork.CuentaRepository.GetByID(idSeleccionado);
                cuentaHab.Visible = true;
                cuentaHab.Modificacion = DateTime.Now;
                try
                {
                    new ModelsValidator().Validate(cuentaHab);

                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea Habilitar la cuenta {cuentaHab.Nombre}?", "Habilitar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.CuentaRepository.Update(cuentaHab);
                        unitOfWork.Save();
                    }

                    ClasesCompartidas.cuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(true));
                    ClasesCompartidas.DesCuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(false));

                    GetAll();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                #endregion

            }
        }

        private async void BtnDis_Click(object sender, EventArgs e)
        {
            var idSeleccionado = Convert.ToInt32(GridData.CurrentRow.Cells[0].Value);
            //
            // Se efectua uno u otro código, en función de lo visualizado en el DataGrid
            //
            if (DataValue == "productos")
            {
                #region ProductoDeshabilitar
                Producto producto = new Producto()
                {
                    // Al modificar un producto, se le debe mantener su id
                    Id = (int)idSeleccionado,
                    TipoProductoId = (int)GridData.CurrentRow.Cells["TipoProductoId"].Value,
                    MarcaId = (int)GridData.CurrentRow.Cells["MarcaId"].Value,
                    SPECId = (int)GridData.CurrentRow.Cells["SPECId"].Value,
                    ProveedorId = (int?)GridData.CurrentRow.Cells["ProveedorId"].Value,
                    Detalles = (string)GridData.CurrentRow.Cells["Detalles"].Value,
                    PrecioBulto = (decimal)GridData.CurrentRow.Cells["PrecioBulto"].Value,
                    CantidadBulto = (int)GridData.CurrentRow.Cells["CantidadBulto"].Value,
                    Ganancia = (int)GridData.CurrentRow.Cells["Ganancia"].Value,
                    PVP = (decimal)GridData.CurrentRow.Cells["PVP"].Value,
                    Stock = (int)GridData.CurrentRow.Cells["Stock"].Value,
                    Modificacion = DateTime.Now,
                    Visible = false,
                    UsuarioId = ClasesCompartidas.UserId,
                    Imagen = (byte[]?)GridData.CurrentRow.Cells["Imagen"].Value,
                };
                try
                {
                    new ModelsValidator().Validate(producto);

                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea Deshabilitar este producto?", "Deshabilitar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.ProductoRepository.Update(producto);
                        unitOfWork.Save();
                    }

                    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    ClasesCompartidas.DesProductList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(false));

                    GetAll();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                #endregion

            }
            else if (DataValue == "proveedores")
            {
                #region ProveedorDeshabilitar
                var proveedorDes = unitOfWork.ProveedorRepository.GetByID(idSeleccionado);
                proveedorDes.Visible = false;
                proveedorDes.Modificacion = DateTime.Now;
                proveedorDes.UsuarioId = ClasesCompartidas.UserId;
                try
                {
                    new ModelsValidator().Validate(proveedorDes);

                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea Deshabilitar al proveedor {proveedorDes.Nombre}?", "Deshabilitar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.ProveedorRepository.Update(proveedorDes);
                        unitOfWork.Save();
                    }

                    ClasesCompartidas.proveedoresList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(true));
                    ClasesCompartidas.DesProveedorList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(false));

                    GetAll();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
            else if (DataValue == "users")
            {
                #region UsuarioDeshabilitar
                var usuarioDes = unitOfWork.UsuarioRepository.GetByID(idSeleccionado);
                usuarioDes.Visible = false;
                usuarioDes.Modificacion = DateTime.Now;
                try
                {
                    new ModelsValidator().Validate(usuarioDes);

                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea Deshabilitar al usuario {usuarioDes.Nombre}?", "Deshabilitar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.UsuarioRepository.Update(usuarioDes);
                        unitOfWork.Save();
                    }
                    //
                    ClasesCompartidas.usuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(true));
                    ClasesCompartidas.DesUsuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(false));
                    //
                    GetAll();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
            else if (DataValue == "cuentas")
            {
                #region CuentaDeshabilitar
                var cuentaDes = unitOfWork.CuentaRepository.GetByID(idSeleccionado);
                cuentaDes.Visible = false;
                cuentaDes.Modificacion = DateTime.Now;
                try
                {
                    new ModelsValidator().Validate(cuentaDes);

                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea Deshabilitar la cuenta {cuentaDes.Nombre}?", "Deshabilitar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.CuentaRepository.Update(cuentaDes);
                        unitOfWork.Save();
                    }
                    //
                    ClasesCompartidas.cuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(true));
                    ClasesCompartidas.DesCuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(false));
                    //
                    GetAll();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                #endregion

            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            var idSeleccionado = Convert.ToInt32(GridData.CurrentRow.Cells[0].Value);
            //
            if (DataValue == "productos")
            {
                #region ProductoEliminado
                var producto = unitOfWork.ProductoRepository.GetByID(idSeleccionado);
                var result = MessageBox.Show($"¿Está seguro que desea eliminar el producto seleccionado?", "Eliminar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    unitOfWork.ProductoRepository.Delete(producto.Id);
                    unitOfWork.Save();
                    ClasesCompartidas.DesProductList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(false));
                    GetAll();
                }
                #endregion

            }
            else if (DataValue == "proveedores")
            {
                #region ProveedorEliminado
                var proveedor = unitOfWork.ProveedorRepository.GetByID(idSeleccionado);
                var result = MessageBox.Show($"¿Está seguro que desea eliminar el proveedor {proveedor.Nombre}?", "Eliminar Proveedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    unitOfWork.ProveedorRepository.Delete(proveedor.Id);
                    unitOfWork.Save();
                    ClasesCompartidas.DesProveedorList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(false));
                    GetAll();
                }
                #endregion
            }
            else if (DataValue == "users")
            {
                #region UsuarioEliminado
                var usuario = unitOfWork.UsuarioRepository.GetByID(idSeleccionado);
                var result = MessageBox.Show($"¿Está seguro que desea eliminar el usuario {usuario.User}?", "Eliminar Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    unitOfWork.UsuarioRepository.Delete(usuario.Id);
                    unitOfWork.Save();
                    ClasesCompartidas.DesUsuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(false));
                    GetAll();
                }
                #endregion
            }
            else if (DataValue == "cuentas")
            {
                #region CuentaEliminado
                var cuenta = unitOfWork.CuentaRepository.GetByID(idSeleccionado);
                var result = MessageBox.Show($"¿Está seguro que desea eliminar el usuario {cuenta.Nombre}?", "Eliminar Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    unitOfWork.CuentaRepository.Delete(cuenta.Id);
                    unitOfWork.Save();
                    ClasesCompartidas.DesCuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(false));
                    GetAll();
                }
                #endregion

            }
        }

        private void BtnFiltro_Click(object sender, EventArgs e)
        {
            _filter = true;
            FilterComboBoxes();
            ClasesCompartidas.FilterSend.DataSource = Filter;
        }

        private async void FilterComboBoxes()
        {
            // Elementos por los cuales filtrar
            // Tipo
            if (CheckCboTipo.Checked == true)
            {
                FilterTipoId = (int)ComboTipoProducto.SelectedValue;
            }
            else
            {
                FilterTipoId = 0;
            }
            // Marca
            if (CheckCboMarca.Checked == true)
            {
                FilterMarcaId = (int)ComboMarcas.SelectedValue;
            }
            else
            {
                FilterMarcaId = 0;
            }
            // Spec
            if (CheckCboSPEC.Checked == true)
            {
                FilterSpecId = (int)ComboSpec.SelectedValue;
            }
            else
            {
                FilterSpecId = 0;
            }
            // Proveedor
            if (CheckCboProveedor.Checked == true)
            {
                FilterSpecId = (int)ComboSpec.SelectedValue;
            }
            else
            {
                FilterSpecId = 0;
            }
            // Usuario
            if (CheckUsuarios.Checked == true)
            {
                FilterUsuarioId = (int)ComboUsuarios.SelectedValue;
            }
            else
            {
                FilterUsuarioId = 0;
            }
            // Tipo de Usuario
            if (CheckTipoUsuario.Checked == true)
            {
                FilterTipoUsuarioId = (int)ComboTipoUsuario.SelectedValue;
            }
            else
            {
                FilterTipoUsuarioId = 0;
            }
            // Provincia
            if (CheckCboProvincia.Checked == true)
            {
                FilterProvinciaId = (int)ComboProvincia.SelectedValue;
            }
            else
            {
                FilterProvinciaId = 0;
            }
            // Localidad
            if (CheckCboLocalidad.Checked == true)
            {
                FilterLocalidadId = (int)ComboLocalidad.SelectedValue;
            }
            else
            {
                FilterLocalidadId = 0;
            }
            // Fecha
            if (CheckFecha.Checked == true)
            {
                FilterFecha = 1;
            }
            else
            {
                FilterFecha = 0;
            }
            //
            // Filtradores
            if (DataValue == "productos")
            {
                #region FiltradoDataProductos
                Filter.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario), filter:
                /*♥Con Todos los Valores*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin Rango de Fecha*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Sin Usuario*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin Tipo*/  FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Sin RDF y Usuario*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Visible.Equals(true) :
                /*Sin RDF y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuarioId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Sin RDF Y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Sin RDF y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Sin RDF y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*♥Sin USUARIO y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin USUARIO y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin USUARIO y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin USUARIO y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 && FilterFecha != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Sin PROVEEDOR y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin PROVEEDOR y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin PROVEEDOR y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Sin SPEC y Marca*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin SPEC y Tipo*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Sin MARCA y TIPO*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Sin FECHA, USUARIO y Proveedor*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.Visible.Equals(true) :
                /*Sin FECHA, USUARIO y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.Visible.Equals(true) :
                /*Sin FECHA, USUARIO y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Visible.Equals(true) :
                /*Sin FECHA, USUARIO y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterProveedorId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Visible.Equals(true) :
                /*♥Sin USUARIO, PROVEEDOR y Spec*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin USUARIO, PROVEEDOR y Marca*/ FilterTipoId != 0 && FilterSpecId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.SPECId.Equals(FilterSpecId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin USUARIO, PROVEEDOR y Tipo*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterFecha != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Sin PROVEEDOR, SPEC y Fecha*/ FilterTipoId != 0 && FilterMarcaId != 0 && FilterUsuarioId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Sin PROVEEDOR, SPEC y Marca*/ FilterTipoId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin PROVEEDOR, SPEC y Tipo*/ FilterMarcaId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Sin SPEC, MARCA y Fecha*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Sin SPEC, MARCA y Usuario*/ FilterTipoId != 0 && FilterProveedorId != 0 && FilterFecha != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.ProveedorId.Equals(FilterProveedorId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin SPEC, MARCA y Tipo*/  FilterProveedorId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Sin MARCA, TIPO y Fecha*/ FilterSpecId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Sin MARCA, TIPO y Usuario*/  FilterSpecId != 0 && FilterProveedorId != 0 && FilterFecha != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.ProveedorId.Equals(FilterProveedorId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Sin MARCA, TIPO y Proveedor*/ FilterSpecId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Sin TIPO, FECHA, y Proveedor*/ FilterMarcaId != 0 && FilterSpecId != 0 && FilterUsuarioId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.SPECId.Equals(FilterSpecId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Sin TIPO, FECHA y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterUsuarioId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.ProveedorId.Equals(FilterProveedorId) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Sin TIPO, USUARIO y Spec*/ FilterMarcaId != 0 && FilterProveedorId != 0 && FilterFecha != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.MarcaId.Equals(FilterMarcaId) && q.MarcaId.Equals(FilterMarcaId) && q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Filtrando FECHA y Usuario*/ FilterFecha != 0 && FilterUsuarioId != 0 ? q => q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Filtrando FECHA y Proveedor*/ FilterFecha != 0 && FilterProveedorId != 0 ? q => q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.ProveedorId.Equals(FilterProveedorId) && q.Visible.Equals(true) :
                /*Filtrando FECHA y Spec*/ FilterFecha != 0 && FilterSpecId != 0 ? q => q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.SPECId.Equals(FilterSpecId) && q.Visible.Equals(true) :
                /*Filtrando FECHA y Marca*/ FilterFecha != 0 && FilterMarcaId != 0 ? q => q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.MarcaId.Equals(FilterMarcaId) && q.Visible.Equals(true) :
                /*Filtrando FECHA y Tipo*/ FilterFecha != 0 && FilterTipoId != 0 ? q => q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.TipoProductoId.Equals(FilterTipoId) && q.Visible.Equals(true) :
                /*♥Filtrando por USUARIO y Proveedor*/ FilterUsuarioId != 0 && FilterProveedorId != 0 ? q => q.UsuarioId.Equals(FilterUsuarioId) && q.ProveedorId.Equals(FilterProveedorId) && q.Visible.Equals(true) :
                /*Filtrando por USUARIO y Spec*/ FilterUsuarioId != 0 && FilterSpecId != 0 ? q => q.UsuarioId.Equals(FilterUsuarioId) && q.SPECId.Equals(FilterSpecId) && q.Visible.Equals(true) :
                /*Filtrando por USUARIO y Marca*/ FilterUsuarioId != 0 && FilterMarcaId != 0 ? q => q.UsuarioId.Equals(FilterUsuarioId) && q.MarcaId.Equals(FilterMarcaId) && q.Visible.Equals(true) :
                /*Filtrando por USUARIO y Tipo*/ FilterUsuarioId != 0 && FilterTipoId != 0 ? q => q.UsuarioId.Equals(FilterUsuarioId) && q.TipoProductoId.Equals(FilterTipoId) && q.Visible.Equals(true) :
                /*♥Filtrando por PROVEEDOR y Spec*/ FilterProveedorId != 0 && FilterSpecId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.SPECId.Equals(FilterSpecId) && q.Visible.Equals(true) :
                /*Filtrando por PROVEEDOR y Marca*/ FilterProveedorId != 0 && FilterMarcaId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.TipoProductoId.Equals(FilterTipoId) && q.Visible.Equals(true) :
                /*Filtrando por PROVEEDOR y Tipo*/ FilterProveedorId != 0 && FilterTipoId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.TipoProductoId.Equals(FilterTipoId) && q.Visible.Equals(true) :
                /*Filtrando por SPEC y Marca*/ FilterSpecId != 0 && FilterMarcaId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.MarcaId.Equals(FilterMarcaId) && q.Visible.Equals(true) :
                /*Filtrando por SPEC y Tipo*/ FilterSpecId != 0 && FilterTipoId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.TipoProductoId.Equals(FilterTipoId) && q.Visible.Equals(true) :
                /*Filtrando por MARCA y Tipo*/ FilterMarcaId != 0 && FilterTipoId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.TipoProductoId.Equals(FilterTipoId) && q.Visible.Equals(true) :
                /*♥Filtrando por FECHA*/ FilterFecha != 0 ? q => q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*♥Filtranndo por USUARIO*/ FilterUsuarioId != 0 ? q => q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*♥Filtrando por SPEC*/ FilterSpecId != 0 ? q => q.SPECId.Equals(FilterSpecId) && q.Visible.Equals(true) :
                /*♥Filtrando por PROVEEDOR*/ FilterProveedorId != 0 ? q => q.ProveedorId.Equals(FilterProveedorId) && q.Visible.Equals(true) :
                /*♥Filtrando por MARCA*/ FilterMarcaId != 0 ? q => q.MarcaId.Equals(FilterMarcaId) && q.Visible.Equals(true) :
                /*♥Filtrando por TIPO ~ TODO*/ FilterTipoId != 0 ? q => q.TipoProductoId.Equals(FilterTipoId) && q.Visible.Equals(true) : q => q.Visible.Equals(true));
                #endregion
            }
            else if (DataValue == "proveedores")
            {
                #region FiltradoDataProveedores
                Filter.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter:
                /*Filtrado por todos los valores permitidos*/ FilterLocalidadId != 0 && FilterProvinciaId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.LocalidadId.Equals(FilterLocalidadId) && v.UsuarioId.Equals(FilterUsuarioId) && v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.Visible.Equals(true) :
                /*Filtrado por Localidad, Provincia y Usuario*/ FilterLocalidadId != 0 && FilterProvinciaId != 0 && FilterUsuarioId != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.LocalidadId.Equals(FilterLocalidadId) && v.UsuarioId.Equals(FilterUsuarioId) && v.Visible.Equals(true) :
                /*Filtrado por Localidad, Provincia y Fecha*/ FilterLocalidadId != 0 && FilterProvinciaId != 0 && FilterFecha != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.LocalidadId.Equals(FilterLocalidadId) && v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.Visible.Equals(true) :
                /*Filtrado por Localidad, Usuario y Fecha*/ FilterLocalidadId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? v => v.LocalidadId.Equals(FilterLocalidadId) && v.UsuarioId.Equals(FilterUsuarioId) && v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.Visible.Equals(true) :
                /*♥Filtrado por FECHA y Usuario*/ FilterFecha != 0 && FilterUsuarioId != 0 ? v => v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.UsuarioId.Equals(FilterUsuarioId) && v.Visible.Equals(true) :
                /*Filtrado por FECHA y Provincia*/ FilterFecha != 0 && FilterProvinciaId != 0 ? v => v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.ProvinciaId.Equals(FilterProvinciaId) && v.Visible.Equals(true) :
                /*Filtrado por FECHA y Localidad*/ FilterFecha != 0 && FilterLocalidadId != 0 ? v => v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.LocalidadId.Equals(FilterLocalidadId) && v.Visible.Equals(true) :
                /*♥Filtrado por USUARIO y Provincia*/ FilterUsuarioId != 0 && FilterProvinciaId != 0 ? v => v.UsuarioId.Equals(FilterUsuarioId) && v.ProvinciaId.Equals(FilterProvinciaId) && v.Visible.Equals(true) :
                /*Filtrado por USUARIO y Localidad*/ FilterUsuarioId != 0 && FilterLocalidadId != 0 ? v => v.UsuarioId.Equals(FilterUsuarioId) && v.LocalidadId.Equals(FilterLocalidadId) && v.Visible.Equals(true) :
                /*♥Filtrado por PROVINCIA y LOCALIDAD*/ FilterProvinciaId != 0 && FilterLocalidadId != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.LocalidadId.Equals(FilterLocalidadId) && v.Visible.Equals(true) :
                /*Filtrado por FECHA*/ FilterFecha != 0 ? q => q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Filtrado por USUARIO*/ FilterUsuarioId != 0 ? q => q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Filtrado por PROVINCIA*/ FilterProvinciaId != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.Visible.Equals(true) :
                /*Filtrado por LOCALIDAD*/ FilterLocalidadId != 0 ? v => v.LocalidadId.Equals(FilterLocalidadId) && v.Visible.Equals(true) : v => v.Visible.Equals(true));
                #endregion
            }
            else if (DataValue == "cuentas")
            {
                #region FiltradoDataCuentas
                Filter.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter:
                /*Filtrado por todos los valores permitidos*/ FilterLocalidadId != 0 && FilterProvinciaId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.LocalidadId.Equals(FilterLocalidadId) && v.UsuarioId.Equals(FilterUsuarioId) && v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.Visible.Equals(true) :
                /*Filtrado por Localidad, Provincia y Usuario*/ FilterLocalidadId != 0 && FilterProvinciaId != 0 && FilterUsuarioId != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.LocalidadId.Equals(FilterLocalidadId) && v.UsuarioId.Equals(FilterUsuarioId) && v.Visible.Equals(true) :
                /*Filtrado por Localidad, Provincia y Fecha*/ FilterLocalidadId != 0 && FilterProvinciaId != 0 && FilterFecha != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.LocalidadId.Equals(FilterLocalidadId) && v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.Visible.Equals(true) :
                /*Filtrado por Localidad, Usuario y Fecha*/ FilterLocalidadId != 0 && FilterUsuarioId != 0 && FilterFecha != 0 ? v => v.LocalidadId.Equals(FilterLocalidadId) && v.UsuarioId.Equals(FilterUsuarioId) && v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.Visible.Equals(true) :
                /*♥Filtrado por FECHA y Usuario*/ FilterFecha != 0 && FilterUsuarioId != 0 ? v => v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.UsuarioId.Equals(FilterUsuarioId) && v.Visible.Equals(true) :
                /*Filtrado por FECHA y Provincia*/ FilterFecha != 0 && FilterProvinciaId != 0 ? v => v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.ProvinciaId.Equals(FilterProvinciaId) && v.Visible.Equals(true) :
                /*Filtrado por FECHA y Localidad*/ FilterFecha != 0 && FilterLocalidadId != 0 ? v => v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.LocalidadId.Equals(FilterLocalidadId) && v.Visible.Equals(true) :
                /*♥Filtrado por USUARIO y Provincia*/ FilterUsuarioId != 0 && FilterProvinciaId != 0 ? v => v.UsuarioId.Equals(FilterUsuarioId) && v.ProvinciaId.Equals(FilterProvinciaId) && v.Visible.Equals(true) :
                /*Filtrado por USUARIO y Localidad*/ FilterUsuarioId != 0 && FilterLocalidadId != 0 ? v => v.UsuarioId.Equals(FilterUsuarioId) && v.LocalidadId.Equals(FilterLocalidadId) && v.Visible.Equals(true) :
                /*♥Filtrado por PROVINCIA y LOCALIDAD*/ FilterProvinciaId != 0 && FilterLocalidadId != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.LocalidadId.Equals(FilterLocalidadId) && v.Visible.Equals(true) :
                /*Filtrado por FECHA*/ FilterFecha != 0 ? q => q.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && q.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && q.Visible.Equals(true) :
                /*Filtrado por USUARIO*/ FilterUsuarioId != 0 ? q => q.UsuarioId.Equals(FilterUsuarioId) && q.Visible.Equals(true) :
                /*Filtrado por PROVINCIA*/ FilterProvinciaId != 0 ? v => v.ProvinciaId.Equals(FilterProvinciaId) && v.Visible.Equals(true) :
                /*Filtrado por LOCALIDAD*/ FilterLocalidadId != 0 ? v => v.LocalidadId.Equals(FilterLocalidadId) && v.Visible.Equals(true) : v => v.Visible.Equals(true));
                #endregion
            }
            else if (DataValue == "users")
            {
                #region FiltradoDataUsuarios
                Filter.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter:
                /*Filtrado por todos los valores permitidos*/ FilterFecha != 0 && FilterTipoUsuarioId != 0 ? v => v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.TipoDeUsuarioId.Equals(FilterTipoUsuarioId) && v.Visible.Equals(true) :
                /*Filtrado por FECHA*/ FilterFecha != 0 ? v => v.Modificacion >= Convert.ToDateTime(FechaDesde.Text) && v.Modificacion <= Convert.ToDateTime(FechaHasta.Text) && v.Visible.Equals(true) :
                /*Filtrado por TIPOdeUSUARIO*/ FilterTipoUsuarioId != 0 ? v => v.TipoDeUsuarioId.Equals(FilterTipoUsuarioId) && v.Visible.Equals(true) : v => v.Visible.Equals(true));
                #endregion
            }
            //
            GridData.DataSource = Filter;
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

        private void BtnRecargar_Click(object sender, EventArgs e)
        {
            // Al hacer click en el botón recargar, todas las herramientas de filtrado
            // y el buscador, se reinician
            // Check's
            _filter = false;
            CheckCboLocalidad.Checked = false;
            CheckCboMarca.Checked = false;
            CheckCboProveedor.Checked = false;
            CheckCboProvincia.Checked = false;
            CheckCboSPEC.Checked = false;
            CheckCboTipo.Checked = false;
            CheckFecha.Checked = false;
            CheckTipoUsuario.Checked = false;
            CheckUsuarios.Checked = false;
            //
            GetComboData();
            //
            TxtBuscar.Text = " ";
            //
            GridData.DataSource = listData;
        }

        private void BtnDetalles_Click(object sender, EventArgs e)
        {
            int IdCuenta = (int)GridData.CurrentRow.Cells["Id"].Value;
            //
            //
            var baseLoading = new BaseLoading(unitOfWork, IdCuenta);
        }

        private async void BtnGuardarPorcentaje_Click(object sender, EventArgs e)
        {
            var Ganancia = Convert.ToDecimal(TxtPVP.Text);
            //
            if (TxtPVP.Text.Length < 1)
            {
                MessageBox.Show("Es necesario completar el campo 'Ganancia'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Ganancia < 1)
            {
                MessageBox.Show("El número ingresado no es apropiado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var nuevoPorcentaje = unitOfWork.PorcentajeGananciaRepository.GetByID(1);
                nuevoPorcentaje.Porcentaje = Ganancia;
                //
                try
                {
                    new ModelsValidator().Validate(nuevoPorcentaje);
                    //
                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea cambiar el porcentaje de ganancia por defecto?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.PorcentajeGananciaRepository.Update(nuevoPorcentaje);
                        unitOfWork.Save();
                        //
                        MessageBox.Show($"¡El porcentaje de ganancia por defecto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                //
                ClasesCompartidas.PorcentajeGanancia.DataSource = await unitOfWork.PorcentajeGananciaRepository.GetAllAsync();
                //
                GetPorcentaje();
            }
        }

        private void TxtPVP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten caracteres númericos en este campo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void IcoBtnCalcular_Click(object sender, EventArgs e)
        {
            DialogResult respuesta;
            //
            bool _selection = false;
            //
            var _ganancia = Convert.ToDecimal(TxtPVP.Text);
            //
            var IdProducto = (int)GridData.CurrentRow.Cells["Id"].Value;
            if (TxtPVP.Text.Length < 1)
            {
                MessageBox.Show("Es necesario completar el campo 'Ganancia'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (_ganancia < 1)
            {
                MessageBox.Show("El número ingresado no es apropiado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (RadioSelection.Checked == true)
                {
                    respuesta = MessageBox.Show($"¿Está seguro que desea cambiar el precio del producto seleccionado?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        _selection = true;
                    }
                    ////
                    //var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                    ////
                    //var precioNuevo = productoNewPrecio.PrecioUnidad + ((productoNewPrecio.PrecioUnidad * Ganancia) / 100);
                    //productoNewPrecio.PVP = precioNuevo;
                    //productoNewPrecio.Porcentaje = Ganancia;
                    ////
                    //try
                    //{
                    //    new ModelsValidator().Validate(productoNewPrecio);
                    //    //
                    //    if (respuesta == DialogResult.Yes)
                    //    {
                    //        unitOfWork.ProductoRepository.Update(productoNewPrecio);
                    //        unitOfWork.Save();
                    //        //
                    //        // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                    //        // base de datos
                    //        ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    //        GetAll();
                    //        MessageBox.Show($"¡El precio del producto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    Debug.Print(ex.Message);
                    //    Debug.Print(ex.InnerException?.Message);
                    //    MessageBox.Show(ex.Message);
                    //}
                }
                else
                {
                    respuesta = MessageBox.Show($"¿Está seguro que desea cambiar el precio de los productos visualizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        _selection = false;

                        //foreach (DataGridViewRow row in GridData.Rows)
                        //{
                        //    var IdProducto = (int)row.Cells["Id"].Value;
                        //    //
                        //    var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                        //    //
                        //    var precioNuevo = productoNewPrecio.PrecioUnidad + ((productoNewPrecio.PrecioUnidad * Ganancia) / 100);
                        //    productoNewPrecio.PVP = precioNuevo;
                        //    productoNewPrecio.Porcentaje = Ganancia;
                        //    //
                        //    try
                        //    {
                        //        new ModelsValidator().Validate(productoNewPrecio);
                        //        //
                        //        unitOfWork.ProductoRepository.Update(productoNewPrecio);
                        //        unitOfWork.Save();
                        //        //
                        //        // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                        //        // base de datos
                        //        ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Debug.Print(ex.Message);
                        //        Debug.Print(ex.InnerException?.Message);
                        //        MessageBox.Show(ex.Message);
                        //    }
                        //}
                        //
                    }
                }
                //
                if (respuesta == DialogResult.Yes)
                {
                    var saveView = new SaveView(unitOfWork, _ganancia, IdProducto, _selection, _filter);
                    saveView.ShowDialog();
                }
                //
                GridData.DataSource = ClasesCompartidas.productosList;
                //
                if (RadioSelection.Checked == true)
                {
                    MessageBox.Show($"¡El precio del producto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"¡El precio de los productos visuzalizados se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //
                _filter = false;
            }
        }

        private async void BtnSumar_Click(object sender, EventArgs e)
        {
            var StockSum = Convert.ToInt32(TxtStock.Text);
            //    
            if (RadioSelection.Checked == true)
            {
                var IdProducto = (int)GridData.CurrentRow.Cells["Id"].Value;
                //
                var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                //
                var stockSumado = productoNewPrecio.Stock + StockSum;
                productoNewPrecio.Stock = stockSumado;
                //
                try
                {
                    new ModelsValidator().Validate(productoNewPrecio);
                    //
                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea restar la cantidad de {StockSum} al stock del producto seleccionado?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.ProductoRepository.Update(productoNewPrecio);
                        unitOfWork.Save();
                        //
                        // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                        // base de datos
                        ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                        GetAll();
                        MessageBox.Show($"¡El stock del producto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea sumar la cantidad de {StockSum} de los productos visualizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in GridData.Rows)
                    {
                        var IdProducto = (int)row.Cells["Id"].Value;
                        //
                        var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                        //
                        var stockSumado = productoNewPrecio.Stock + StockSum;
                        productoNewPrecio.Stock = stockSumado;
                        //
                        try
                        {
                            new ModelsValidator().Validate(productoNewPrecio);
                            //
                            unitOfWork.ProductoRepository.Update(productoNewPrecio);
                            unitOfWork.Save();
                            //
                            // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                            // base de datos
                        }
                        catch (Exception ex)
                        {
                            Debug.Print(ex.Message);
                            Debug.Print(ex.InnerException?.Message);
                            MessageBox.Show(ex.Message);
                        }
                    }
                    //
                    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    GetAll();
                    MessageBox.Show($"¡El stock de los productos visuzalizados se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void BtnRestar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta;
            //
            var StockRest = Convert.ToInt32(TxtStock.Text);
            //    
            if (RadioSelection.Checked == true)
            {
                var IdProducto = (int)GridData.CurrentRow.Cells["Id"].Value;
                //
                var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                //
                if (productoNewPrecio.Stock >= StockRest)
                {
                    var stockRestado = productoNewPrecio.Stock - StockRest;
                    productoNewPrecio.Stock = stockRestado;
                    //
                    try
                    {
                        new ModelsValidator().Validate(productoNewPrecio);
                        //
                        respuesta = MessageBox.Show($"¿Está seguro que desea restar la cantidad de {StockRest} al stock del producto seleccionado?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            unitOfWork.ProductoRepository.Update(productoNewPrecio);
                            unitOfWork.Save();
                            //
                            // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                            // base de datos
                            ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                            GetAll();
                            MessageBox.Show($"¡El stock del producto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        Debug.Print(ex.InnerException?.Message);
                        MessageBox.Show(ex.Message);
                    }
                    //
                }
                else
                {
                    MessageBox.Show($"No ha sido posbile restar el stock del producto código:{productoNewPrecio.Id} la cantidad ingresada para restar, supera a la resgistrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                respuesta = MessageBox.Show($"¿Está seguro que desea restar la cantidad de {StockRest} de los productos visualizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in GridData.Rows)
                    {
                        var IdProducto = (int)row.Cells["Id"].Value;
                        //
                        var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                        //
                        if (productoNewPrecio.Stock >= StockRest)
                        {
                            var stockRestado = productoNewPrecio.Stock - StockRest;
                            productoNewPrecio.Stock = stockRestado;
                            //
                            try
                            {
                                new ModelsValidator().Validate(productoNewPrecio);
                                //         
                                unitOfWork.ProductoRepository.Update(productoNewPrecio);
                                unitOfWork.Save();
                            }
                            catch (Exception ex)
                            {
                                Debug.Print(ex.Message);
                                Debug.Print(ex.InnerException?.Message);
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No ha sido posbile restar el stock del producto código:{productoNewPrecio.Id} la cantidad ingresada para restar, supera a la resgistrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    GetAll();
                    MessageBox.Show($"¡El stock de los productos visuzalizados se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RadioSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioSelection.Checked == false)
            {
                RadioSelection.Checked = false;
                //
                RadioAll.Checked = true;
            }
            else
            {
                RadioSelection.Checked = true;
                //
                RadioAll.Checked = false;
            }
        }

        private void RadioAll_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioAll.Checked == false)
            {
                RadioSelection.Checked = true;
                //
                RadioAll.Checked = false;
            }
            else
            {
                RadioSelection.Checked = false;
                //
                RadioAll.Checked = true;
            }
        }

        private void BtnCalcularBultoPVP_Click(object sender, EventArgs e)
        {
            DialogResult respuesta;
            //
            bool _selection = false;
            //
            var _descuento = Convert.ToDecimal(TxtDescuento.Text);
            //
            var IdProducto = (int)GridData.CurrentRow.Cells["Id"].Value;
            if (TxtPVP.Text.Length < 1)
            {
                MessageBox.Show("Es necesario completar el campo 'Descuento'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (_descuento < 1)
            {
                MessageBox.Show("El número ingresado no es apropiado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (RadioSelection.Checked == true)
                {
                    respuesta = MessageBox.Show($"¿Está seguro que desea cambiar el precio por bulto del producto seleccionado?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        _selection = true;
                    }
                    ////
                    //var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                    ////
                    //var precioNuevo = productoNewPrecio.PrecioUnidad + ((productoNewPrecio.PrecioUnidad * Ganancia) / 100);
                    //productoNewPrecio.PVP = precioNuevo;
                    //productoNewPrecio.Porcentaje = Ganancia;
                    ////
                    //try
                    //{
                    //    new ModelsValidator().Validate(productoNewPrecio);
                    //    //
                    //    if (respuesta == DialogResult.Yes)
                    //    {
                    //        unitOfWork.ProductoRepository.Update(productoNewPrecio);
                    //        unitOfWork.Save();
                    //        //
                    //        // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                    //        // base de datos
                    //        ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    //        GetAll();
                    //        MessageBox.Show($"¡El precio del producto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    Debug.Print(ex.Message);
                    //    Debug.Print(ex.InnerException?.Message);
                    //    MessageBox.Show(ex.Message);
                    //}
                }
                else
                {
                    respuesta = MessageBox.Show($"¿Está seguro que desea cambiar el precio por bulto de los productos visualizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        _selection = false;

                        //foreach (DataGridViewRow row in GridData.Rows)
                        //{
                        //    var IdProducto = (int)row.Cells["Id"].Value;
                        //    //
                        //    var productoNewPrecio = unitOfWork.ProductoRepository.GetByID(IdProducto);
                        //    //
                        //    var precioNuevo = productoNewPrecio.PrecioUnidad + ((productoNewPrecio.PrecioUnidad * Ganancia) / 100);
                        //    productoNewPrecio.PVP = precioNuevo;
                        //    productoNewPrecio.Porcentaje = Ganancia;
                        //    //
                        //    try
                        //    {
                        //        new ModelsValidator().Validate(productoNewPrecio);
                        //        //
                        //        unitOfWork.ProductoRepository.Update(productoNewPrecio);
                        //        unitOfWork.Save();
                        //        //
                        //        // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                        //        // base de datos
                        //        ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Debug.Print(ex.Message);
                        //        Debug.Print(ex.InnerException?.Message);
                        //        MessageBox.Show(ex.Message);
                        //    }
                        //}
                        //
                    }
                }
                if (respuesta == DialogResult.Yes)
                {
                    var saveView = new SaveView(_descuento, IdProducto, _selection, _filter, unitOfWork);
                    saveView.ShowDialog();
                }
                //
                GridData.DataSource = ClasesCompartidas.productosList;
                //
                if (RadioSelection.Checked == true)
                {
                    MessageBox.Show($"¡El precio por bulto del producto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"¡El precio por bulto de los productos visuzalizados se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _filter = false;
            }

        }

        private async void BtnGuardarDescuento_Click(object sender, EventArgs e)
        {
            var Descuento = Convert.ToDecimal(TxtDescuento.Text);
            //
            if (TxtPVP.Text.Length < 1)
            {
                MessageBox.Show("Es necesario completar el campo 'Descuento'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Descuento < 1)
            {
                MessageBox.Show("El número ingresado no es apropiado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var nuevoPorcentaje = unitOfWork.PorcentajeGananciaRepository.GetByID(2);
                nuevoPorcentaje.Porcentaje = Descuento;
                //
                try
                {
                    new ModelsValidator().Validate(nuevoPorcentaje);
                    //
                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea cambiar el porcentaje de descuento por defecto?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        unitOfWork.PorcentajeGananciaRepository.Update(nuevoPorcentaje);
                        unitOfWork.Save();
                        //
                        MessageBox.Show($"¡El porcentaje de descuento por defecto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    Debug.Print(ex.InnerException?.Message);
                    MessageBox.Show(ex.Message);
                }
                //
                ClasesCompartidas.PorcentajeGanancia.DataSource = await unitOfWork.PorcentajeGananciaRepository.GetAllAsync();
                //
                GetPorcentaje();
            }

        }

        private void BtnRemito_Click(object sender, EventArgs e)
        {
            if (DataValue == "proveedores")
            {
                var IdProvedor = (int)GridData.CurrentRow.Cells["Id"].Value;
                //
                var remitoView = new RemitoView(unitOfWork, IdProvedor);
                remitoView.ShowDialog();
            }
            else
            {
                IUnitOfWork unitOfWork = new UnitOfWork();
                var remitoView = new RemitoView(unitOfWork);
                remitoView.ShowDialog();
            }
        }

        private void GridRemitos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Ajustes de columnas
            foreach (DataGridViewColumn columna in GridRemitos.Columns)
            {
                if (columna.Name == "Importe")
                {
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                }
                //
                if (columna.Name == "TipoComprobante")
                {
                    columna.HeaderText = "Tipo";
                }
                //
                if (columna.Name == "CantidadProductos")
                {
                    columna.HeaderText = "Cant. Productos";
                }

            }
            // Columnas Ocultas
            foreach (DataGridViewColumn columna in GridRemitos.Columns)
            {
                if (columna.Name == "ProveedorId")
                {
                    columna.Visible = false;
                }
                //
                if (columna.Name == "UsuarioId")
                {
                    columna.Visible = false;
                }
            }
        }

        private void GridRemitosDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Ajustes de columnas
            foreach (DataGridViewColumn columna in GridRemitosDetalle.Columns)
            {
                if (columna.Name == "TipoProducto")
                {
                    columna.HeaderText = "Tipo";
                }
                //
                if (columna.Name == "CantidadBultos")
                {
                    columna.HeaderText = "Cant. de Bultos";
                }
                //
                if (columna.Name == "CantidadXBultos")
                {
                    columna.HeaderText = "Cant. por Bultos";
                }
                //
                if (columna.Name == "PrecioBulto")
                {
                    columna.HeaderText = "P. Bulto";
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                }
                //
                if (columna.Name == "PrecioUnitario")
                {
                    columna.HeaderText = "P. Unitario";
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                }
                //
                if (columna.Name == "CantidadTotal")
                {
                    columna.HeaderText = "Cant. Total";
                }
                //
                if (columna.Name == "PrecioTotal")
                {
                    columna.HeaderText = "P. Total";
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                }
            }
            //
            foreach (DataGridViewColumn columna in GridRemitosDetalle.Columns)
            {
                if (columna.Name == "TipoProductoId")
                {
                    columna.Visible = false;
                }
                //
                if (columna.Name == "MarcaId")
                {
                    columna.Visible = false;
                }
                //
                if (columna.Name == "SPECId")
                {
                    columna.Visible = false;
                }
                //
                if (columna.Name == "Remito")
                {
                    columna.Visible = false;
                }
                //
                if (columna.Name == "RemitoId")
                {
                    columna.Visible = false;
                }
            }
        }

        private void GridRemitos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var IdRemito = (int)GridRemitos.CurrentRow.Cells["Id"].Value;
            //
            var miniLoading = new MiniLoading(unitOfWork, IdRemito);
            miniLoading.ShowDialog();
            //
            GridRemitosDetalle.DataSource = ClasesCompartidas.FilterRemito;
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (DataValue == "productos")
            {
                productos.DataSource = listData;
                ProdcutosViewReport prodcutosViewReport = new ProdcutosViewReport(this.productos);
                prodcutosViewReport.ShowDialog();
            }
            else if (DataValue == "proveedores")
            {
                proveedores.DataSource = listData;
                ProveedoresViewReport proveedoresViewReport = new ProveedoresViewReport(this.proveedores);
                proveedoresViewReport.ShowDialog();
            }
            else if (DataValue == "cuentas")
            {
                cuentas.DataSource = listData;
                CuentasViewReport cuentasViewReport = new CuentasViewReport(this.cuentas);
                cuentasViewReport.ShowDialog();
            }
            else if (DataValue == "usuarios")
            {
                usuarios.DataSource = listData;
                UsuariosViewReport usuariosViewReport = new UsuariosViewReport(this.usuarios);
                usuariosViewReport.ShowDialog();
            }
        }
    }
}
