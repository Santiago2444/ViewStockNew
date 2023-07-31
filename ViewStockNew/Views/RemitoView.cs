using Microsoft.EntityFrameworkCore;
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
using ViewStockNew.ViewReport;

namespace ViewStockNew.Views
{
    public partial class RemitoView : Form
    {
        IUnitOfWork unitOfWork;
        //IEnumerable<RemitoDetalle> remitosDetalles;
        BindingSource remitosDetalles = new BindingSource();
        BindingSource Filter = new BindingSource();
        private int idProvedor;
        private int FilterTipoId;
        private int FilterMarcaId;
        private int FilterSpecId;
        private decimal PrecioAcumulado;
        private int CantidadCarrito;

        public RemitoView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            GetGridsData();
            CargarComboProveedor();
            //
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.BtnQuitar, "Quitar");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.BtnAgregar, "Agregar");
            //
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.BtnFiltrar, "Filtrar");
            //
            System.Windows.Forms.ToolTip ToolTip4 = new System.Windows.Forms.ToolTip();
            ToolTip4.SetToolTip(this.BtnBuscar, "Buscar");
            //
            System.Windows.Forms.ToolTip ToolTip5 = new System.Windows.Forms.ToolTip();
            ToolTip5.SetToolTip(this.BtnNuevoProveedor, "Agregar Nuevo Proveedor");
            //
            System.Windows.Forms.ToolTip ToolTip6 = new System.Windows.Forms.ToolTip();
            ToolTip6.SetToolTip(this.BtnCancelar, "Cancelar Remito");
            //
            System.Windows.Forms.ToolTip ToolTip7 = new System.Windows.Forms.ToolTip();
            ToolTip7.SetToolTip(this.BtnGuardar, "Guardar Remito");
            //
            System.Windows.Forms.ToolTip ToolTip8 = new System.Windows.Forms.ToolTip();
            ToolTip8.SetToolTip(this.BtnAgregarProducto, "Agregar Nuevo Producto");
            //
            System.Windows.Forms.ToolTip ToolTip9 = new System.Windows.Forms.ToolTip();
            ToolTip9.SetToolTip(this.BtnRecargarData, "Refrescar");
        }

        public RemitoView(IUnitOfWork unitOfWork, int idProvedor)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.idProvedor = idProvedor;
            //
            GetGridsData();
            CargarComboProveedor(idProvedor);
            //
        }

        private void GetGridsData()
        {
            GridProductos.DataSource = ClasesCompartidas.productosList;
            GridRemitoDetalle.DataSource = ClasesCompartidas.RemitosDetalle;
        }

        private void CargarComboProveedor(int? proveedorId = 0)
        {

            ComboProveedor.DisplayMember = "Nombre";
            ComboProveedor.ValueMember = "Id";
            ComboProveedor.DataSource = ClasesCompartidas.proveedoresList;
            //
            if (ClasesCompartidas.ProveedorNuevo != null)
            {
                int index = (ComboProveedor.Items.Count);
                ComboProveedor.DataSource = ClasesCompartidas.proveedoresList;
                ComboProveedor.DisplayMember = "Nombre";
                ComboProveedor.SelectedIndex = index - 1;
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void RemitoView_Load(object sender, EventArgs e)
        {
            ComboTipoCom.SelectedIndex = 0;
            //
            GetComboData();
        }

        private void GetComboData()
        {
            // TiposDeProductos
            ComboTipo.DisplayMember = "Nombre";
            ComboTipo.ValueMember = "Id";
            ComboTipo.DataSource = ClasesCompartidas.tiposProductosList;

            // Marcas
            ComboMarca.DisplayMember = "Nombre";
            ComboMarca.ValueMember = "Id";
            ComboMarca.DataSource = ClasesCompartidas.marcasList;

            // SPEC's
            ComboSpec.DisplayMember = "Nombre";
            ComboSpec.ValueMember = "Id";
            ComboSpec.DataSource = ClasesCompartidas.specsList;
        }

        private void RadioBulto_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RadioUnidad_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckTipo_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckTipo.Checked == true)
            {
                ComboTipo.Enabled = true;
            }
            else if (CheckTipo.Checked == false)
            {
                FilterTipoId = 0;
                ComboTipo.Enabled = false;
            }
        }

        private void CheckMarca_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckMarca.Checked == true)
            {
                ComboMarca.Enabled = true;
            }
            else if (CheckMarca.Checked == false)
            {
                FilterMarcaId = 0;
                ComboMarca.Enabled = false;
            }
        }

        private void CheckSpec_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckSpec.Checked == true)
            {
                ComboSpec.Enabled = true;
            }
            else if (CheckSpec.Checked == false)
            {
                FilterSpecId = 0;
                ComboSpec.Enabled = false;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string txtBusqueda = TxtBuscar.Text;
            GetGridProductsData(txtBusqueda);
        }

        private async void GetGridProductsData(string txtBusqueda)
        {
            var recibedSearch = txtBusqueda;
            GridProductos.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario),
            filter: c => c.TipoProducto.Nombre.Contains(recibedSearch) && c.Visible.Equals(true) || c.Marca.Nombre.Contains(recibedSearch) && c.Visible.Equals(true) || c.SPEC.Nombre.Contains(recibedSearch) &&
            c.Visible.Equals(true) || c.Detalles.Contains(recibedSearch) && c.Visible.Equals(true));
        }

        private void BtnFiltrar_Click(object sender, EventArgs e)
        {
            FilterComboBoxes();
        }

        private async void FilterComboBoxes()
        {
            // TipoFilter
            if (CheckTipo.Checked == true)
            {
                FilterTipoId = (int)ComboTipo.SelectedValue;
            }
            else
            {
                FilterTipoId = 0;
            }

            // MarcaFilter
            if (CheckMarca.Checked == true)
            {
                FilterMarcaId = (int)ComboMarca.SelectedValue;
            }
            else
            {
                FilterMarcaId = 0;
            }

            //SpecFilter
            if (CheckSpec.Checked == true)
            {
                FilterSpecId = (int)ComboSpec.SelectedValue;
            }
            else
            {
                FilterSpecId = 0;
            }
            //
            Filter.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: FilterTipoId != 0 && FilterMarcaId != 0 && FilterSpecId != 0 ? t => t.TipoProductoId.Equals(FilterTipoId) && t.MarcaId.Equals(FilterMarcaId) && t.SPECId.Equals(FilterSpecId) && t.Visible.Equals(true) : FilterTipoId != 0 && FilterMarcaId != 0 ? t => t.TipoProductoId.Equals(FilterTipoId) && t.MarcaId.Equals(FilterMarcaId) && t.Visible.Equals(true) : FilterTipoId != 0 && FilterSpecId != 0 ? t => t.TipoProductoId.Equals(FilterTipoId) && t.SPECId.Equals(FilterSpecId) && t.Visible.Equals(true) : FilterMarcaId != 0 && FilterSpecId != 0 ? t => t.MarcaId.Equals(FilterMarcaId) && t.SPECId.Equals(FilterSpecId) && t.Visible.Equals(true) : FilterTipoId != 0 ? t => t.TipoProductoId.Equals(FilterTipoId) && t.Visible.Equals(true) : FilterMarcaId != 0 ? t => t.MarcaId.Equals(FilterMarcaId) && t.Visible.Equals(true) : FilterSpecId != 0 ? t => t.SPECId.Equals(FilterSpecId) && t.Visible.Equals(true) : t => t.Visible.Equals(true));
            //
            GridProductos.DataSource = Filter;
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (TxtBuscar.Text.Length < 1)
            {
                GetGridsData();
            }
        }

        private void GridProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridProductos.DataBindingComplete += delegate
            {
                foreach (DataGridViewRow row in GridProductos.Rows)
                {
                    for (int i = 0; i < GridProductos.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(row.Cells["Stock"].Value) < 1)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(205, 69, 69);
                            row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 66, 66);
                        }
                    }
                }
                //
                foreach (DataGridViewRow row in GridProductos.Rows)
                {
                    for (int i = 0; i < GridProductos.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(row.Cells["Carrito"].Value) > 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.SeaGreen;
                            row.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                        }
                    }
                }
                foreach (DataGridViewRow row in GridProductos.Rows)
                {
                    for (int i = 0; i < GridProductos.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(row.Cells["Carrito"].Value) < 1)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(38, 38, 38);
                            row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                        }
                    }
                }
                foreach (DataGridViewColumn columna in GridProductos.Columns)
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
                    //if (columna.Name == "CantidadBulto")
                    //    columna.Visible = true;
                    if (columna.Name == "Usuario")
                        columna.Visible = false;
                    if (columna.Name == "Proveedor")
                        columna.Visible = false;
                    if (columna.Name == "Modificacion")
                        columna.Visible = false;
                    if (columna.Name == "Descuento")
                        columna.Visible = false;
                    if (columna.Name == "Ganancia")
                        columna.Visible = false;
                    if (columna.Name == "PVP")
                        columna.Visible = false;
                    #endregion
                    //-------------------
                    #region AjustesDeColumnas
                    if (columna.Name == "CantidadBulto")
                        columna.HeaderText = "Uds. por Bulto";

                    if (columna.Name == "Id")
                    {
                        //columna.Visible = false;
                        columna.Width = 40;
                    }
                    if (columna.Name == "TipoProducto")
                        columna.HeaderText = "Tipo";
                    if (columna.Name == "PrecioBulto")
                    {
                        columna.HeaderText = "PVP Bulto";
                        this.GridProductos.Columns["PrecioBulto"].DefaultCellStyle.Format = "$" + "0.00";
                    }
                    if (columna.Name == "Carrito")
                    {
                        columna.HeaderText = "Remito";
                    }
                    if (columna.Name == "PrecioUnidad")
                    {
                        columna.HeaderText = "P. Unidad";
                        this.GridProductos.Columns["PrecioUnidad"].DefaultCellStyle.Format = "$" + "0.00";
                    }
                    if (columna.Name == "PVP")
                    {
                        columna.HeaderText = "PVP";
                        this.GridProductos.Columns["PVP"].DefaultCellStyle.Format = "$" + "0.00";
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
                        this.GridProductos.Columns["Deuda"].DefaultCellStyle.Format = "$" + "0.00";
                    }
                    if (columna.Name == "Ganancia")
                    {
                        //this.GridProductos.Columns["Ganancia"].DefaultCellStyle.Format = ;
                    }
                    if (columna.Name == "Stock")
                    {
                    }
                    if (columna.Name == "SPEC")
                    {
                    }



                    #endregion
                }
            };
        }

        private void BtnRecargarData_Click(object sender, EventArgs e)
        {

            RecargarData();
        }

        private void RecargarData()
        {
            CheckTipo.Checked = false;
            CheckMarca.Checked = false;
            CheckSpec.Checked = false;
            //
            TxtBuscar.Text = " ";
            //
            GetComboData();
            //
            GridProductos.DataSource = ClasesCompartidas.productosList;
        }

        private void BtnNuevoProveedor_Click(object sender, EventArgs e)
        {
            bool editando = false;
            IUnitOfWork unitOfWork = new UnitOfWork();
            //
            var createProovedorView = new CreateProveedorView(unitOfWork, editando);
            createProovedorView.ShowDialog();
            //
            CargarComboProveedor();
        }

        private void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            bool editando = false;
            IUnitOfWork unitOfWork = new UnitOfWork();
            //
            var createProductView = new CreateProductView(unitOfWork, editando);
            createProductView.ShowDialog();
            //
            FilterProductoCreado();
        }

        private async void FilterProductoCreado()
        {
            GridProductos.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true) && v.Id.Equals(ClasesCompartidas.ProductoId));
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Remito? _remito = null;
            RemitoDetalle? _remitoDetalle = null;
            Producto? _producto = null;
            bool _repetido = false;
            //
            int IdProducto = (int)GridProductos.CurrentRow.Cells["Id"].Value;
            //
            int StockActual = (int)GridProductos.CurrentRow.Cells["Stock"].Value;
            //
            var cantidadxBulto = Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value);
            //
            int CantidadDeseada;
            int CantidadBulto;
            int CantidadRemito = 0;
            //
            CantidadDeseada = (int)NumCantidad.Value;
            //
            if (RadioBulto.Checked == true)
            {
                PrecioAcumulado = PrecioAcumulado + ((decimal)GridProductos.CurrentRow.Cells["PrecioBulto"].Value * CantidadDeseada);
            }
            else
            {
                PrecioAcumulado = PrecioAcumulado + ((decimal)GridProductos.CurrentRow.Cells["PrecioUnidad"].Value * CantidadDeseada);
            }
            //
            LblImporte.Text = "$" + PrecioAcumulado.ToString("0.00");
            //
            if (RadioBulto.Checked == true)
            {
                //
                CantidadCarrito = CantidadCarrito + (CantidadDeseada * cantidadxBulto);
                LblCantidadRemito.Text = Convert.ToString(CantidadCarrito);
            }
            else
            {
                CantidadCarrito = CantidadCarrito + CantidadDeseada;
                LblCantidadRemito.Text = Convert.ToString(CantidadCarrito);
            }
            //
            //
            if (StockActual < 1 && ComboTipoCom.SelectedIndex == 1)
            {
                MessageBox.Show($"El producto que intenta vender se encuentra sin stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (StockActual < CantidadDeseada && ComboTipoCom.SelectedIndex == 1)
                {
                    MessageBox.Show($"La cantidad indicada de productos, supera el stock regitrado del mismo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Color
                    GridProductos.CurrentRow.DefaultCellStyle.BackColor = Color.SeaGreen;
                    GridProductos.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                    // Valor
                    if (RadioBulto.Checked == true)
                    {
                        RemitoDetalle remitoDetalle = new RemitoDetalle()
                        {
                            TipoProductoId = Convert.ToInt32(GridProductos.CurrentRow.Cells["TipoProductoId"].Value),
                            MarcaId = Convert.ToInt32(GridProductos.CurrentRow.Cells["MarcaId"].Value),
                            Detalles = (string)(GridProductos.CurrentRow.Cells["Detalles"].Value),
                            SPECId = Convert.ToInt32(GridProductos.CurrentRow.Cells["SPECId"].Value),
                            bulto = true,
                            CantidadBultos = CantidadDeseada,
                            CantidadXBultos = Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value),
                            PrecioBulto = Convert.ToInt32(GridProductos.CurrentRow.Cells["PrecioBulto"].Value),
                            PrecioUnitario = 0,
                            CantidadTotal = CantidadCarrito,
                            PrecioTotal = PrecioAcumulado,
                            RemitoId = null
                        };
                        //
                        var cantidadBulto = Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value);
                        CantidadRemito = CantidadDeseada * cantidadBulto;
                        _remitoDetalle = remitoDetalle;
                    }
                    else
                    {
                        RemitoDetalle remitoDetalle = new RemitoDetalle()
                        {
                            TipoProductoId = Convert.ToInt32(GridProductos.CurrentRow.Cells["TipoProductoId"].Value),
                            MarcaId = Convert.ToInt32(GridProductos.CurrentRow.Cells["MarcaId"].Value),
                            Detalles = (string)(GridProductos.CurrentRow.Cells["Detalles"].Value),
                            SPECId = Convert.ToInt32(GridProductos.CurrentRow.Cells["SPECId"].Value),
                            bulto = false,
                            CantidadBultos = 0,
                            CantidadXBultos = 0,
                            PrecioBulto = 0,
                            PrecioUnitario = Convert.ToInt32(GridProductos.CurrentRow.Cells["PrecioUnidad"].Value),
                            CantidadTotal = CantidadDeseada,
                            PrecioTotal = PrecioAcumulado,
                            RemitoId = null
                        };
                        //
                        _remitoDetalle = remitoDetalle;
                        CantidadRemito = CantidadDeseada;
                    }
                    //
                    bool bultoExistente = true;
                    bool productoExistente = true;
                    // En el siguiente bool se comprueba si el producto enviado ya se encuentra cargado en el remito
                    // por lo tanto se deberia de agregar una unidad nueva a la cantidad de articulos 
                    if (RadioBulto.Checked == true)
                    {
                        bultoExistente = unitOfWork.RemitoDetalleRepository.dbSet.Any(x => x.TipoProductoId.Equals(_remitoDetalle.TipoProductoId) && x.MarcaId.Equals(_remitoDetalle.MarcaId) && x.SPECId.Equals(_remitoDetalle.SPECId) && x.Detalles.Equals(_remitoDetalle.Detalles) && x.RemitoId == null && x.bulto == true);
                    }
                    else
                    {
                        productoExistente = unitOfWork.RemitoDetalleRepository.dbSet.Any(x => x.TipoProductoId.Equals(_remitoDetalle.TipoProductoId) && x.MarcaId.Equals(_remitoDetalle.MarcaId) && x.SPECId.Equals(_remitoDetalle.SPECId) && x.Detalles.Equals(_remitoDetalle.Detalles) && x.RemitoId == null && x.bulto == false);
                    }
                    //
                    if (!bultoExistente || !productoExistente) // Nuevo producto en el carrito 
                    {
                        _repetido = false;
                    }
                    else if (_repetido == false)
                    {
                        //  ProductoRepetido
                        #region VariablesParaComparar
                        int idEncontrado = 0;
                        int TipoId = (int)GridProductos.CurrentRow.Cells["TipoProductoId"].Value;
                        int MarcaId = (int)GridProductos.CurrentRow.Cells["MarcaId"].Value;
                        string Detalles = (string)GridProductos.CurrentRow.Cells["Detalles"].Value;
                        int SpecId = (int)GridProductos.CurrentRow.Cells["SPECId"].Value;
                        int? remitoId = null;
                        #endregion
                        // foreach para encontrar el id del producto repetido
                        //foreach (DataGridViewRow row in GridRemitoDetalle.Rows)
                        //{
                        //    if (RadioBulto.Checked == true)
                        //    {
                        //        if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && remitoId == Convert.ToInt32(row.Cells["RemitoId"].Value) && (bool)row.Cells["Bulto"].Value == true)
                        //        {
                        //            idEncontrado = (int)row.Cells[0].Value;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && remitoId == Convert.ToInt32(row.Cells["RemitoId"].Value) && (bool)row.Cells["Bulto"].Value == false)
                        //        {
                        //            idEncontrado = (int)row.Cells[0].Value;
                        //        }
                        //    }
                        //}
                        //
                        foreach (DataGridViewRow row in GridRemitoDetalle.Rows)
                        {
                            if (RadioBulto.Checked == true)
                            {
                                if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && (bool)row.Cells["Bulto"].Value == true)
                                {
                                    idEncontrado = (int)row.Cells[0].Value;
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && (bool)row.Cells["Bulto"].Value == false)
                                {
                                    idEncontrado = (int)row.Cells[0].Value;
                                }
                            }
                        }

                        //
                        if (idEncontrado != 0) // Si se ha encontrado un id dentro de la tabla, es que se trata de un producto repetido por ende es encesario actualizarle su cantidad
                        {
                            var remitoDetalleEdit = unitOfWork.RemitoDetalleRepository.GetByID(idEncontrado);
                            var bultazo = remitoDetalleEdit.bulto;
                            //
                            if (bultazo == true)
                            {
                                var precioBulto = Convert.ToDecimal(GridRemitoDetalle.CurrentRow.Cells["PrecioBulto"].Value);
                                var cantidadXBulto = Convert.ToInt32(GridRemitoDetalle.CurrentRow.Cells["CantidadXBultos"].Value);
                                //
                                remitoDetalleEdit.CantidadTotal = remitoDetalleEdit.CantidadTotal + CantidadDeseada * cantidadXBulto;
                                remitoDetalleEdit.CantidadBultos = remitoDetalleEdit.CantidadBultos + CantidadDeseada;
                                remitoDetalleEdit.PrecioTotal = remitoDetalleEdit.PrecioTotal + CantidadDeseada * precioBulto;
                                _remitoDetalle = remitoDetalleEdit;
                                //
                                CantidadRemito = CantidadDeseada * cantidadXBulto;
                            }
                            else
                            {
                                var precioUnitario = Convert.ToDecimal(GridRemitoDetalle.CurrentRow.Cells["PrecioUnitario"].Value);
                                //
                                remitoDetalleEdit.CantidadTotal = remitoDetalleEdit.CantidadTotal + CantidadDeseada;
                                remitoDetalleEdit.PrecioTotal = remitoDetalleEdit.PrecioTotal + (precioUnitario * CantidadDeseada);
                                _remitoDetalle = remitoDetalleEdit;
                                //
                                CantidadRemito = CantidadDeseada;
                            }
                            //
                            _repetido = true;
                        }
                    }
                    //
                    var productoRemito = unitOfWork.ProductoRepository.GetByID(IdProducto);
                    productoRemito.Carrito = productoRemito.Carrito + CantidadRemito;
                    _producto = productoRemito;
                    //
                    var miniLoading = new MiniLoading(_remitoDetalle, unitOfWork, _repetido, _producto);
                    miniLoading.ShowDialog();
                    //
                    GetGridsData();
                }
            }
        }

        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            RemitoDetalle? _remitoDetalle = null;
            Producto? _productoRemito = null;
            int? cantidadDevuelta = 0;
            //
            bool _delete = false;
            //
            int IdRemitoDetalle = (int)GridRemitoDetalle.CurrentRow.Cells["Id"].Value;
            //
            int IdProducto = 0;
            //
            #region VariablesParaComparar
            int TipoId = (int)GridRemitoDetalle.CurrentRow.Cells["TipoProductoId"].Value;
            int MarcaId = (int)GridRemitoDetalle.CurrentRow.Cells["MarcaId"].Value;
            string Detalles = (string)GridRemitoDetalle.CurrentRow.Cells["Detalles"].Value;
            int SpecId = (int)GridRemitoDetalle.CurrentRow.Cells["SPECId"].Value;
            #endregion

            //

            int CantidadDeseada = (int)NumCantidad.Value;
            //
            if (RadioBulto.Checked == true)
            {
                PrecioAcumulado = PrecioAcumulado - ((decimal)GridProductos.CurrentRow.Cells["PrecioBulto"].Value * CantidadDeseada);
            }
            else
            {
                PrecioAcumulado = PrecioAcumulado - ((decimal)GridProductos.CurrentRow.Cells["PrecioUnidad"].Value * CantidadDeseada);
            }
            //
            LblImporte.Text = "$" + PrecioAcumulado.ToString("0.00");
            //
            if (RadioBulto.Checked == true)
            {
                CantidadCarrito = CantidadCarrito - CantidadDeseada * Convert.ToInt32(GridRemitoDetalle.CurrentRow.Cells["CantidadXBultos"].Value);
                LblCantidadRemito.Text = Convert.ToString(CantidadCarrito);

            }
            else
            {
                CantidadCarrito = CantidadCarrito - CantidadDeseada;
                LblCantidadRemito.Text = Convert.ToString(CantidadCarrito);

            }
            //
            if (Convert.ToInt32(GridRemitoDetalle.CurrentRow.Cells["CantidadTotal"].Value) < CantidadDeseada)
            {
                MessageBox.Show($"La cantidad indicada de productos para quitar, supera la cantidad regitrada en el remito del mismo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Convert.ToInt32(GridRemitoDetalle.CurrentRow.Cells["CantidadTotal"].Value) == CantidadDeseada || (Convert.ToInt32(GridRemitoDetalle.CurrentRow.Cells["CantidadTotal"].Value) == Convert.ToInt32(GridRemitoDetalle.CurrentRow.Cells["CantidadXBultos"].Value)))
                {
                    // Delete Remito
                    var remitoDetalle = unitOfWork.RemitoDetalleRepository.GetByID(IdRemitoDetalle);
                    cantidadDevuelta = remitoDetalle.CantidadTotal;
                    _remitoDetalle = remitoDetalle;
                    _delete = true;
                }
                else
                {
                    // Quitar                     
                    var remitoDetalle = unitOfWork.RemitoDetalleRepository.GetByID(IdRemitoDetalle);
                    bool bulto = remitoDetalle.bulto;
                    //
                    if (bulto == true)
                    {
                        var precioBulto = Convert.ToDecimal(GridRemitoDetalle.CurrentRow.Cells["PrecioBulto"].Value);
                        var cantidadXBulto = Convert.ToInt32(GridRemitoDetalle.CurrentRow.Cells["CantidadXBultos"].Value);
                        //
                        remitoDetalle.PrecioTotal = remitoDetalle.PrecioTotal - (precioBulto * CantidadDeseada);
                        remitoDetalle.CantidadBultos = remitoDetalle.CantidadBultos - CantidadDeseada;
                        remitoDetalle.CantidadTotal = remitoDetalle.CantidadTotal - (CantidadDeseada * cantidadXBulto);
                        cantidadDevuelta = CantidadDeseada * cantidadXBulto;
                    }
                    else
                    {
                        var precioUnitario = Convert.ToDecimal(GridRemitoDetalle.CurrentRow.Cells["PrecioUnitario"].Value);
                        remitoDetalle.PrecioTotal = remitoDetalle.PrecioTotal - (precioUnitario * CantidadDeseada);
                        remitoDetalle.CantidadTotal = remitoDetalle.CantidadTotal - CantidadDeseada;
                        cantidadDevuelta = CantidadDeseada;
                    }
                    //
                    _remitoDetalle = remitoDetalle;
                    _delete = false;
                }
                //
                foreach (DataGridViewRow row in GridProductos.Rows)
                {
                    if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value))
                    {
                        IdProducto = Convert.ToInt32(row.Cells["Id"].Value);
                    }
                }
                //
                if (IdProducto != 0)
                {
                    var productoRemito = unitOfWork.ProductoRepository.GetByID(IdProducto);
                    productoRemito.Carrito = productoRemito.Carrito - (int)cantidadDevuelta;
                    //
                    _productoRemito = productoRemito;
                }
                //
                var miniLoading = new MiniLoading(unitOfWork, _remitoDetalle, _delete, _productoRemito);
                miniLoading.ShowDialog();
            }
            //
            GetGridsData();
        }

        private void GridRemitoDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridRemitoDetalle.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn columna in GridRemitoDetalle.Columns)
                {
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


                    if (columna.Name == "Usuario")
                        columna.Visible = false;
                    if (columna.Name == "Proveedor")
                        columna.Visible = false;
                    if (columna.Name == "Modificacion")
                        columna.Visible = false;
                    if (columna.Name == "Stock")
                        columna.Visible = false;
                    if (columna.Name == "Ganancia")
                        columna.Visible = false;
                    if (columna.Name == "PrecioUnidad")
                        columna.Visible = false;
                    if (columna.Name == "CuentaId")
                        columna.Visible = false;
                    if (columna.Name == "Cuenta")
                        columna.Visible = false;
                    if (columna.Name == "Pagado")
                        columna.Visible = false;
                    if (columna.Name == "FechaDePago")
                        columna.Visible = false;
                    if (columna.Name == "CodigoDeVenta")
                        columna.Visible = false;

                    #endregion
                    //-------------------
                    #region AjustesDeColumnas
                    if (columna.Name == "PrecioTotal")
                    {
                        columna.DefaultCellStyle.Format = "$" + "0.00";
                        columna.HeaderText = "P. Total";
                    }
                    if (columna.Name == "TipoProducto")
                        columna.HeaderText = "Tipo";

                    if (columna.Name == "PrecioBulto")
                    {
                        columna.DefaultCellStyle.Format = "$" + "0.00";
                        columna.HeaderText = "PVP Bulto";
                    }
                    if (columna.Name == "CantidadBultos")
                    {
                        columna.HeaderText = "Cant. de Bultos";
                    }
                    if (columna.Name == "CantidadXBultos")
                    {
                        columna.HeaderText = "Uds. por Bulto";
                    }
                    if (columna.Name == "CantidadTotal")
                    {
                        columna.HeaderText = "Cantidad Total";
                    }
                    if (columna.Name == "PrecioUnitario")
                    {
                        columna.DefaultCellStyle.Format = "$" + "0.00";
                        columna.HeaderText = "P. Unitario";
                    }
                    if (columna.Name == "PVP")
                    {
                        columna.HeaderText = "PVP";
                        this.GridRemitoDetalle.Columns["PVP"].DefaultCellStyle.Format = "$" + "0.00";
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
                        this.GridRemitoDetalle.Columns["Deuda"].DefaultCellStyle.Format = "$" + "0.00";
                    }
                    if (columna.Name == "Ganancia")
                    {
                        columna.Width = 80;
                        //this.GridProductos.Columns["Ganancia"].DefaultCellStyle.Format = ;
                    }
                    if (columna.Name == "Stock")
                    {
                        columna.Width = 70;
                    }
                    #endregion
                }
            };
        }

        private void RemitoView_Activated(object sender, EventArgs e)
        {
            if (CantidadCarrito > 0)
            {
                BtnCancelar.Enabled = true;
                BtnGuardar.Enabled = true;
            }
            else
            {
                BtnCancelar.Enabled = false;
                BtnGuardar.Enabled = false;
            }

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea cancelar el Remito?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                var saveLoading = new SaveView(unitOfWork);
                saveLoading.ShowDialog();
                //
                GetGridsData();
                //
                LblImporte.Text = " ";
                LblCantidadRemito.Text = " ";
                GetComboData();
                GetGridsData();
                tabControl1.SelectedIndex = 0;
                //
                MessageBox.Show($"El Remito se ha cancelado con éxito!", "Remito Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar el Remito?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                Remito remito = new Remito()
                {
                    TipoComprobante = Convert.ToString(ComboTipoCom.SelectedItem),
                    Importe = PrecioAcumulado,
                    ProveedorId = (int)ComboProveedor.SelectedValue,
                    CantidadProductos = CantidadCarrito,
                    UsuarioId = ClasesCompartidas.UserId,
                    Fecha = DateTime.Now
                };
                //
                var saveLoading = new SaveView(unitOfWork, remito);
                saveLoading.ShowDialog();
                //
                LblImporte.Text = " ";
                LblCantidadRemito.Text = " ";
                GetComboData();
                GetGridsData();
                RecargarData();
                tabControl1.SelectedIndex = 0;
                //
                MessageBox.Show($"El Remito se ha guardado con éxito!", "Remito Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                BtnAgregar.Enabled = true;
                BtnQuitar.Enabled = false;

            }
            else if (tabControl1.SelectedIndex == 1)
            {
                BtnAgregar.Enabled = false;
                BtnQuitar.Enabled = true;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void BtnImprimir_Click(object sender, EventArgs e)
        {
            remitosDetalles.DataSource = await unitOfWork.RemitoDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Remito).Include(q => q.Remito.Proveedor).Include(q => q.Remito.Usuario), filter: q => q.RemitoId.Equals(ClasesCompartidas.RemitoId));
            //remitosDetalles = (IEnumerable<RemitoDetalle>)unitOfWork.RemitoDetalleRepository.GetByID(1);
            ComprobanteViewReport comprobanteViewReport = new ComprobanteViewReport(this.remitosDetalles);
            comprobanteViewReport.ShowDialog();
        }
    }
}
