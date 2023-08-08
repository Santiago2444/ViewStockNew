using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;
using ViewStockNew.Repositories;
using ViewStockNew.Utils;
using ViewStockNew.ViewReport;

namespace ViewStockNew.Views
{
    public partial class MakeSaleView : Form
    {
        IUnitOfWork unitOfWork;
        BindingSource Filter = new BindingSource();
        BindingSource ticket = new BindingSource();
        //
        private decimal PrecioAcumulado;
        private int CantidadCarrito;
        private decimal Vuelto;
        private int ErrorMensaje = 0;
        private string AuxImporte;
        //
        private int FilterTipoId = 0;
        private int FilterMarcaId = 0;
        private int FilterSpecId = 0;
        //
        public MakeSaleView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            //
            GetUserData();
            GetComboCuentas();
            GetComboDatas();
            //
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.BtnBuscar, "Buscar");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.FechaDesde, "Filtrar");
            //
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.BtnAgregar, "Agregar");
            //
            System.Windows.Forms.ToolTip ToolTip4 = new System.Windows.Forms.ToolTip();
            ToolTip4.SetToolTip(this.BtnQuitar, "Quitar");
            //
            System.Windows.Forms.ToolTip ToolTip5 = new System.Windows.Forms.ToolTip();
            ToolTip5.SetToolTip(this.BtnAgregarCuenta, "Agregar Cuenta");
        }

        private void GetComboDatas()
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
        private void GetComboCuentas(int? cuentaId = 0)
        {
            ComboCuentas.DisplayMember = "Nombre";
            ComboCuentas.ValueMember = "Id";
            ComboCuentas.DataSource = ClasesCompartidas.cuentasList;
            //
            if (cuentaId != 0)
                ComboCuentas.SelectedValue = cuentaId;
            else
                ComboCuentas.SelectedValue = 1;
            //
            if (ClasesCompartidas.CuentaNueva != null)
            {
                int index = (ComboCuentas.Items.Count);
                ComboCuentas.DataSource = ClasesCompartidas.cuentasList;
                ComboCuentas.DisplayMember = "Nombre";
                ComboCuentas.SelectedIndex = index - 1;
            }
        }

        private void GetUserData()
        {
            var user = unitOfWork.UsuarioRepository.GetByID(ClasesCompartidas.UserId);
            //
            if (user.Imagen != null)
            {
                PctUser.Image = (Bitmap)((new ImageConverter()).ConvertFrom(user.Imagen));
            }
            //
            LblNombreUser.Text = user.Nombre;
            //
            if (ClasesCompartidas.TipoUsuarioId == 1)
                LblCargoUser.Text = "Administrador";
            else if (ClasesCompartidas.TipoUsuarioId == 2)
                LblCargoUser.Text = "Encargado";
            else
                LblCargoUser.Text = "Empleado";
        }

        private void MakeSaleView_Load(object sender, EventArgs e)
        {
            GetGridsProductsDatas();
        }
        private void GetGridsProductsDatas()
        {
            GridProductos.DataSource = ClasesCompartidas.productosList;
            GridVentaDetalle.DataSource = ClasesCompartidas.VentaDetalle;

        }
        private async void GetGridProductsData(string txtBusqueda)
        {
            var recibedSearch = txtBusqueda;
            GridProductos.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario),
            filter: c => c.TipoProducto.Nombre.Contains(recibedSearch) && c.Visible.Equals(true) || c.Marca.Nombre.Contains(recibedSearch) && c.Visible.Equals(true) || c.SPEC.Nombre.Contains(recibedSearch) &&
            c.Visible.Equals(true) || c.Detalles.Contains(recibedSearch) && c.Visible.Equals(true));
        }

        private void GridProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
                if (columna.Name == "PrecioUnidad")
                    columna.Visible = false;
                #endregion
                //-------------------
                #region AjustesDeColumnas
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
                    columna.Width = 90;
                    this.GridProductos.Columns["PrecioBulto"].DefaultCellStyle.Format = "$" + "0.00";
                }
                if (columna.Name == "CantidadBulto")
                {
                    columna.HeaderText = "Cantidad";
                    columna.Width = 90;
                }
                if (columna.Name == "PrecioUnidad")
                {
                    columna.HeaderText = "P. Unidad";
                    columna.Width = 90;
                    this.GridProductos.Columns["PrecioUnidad"].DefaultCellStyle.Format = "$" + "0.00";
                }
                if (columna.Name == "PVP")
                {
                    columna.HeaderText = "PVP";
                    columna.Width = 50;
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
                    columna.Width = 90;
                    this.GridProductos.Columns["Deuda"].DefaultCellStyle.Format = "$" + "0.00";
                }
                if (columna.Name == "Ganancia")
                {
                    columna.Width = 80;
                    //this.GridProductos.Columns["Ganancia"].DefaultCellStyle.Format = ;
                }
                if (columna.Name == "Stock")
                {
                    columna.Width = 50;
                }
                if (columna.Name == "SPEC")
                {
                    columna.Width = 70;
                }
                if (columna.Name == "Carrito")
                    columna.Width = 60;
                if (columna.Name == "CantidadBulto")
                {
                    columna.HeaderText = "Uds. por Bulto";
                }

                #endregion
            }


        }

        private void BtnRecargar_Click(object sender, EventArgs e)
        {
            // Al hacer click en este boton, todas las herramientas de filtrado, vuelven a su estado original
            CheckTipo.Checked = false;
            CheckMarca.Checked = false;
            CheckSpec.Checked = false;
            //
            TxtBuscar.Text = " ";
            //
            GetComboDatas();
            //
            GridProductos.DataSource = ClasesCompartidas.productosList;
        }

        private void BtnFechaFiltro_Click(object sender, EventArgs e)
        {
            FilterComboBoxes();
        }

        private async void BtnContinuar_Click(object sender, EventArgs e)
        {
            // Al presionar este boton los productos en el carrito se eliminan y los que estan en venta, su columna carrito vuelve a 0
            //
            int IdProducto = 0;
            //
            int IdVentaDetalle = 0;
            //
            int StockDevuelto = 0;
            //
            int RowsCount = 0;
            //
            LblCantidadCarrito.Text = "0";
            //
            #region VariablesParaComparar
            int TipoId = 0;
            int MarcaId = 0;
            string Detalles = "-";
            int SpecId = 0;
            #endregion
            //
            if (GridVentaDetalle.Rows.Count == 1)
            {
                RowsCount = GridVentaDetalle.Rows.Count;
            }
            else
            {
                RowsCount = (int)GridVentaDetalle.Rows.Count - 1;
            }
            //
            if (ErrorMensaje == 1)
            {
                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea salir sin guardar la venta?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in GridVentaDetalle.Rows)
                    {
                        for (int v = 0; v < RowsCount; v++)
                        {
                            if (Convert.ToString(row.Cells["Pagado"].Value) == "Carrito")
                            {
                                IdVentaDetalle = (int)row.Cells["Id"].Value;
                                //
                                TipoId = (int)row.Cells["TipoProductoId"].Value;
                                MarcaId = (int)row.Cells["MarcaId"].Value;
                                SpecId = (int)row.Cells["SPECId"].Value;
                                Detalles = (string)row.Cells["Detalles"].Value;
                                //
                                StockDevuelto = (int)row.Cells["Cantidad"].Value;
                                //
                            }
                            if (IdVentaDetalle != 0)
                            {
                                var ventaDetalle = unitOfWork.VentaDetalleRepository.GetByID(IdVentaDetalle);
                                //
                                unitOfWork.VentaDetalleRepository.Delete(ventaDetalle);
                                unitOfWork.Save();
                                IdVentaDetalle = 0;
                            }
                            //
                            foreach (DataGridViewRow wor in GridProductos.Rows)
                            {
                                for (int p = 0; p < GridProductos.Rows.Count; p++)
                                {
                                    if (Convert.ToInt32(TipoId) == Convert.ToInt32(wor.Cells["TipoProductoId"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(wor.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(wor.Cells["SPECId"].Value) && Convert.ToString(Detalles) == Convert.ToString(wor.Cells["Detalles"].Value))
                                    {
                                        if (Convert.ToInt32(wor.Cells["Carrito"].Value) > 0)
                                        {
                                            IdProducto = (int)wor.Cells["Id"].Value;
                                        }
                                        //
                                        if (IdProducto != 0)
                                        {
                                            Producto producto = new Producto()
                                            {
                                                Id = IdProducto,
                                                TipoProductoId = (int)wor.Cells["TipoProductoId"].Value,
                                                MarcaId = (int)wor.Cells["MarcaId"].Value,
                                                SPECId = (int)wor.Cells["SPECId"].Value,
                                                ProveedorId = (int?)wor.Cells["ProveedorId"].Value,
                                                Detalles = (string?)wor.Cells["Detalles"].Value,
                                                PrecioBulto = (decimal)wor.Cells["PrecioBulto"].Value,
                                                CantidadBulto = (int)wor.Cells["CantidadBulto"].Value,
                                                PrecioUnidad = (decimal)wor.Cells["PrecioUnidad"].Value,
                                                Ganancia = (int)wor.Cells["Ganancia"].Value,
                                                PVP = (decimal)wor.Cells["PVP"].Value,
                                                Stock = (int)wor.Cells["Stock"].Value + StockDevuelto,
                                                Modificacion = (DateTime)wor.Cells["Modificacion"].Value,
                                                Visible = true,
                                                UsuarioId = (int)wor.Cells["UsuarioId"].Value,
                                                Imagen = (byte[]?)wor.Cells["Imagen"].Value,
                                                Carrito = 0
                                            };
                                            try
                                            {
                                                new ModelsValidator().Validate(producto);
                                                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                //if (respuesta == DialogResult.Yes)
                                                //{
                                                unitOfWork.ProductoRepository.Update(producto);
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
                        }
                    }
                    //
                    // A continuación se actualizan la lista delos datos
                    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    Close();
                }
            }
            else
            {
                Close();
            }
            //
            ClasesCompartidas.CuentaNueva = null;
        }

        private void GridProductos_DoubleClick(object sender, EventArgs e)
        {
            // En desuso
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string txtBusqueda = TxtBuscar.Text;
            GetGridProductsData(txtBusqueda);
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (TxtBuscar.Text.Length < 1)
            {
                GetGridsProductsDatas();
            }
        }

        private async void GridProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            // Variables para enviar a enviar al LoadingView
            Producto? _producto = null;
            VentaDetalle? _ventaDetalle = null;
            bool _repetido = false;
            //
            ErrorMensaje = 1;
            //
            int StockActual = (int)GridProductos.CurrentRow.Cells["Stock"].Value;
            //
            int IdProducto = (int)GridProductos.CurrentRow.Cells["Id"].Value;
            //
            int CarritoActual = (int)GridProductos.CurrentRow.Cells["Carrito"].Value;
            //
            PrecioAcumulado = PrecioAcumulado + (decimal)GridProductos.CurrentRow.Cells["PVP"].Value;
            LblImporte.Text = "$" + Convert.ToString(PrecioAcumulado);
            //
            CantidadCarrito++;
            LblCantidadCarrito.Text = Convert.ToString(CantidadCarrito);
            //
            if (CantidadCarrito >= 1)
            {
                BtnCancelar.Enabled = true;
                BtnGuardarDeuda.Enabled = true;
                TxtDineroCliente.ReadOnly = false;
                //
                BtnZero.Enabled = true;
                BtnOne.Enabled = true;
                BtnTwo.Enabled = true;
                BtnTrhee.Enabled = true;
                BtnFour.Enabled = true;
                BtnFive.Enabled = true;
                BtnSix.Enabled = true;
                BtnSeven.Enabled = true;
                BtnEight.Enabled = true;
                BtnNine.Enabled = true;
            }
            //
            if ((int)GridProductos.CurrentRow.Cells["Stock"].Value < 1)
            {
                MessageBox.Show($"El producto que intenta vender se encuentra sin stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //// Color
                //GridProductos.CurrentRow.DefaultCellStyle.BackColor = Color.FromArgb(226, 41, 41);
                //GridProductos.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 66, 66);
            }
            else
            {
                // Color
                GridProductos.CurrentRow.DefaultCellStyle.BackColor = Color.SeaGreen;
                GridProductos.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                // Valor

                VentaDetalle ventaDetalle = new VentaDetalle()
                {
                    TipoProductoId = Convert.ToInt32(GridProductos.CurrentRow.Cells["TipoProductoId"].Value),
                    MarcaId = Convert.ToInt32(GridProductos.CurrentRow.Cells["MarcaId"].Value),
                    Detalles = (string)(GridProductos.CurrentRow.Cells["Detalles"].Value),
                    SPECId = Convert.ToInt32(GridProductos.CurrentRow.Cells["SPECId"].Value),
                    ProveedorId = Convert.ToInt32(GridProductos.CurrentRow.Cells["ProveedorId"].Value),
                    PVP = Convert.ToDecimal(GridProductos.CurrentRow.Cells["PVP"].Value),
                    Cantidad = 1,
                    CuentaId = 1,
                    FechaDePago = DateTime.Now,
                    Pagado = "Carrito",
                    Imagen = (byte[]?)GridProductos.CurrentRow.Cells["Imagen"].Value,
                    UsuarioId = ClasesCompartidas.UserId

                };
                //
                // En el siguiente bool se comprueba si el producto enviado ya se encuentra cargado en el carrito
                // por lo tanto se deberia de agregar una unidad nueva a la cantidad de articulos 
                bool productoExistente = unitOfWork.VentaDetalleRepository.dbSet.Any(x => x.TipoProductoId.Equals(ventaDetalle.TipoProductoId) && x.MarcaId.Equals(ventaDetalle.MarcaId) && x.SPECId.Equals(ventaDetalle.SPECId) && x.Detalles.Equals(ventaDetalle.Detalles) && x.Pagado == ventaDetalle.Pagado);
                //
                //
                if (!productoExistente) // Nuevo producto en el carrito 
                {
                    //
                    Producto productoResStock = new Producto()
                    {
                        Id = IdProducto,
                        TipoProductoId = (int)GridProductos.CurrentRow.Cells["TipoProductoId"].Value,
                        MarcaId = (int)GridProductos.CurrentRow.Cells["MarcaId"].Value,
                        SPECId = (int)GridProductos.CurrentRow.Cells["SPECId"].Value,
                        ProveedorId = (int?)GridProductos.CurrentRow.Cells["ProveedorId"].Value,
                        Detalles = (string)GridProductos.CurrentRow.Cells["Detalles"].Value,
                        PrecioBulto = (decimal)GridProductos.CurrentRow.Cells["PrecioBulto"].Value,
                        CantidadBulto = (int)GridProductos.CurrentRow.Cells["CantidadBulto"].Value,
                        PrecioUnidad = (decimal)GridProductos.CurrentRow.Cells["PrecioUnidad"].Value,
                        Ganancia = (int)GridProductos.CurrentRow.Cells["Ganancia"].Value,
                        PVP = Convert.ToDecimal(GridProductos.CurrentRow.Cells["PVP"].Value),
                        Stock = StockActual - 1,
                        Modificacion = (DateTime)GridProductos.CurrentRow.Cells["Modificacion"].Value,
                        Visible = true,
                        UsuarioId = (int)GridProductos.CurrentRow.Cells["UsuarioId"].Value,
                        Imagen = (byte[]?)GridProductos.CurrentRow.Cells["Imagen"].Value,
                        Carrito = 1
                    };
                    //
                    _producto = productoResStock;
                    _ventaDetalle = ventaDetalle;
                    _repetido = false;
                    //try
                    //{
                    //    new ModelsValidator().Validate(productoResStock);
                    //    new ModelsValidator().Validate(ventaDetalle);
                    //    //
                    //    unitOfWork.ProductoRepository.Update(productoResStock);
                    //    unitOfWork.VentaDetalleRepository.Add(ventaDetalle);
                    //    unitOfWork.Save();
                    //    //
                    //    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    //    //VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");
                    //    //
                    //    GetGridsProductsDatas();
                    //}
                    //catch (Exception ex)
                    //{
                    //    Debug.Print(ex.Message);
                    //    Debug.Print(ex.InnerException?.Message);
                    //    MessageBox.Show(ex.Message);
                    //}
                }
                else
                {   //  ProductoRepetido
                    #region VariablesParaComparar
                    int idEncontrado = 0;
                    int TipoId = (int)GridProductos.CurrentRow.Cells["TipoProductoId"].Value;
                    int MarcaId = (int)GridProductos.CurrentRow.Cells["MarcaId"].Value;
                    string Detalles = (string)GridProductos.CurrentRow.Cells["Detalles"].Value;
                    int SpecId = (int)GridProductos.CurrentRow.Cells["SPECId"].Value;
                    string Pagado = "Carrito";
                    #endregion
                    // foreach para encontrar el id del producto repetido
                    foreach (DataGridViewRow row in GridVentaDetalle.Rows)
                    {
                        for (int i = 0; i < GridVentaDetalle.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && Pagado == Convert.ToString(row.Cells["Pagado"].Value))
                            {
                                idEncontrado = (int)row.Cells[0].Value;
                            }
                        }
                    }

                    if (idEncontrado != 0) // Si se ha encontrado un id dentro de la tabla, es que se trata de un producto repetido por ende es encesario actualizarle su cantidad
                    {
                        // Se averigua la cantidad de articulos que posee el producto en el carrito
                        var productoEdit = unitOfWork.VentaDetalleRepository.GetByID(idEncontrado);
                        var cantidadActual = productoEdit.Cantidad;

                        VentaDetalle productoRepetido = new VentaDetalle()
                        {
                            Id = (int)idEncontrado,
                            TipoProductoId = Convert.ToInt32(GridProductos.CurrentRow.Cells["TipoProductoId"].Value),
                            MarcaId = Convert.ToInt32(GridProductos.CurrentRow.Cells["MarcaId"].Value),
                            Detalles = (string)(GridProductos.CurrentRow.Cells["Detalles"].Value),
                            SPECId = Convert.ToInt32(GridProductos.CurrentRow.Cells["SPECId"].Value),
                            ProveedorId = Convert.ToInt32(GridProductos.CurrentRow.Cells["ProveedorId"].Value),
                            PVP = Convert.ToDecimal(GridProductos.CurrentRow.Cells["PVP"].Value),
                            Cantidad = cantidadActual + 1,
                            CuentaId = 1,
                            FechaDePago = DateTime.Now,
                            Pagado = "Carrito",
                            Imagen = (byte[]?)GridProductos.CurrentRow.Cells["Imagen"].Value,
                            UsuarioId = ClasesCompartidas.UserId
                        };
                        //
                        Producto productoResStock = new Producto()
                        {
                            Id = IdProducto,
                            TipoProductoId = (int)GridProductos.CurrentRow.Cells["TipoProductoId"].Value,
                            MarcaId = (int)GridProductos.CurrentRow.Cells["MarcaId"].Value,
                            SPECId = (int)GridProductos.CurrentRow.Cells["SPECId"].Value,
                            ProveedorId = (int?)GridProductos.CurrentRow.Cells["ProveedorId"].Value,
                            Detalles = (string)GridProductos.CurrentRow.Cells["Detalles"].Value,
                            PrecioBulto = (decimal)GridProductos.CurrentRow.Cells["PrecioBulto"].Value,
                            CantidadBulto = (int)GridProductos.CurrentRow.Cells["CantidadBulto"].Value,
                            PrecioUnidad = (decimal)GridProductos.CurrentRow.Cells["PrecioUnidad"].Value,
                            Ganancia = (int)GridProductos.CurrentRow.Cells["Ganancia"].Value,
                            PVP = Convert.ToDecimal(GridProductos.CurrentRow.Cells["PVP"].Value),
                            Stock = StockActual - 1,
                            Modificacion = (DateTime)GridProductos.CurrentRow.Cells["Modificacion"].Value,
                            Visible = true,
                            UsuarioId = (int)GridProductos.CurrentRow.Cells["UsuarioId"].Value,
                            Imagen = (byte[]?)GridProductos.CurrentRow.Cells["Imagen"].Value,
                            Carrito = CarritoActual + 1
                        };
                        //
                        _producto = productoResStock;
                        _ventaDetalle = productoRepetido;
                        _repetido = false;
                        //try
                        //{
                        //    new ModelsValidator().Validate(productoResStock);
                        //    new ModelsValidator().Validate(productoRepetido);

                        //    //
                        //    unitOfWork.ProductoRepository.Update(productoResStock);
                        //    unitOfWork.VentaDetalleRepository.Update(productoRepetido);
                        //    unitOfWork.Save();
                        //    //
                        //    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                        //    //VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");
                        //    //
                        //    GetGridsProductsDatas();
                        //}
                        //catch (Exception ex)
                        //{
                        //    Debug.Print(ex.Message);
                        //    Debug.Print(ex.InnerException?.Message);
                        //    MessageBox.Show(ex.Message);
                        //}
                    }
                }
                var miniloading = new MiniLoading(_ventaDetalle, _producto, unitOfWork, _repetido);
                miniloading.ShowDialog();
            }
            //
            GetGridsProductsDatas();
            */
        }

        private void GridVentaDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridVentaDetalle.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn columna in GridVentaDetalle.Columns)
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
                    if (columna.Name == "Venta")
                        columna.Visible = false;
                    if (columna.Name == "VentaId")
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
                    if (columna.Name == "TipoProducto")
                        columna.HeaderText = "Tipo";
                    if (columna.Name == "PrecioBulto")
                    {
                        columna.HeaderText = "PVP Bulto";
                        columna.DefaultCellStyle.Format = "$" + "0.00";
                    }
                    if (columna.Name == "CantidadBultos")
                    {
                        columna.HeaderText = "Cant. de Bultos";
                        columna.Width = 90;
                    }
                    if (columna.Name == "CantidadXBultos")
                    {
                        columna.HeaderText = "Uds. por Bulto";
                        columna.Width = 90;
                    }
                    if (columna.Name == "Cantidad")
                    {
                        columna.HeaderText = "Cantidad Total";
                        columna.Width = 90;
                    }
                    if (columna.Name == "PrecioUnidad")
                    {
                        columna.HeaderText = "P. Unidad";
                        columna.DefaultCellStyle.Format = "$" + "0.00";
                    }
                    if (columna.Name == "PVP")
                    {
                        columna.HeaderText = "PVP";
                        columna.Width = 125;
                        columna.DefaultCellStyle.Format = "$" + "0.00";
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
                        columna.DefaultCellStyle.Format = "$" + "0.00";
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

        private void BtnGuardarVenta_Click(object sender, EventArgs e)
        {
            ErrorMensaje = 0;
            //
            int IdProducto = 0;
            //
            int IdVentaDetalle = 0;
            //
            int _codigoVenta = 0;
            //
            int RowsCount = 0;
            //
            int _articulos = CantidadCarrito;
            //
            decimal _vuelto = Vuelto;
            //
            decimal _dinero = Convert.ToDecimal(TxtDineroCliente.Text);
            //
            decimal _importe = PrecioAcumulado;
            //
            int RowsProductCount = (int)GridProductos.Rows.Count - 1;
            //
            var _cuentaId = (int)ComboCuentas.SelectedValue;
            //
            if (GridVentaDetalle.Rows.Count == 1)
            {
                RowsCount = GridVentaDetalle.Rows.Count;
            }
            else
            {
                RowsCount = (int)GridVentaDetalle.Rows.Count - 1;
            }
            //
            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                var saveView = new SaveView(RowsCount, RowsProductCount, unitOfWork, _cuentaId, _articulos, _vuelto, _dinero, _importe);
                saveView.ShowDialog();

                //foreach (DataGridViewRow row in GridProductos.Rows)
                //{
                //    for (int i = 0; i < GridProductos.Rows.Count; i++)
                //    {
                //        if (Convert.ToInt32(row.Cells["Carrito"].Value) > 0)
                //        {
                //            IdProducto = (int)row.Cells["Id"].Value;
                //        }
                //        //
                //        if (IdProducto != 0)
                //        {
                //            Producto producto = new Producto()
                //            {
                //                Id = IdProducto,
                //                TipoProductoId = (int)row.Cells["TipoProductoId"].Value,
                //                MarcaId = (int)row.Cells["MarcaId"].Value,
                //                SPECId = (int)row.Cells["SPECId"].Value,
                //                ProveedorId = (int?)row.Cells["ProveedorId"].Value,
                //                Detalles = (string?)row.Cells["Detalles"].Value,
                //                PrecioBulto = (decimal)row.Cells["PrecioBulto"].Value,
                //                CantidadBulto = (int)row.Cells["CantidadBulto"].Value,
                //                PrecioUnidad = (decimal)row.Cells["PrecioUnidad"].Value,
                //                Ganancia = (int)row.Cells["Ganancia"].Value,
                //                PVP = (decimal)row.Cells["PVP"].Value,
                //                Stock = (int)row.Cells["Stock"].Value,
                //                Modificacion = (DateTime)row.Cells["Modificacion"].Value,
                //                Visible = true,
                //                UsuarioId = (int)row.Cells["UsuarioId"].Value,
                //                Imagen = (byte[]?)GridProductos.CurrentRow.Cells["Imagen"].Value,
                //                Carrito = 0
                //            };
                //            try
                //            {
                //                new ModelsValidator().Validate(producto);
                //                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //                //if (respuesta == DialogResult.Yes)
                //                //{
                //                unitOfWork.ProductoRepository.Update(producto);
                //                unitOfWork.Save();
                //                IdProducto = 0;
                //            }
                //            catch (Exception ex)
                //            {
                //                Debug.Print(ex.Message);
                //                Debug.Print(ex.InnerException?.Message);
                //                MessageBox.Show(ex.Message);
                //            }
                //        }
                //    }
                //}
                ////
                //var oldCodigoVenta = unitOfWork.CodigoVentaRepository.GetByID(1);
                //_codigoVenta = oldCodigoVenta.Codigo + 1;
                ////
                //CodigoVenta codigoVenta = new CodigoVenta()
                //{
                //    Id = 1,
                //    Codigo = _codigoVenta
                //};
                //try
                //{
                //    new ModelsValidator().Validate(codigoVenta);
                //    //
                //    unitOfWork.CodigoVentaRepository.Update(codigoVenta);
                //    unitOfWork.Save();
                //}
                //catch (Exception ex)
                //{
                //    Debug.Print(ex.Message);
                //    Debug.Print(ex.InnerException?.Message);
                //    MessageBox.Show(ex.Message);
                //}
                ////
                //foreach (DataGridViewRow row in GridVentaDetalle.Rows)
                //{
                //    for (int i = 0; i < RowsCount; i++)
                //    {
                //        if (Convert.ToString(row.Cells["Pagado"].Value) == "Carrito")
                //        {
                //            IdVentaDetalle = (int)row.Cells["Id"].Value;
                //        }
                //        //
                //        if (IdVentaDetalle != 0)
                //        {

                //            VentaDetalle productoRepetido = new VentaDetalle()
                //            {
                //                Id = IdVentaDetalle,
                //                TipoProductoId = (int)row.Cells["TipoProductoId"].Value,
                //                MarcaId = (int)row.Cells["MarcaId"].Value,
                //                Detalles = (string)(GridProductos.CurrentRow.Cells["Detalles"].Value),
                //                SPECId = (int)row.Cells["SPECId"].Value,
                //                ProveedorId = (int?)row.Cells["ProveedorId"].Value,
                //                PVP = (decimal)row.Cells["PVP"].Value,
                //                Cantidad = (int)row.Cells["Cantidad"].Value,
                //                CuentaId = 1,
                //                FechaDePago = DateTime.Now,
                //                Pagado = "Si",
                //                Imagen = (byte[]?)row.Cells["Imagen"].Value,
                //                UsuarioId = ClasesCompartidas.UserId,
                //                CodigoDeVenta = _codigoVenta
                //            };
                //            try
                //            {
                //                new ModelsValidator().Validate(productoRepetido);
                //                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //                //if (respuesta == DialogResult.Yes)
                //                //{
                //                unitOfWork.VentaDetalleRepository.Update(productoRepetido);
                //                unitOfWork.Save();
                //                IdVentaDetalle = 0;
                //            }
                //            catch (Exception ex)
                //            {
                //                Debug.Print(ex.Message);
                //                Debug.Print(ex.InnerException?.Message);
                //                MessageBox.Show(ex.Message);
                //            }
                //        }

                //    }
                //}
                ////
                //Venta venta = new Venta()
                //{
                //    Importe = PrecioAcumulado,
                //    Dinero = Convert.ToDecimal(TxtDineroCliente.Text),
                //    Vuelto = Vuelto,
                //    Articulos = Convert.ToInt32(LblCantidadCarrito.Text),
                //    CuentaId = 1,
                //    Estado = "Pagado",
                //    Fecha = DateTime.Now,
                //    Codigo = _codigoVenta,
                //    UsuarioId = ClasesCompartidas.UserId
                //};
                //try
                //{
                //    new ModelsValidator().Validate(venta);
                //    //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    //if (respuesta == DialogResult.Yes)
                //    //{
                //    unitOfWork.VentaRepository.Add(venta);
                //    unitOfWork.Save();
                //}
                //catch (Exception ex)
                //{
                //    Debug.Print(ex.Message);
                //    Debug.Print(ex.InnerException?.Message);
                //    MessageBox.Show(ex.Message);
                //}
                //// A continuación se actualizan los datos recientemente cargados
                //ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                //ClasesCompartidas.ventasList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado");
                //ClasesCompartidas.ventaDetallesList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si");
                ////
                MessageBox.Show($"La venta se ha realizado con éxito", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                //unitOfWork.Save();
                // Al guardar la venta, ciertas funciones se deshabilitan para evitar errores
                BtnGuardarDeuda.Enabled = false;
                BtnCancelar.Enabled = false;
                BtnGuardarVenta.Enabled = false;
                //
                TxtDineroCliente.ReadOnly = true;
                //
                BtnCalcular.Enabled = false;
                BtnDis.Enabled = false;
                //
                BtnZero.Enabled = false;
                BtnOne.Enabled = false;
                BtnTwo.Enabled = false;
                BtnTrhee.Enabled = false;
                BtnFour.Enabled = false;
                BtnFive.Enabled = false;
                BtnSix.Enabled = false;
                BtnSeven.Enabled = false;
                BtnEight.Enabled = false;
                BtnNine.Enabled = false;
                //
                FechaDesde.Enabled = false;
                BtnRecargarData.Enabled = false;
                BtnBuscar.Enabled = false;
                TxtBuscar.ReadOnly = true;
                //
                BtnAgregar.Enabled = false;
                BtnQuitar.Enabled = false;
                //
                //
                BtnRealizarOtraVenta.Enabled = true;
                BtnTerminarVenta.Enabled = true;
            }
        }

        private void GridProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Al seleccionar un prodcuto, se muestra su imagen, si es que posee una 
            int _idSeleccionado = (int)GridProductos.CurrentRow.Cells["Id"].Value;
            //
            var productoSeleccionado = unitOfWork.ProductoRepository.GetByID(_idSeleccionado);
            //
            if (productoSeleccionado.Imagen != null)
            {
                PctProducto.Image = (Bitmap)((new ImageConverter()).ConvertFrom(productoSeleccionado.Imagen));
            }
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            var aux = Convert.ToDecimal(TxtDineroCliente.Text);
            //
            if (aux < PrecioAcumulado)
            {
                MessageBox.Show($"El dinero del cliente es menor al importe de la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Vuelto = aux - PrecioAcumulado;
                TxtVuelto.Text = "$" + Vuelto.ToString("0.00");
                //
                BtnGuardarVenta.Enabled = true;
                BtnGuardarDeuda.Enabled = true;
            }
        }

        private async void BtnRealizarOtraVenta_Click(object sender, EventArgs e)
        {
            ticket.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta).Include(q => q.Venta), filter: q => q.Pagado == "Si" && q.CodigoDeVenta.Equals(ClasesCompartidas.CodigoDeVenta));
            //
            TicketViewReport ticketViewReport = new TicketViewReport(ticket);
            ticketViewReport.ShowDialog();
        }

        private async void BtnCancelar_Click(object sender, EventArgs e)
        {
            ErrorMensaje = 0;
            // Al presionar este boton los productos en el carrito se eliminan y los que estan en venta, su columna carrito vuelve a 0
            //
            int IdProducto = 0;
            //
            int IdVentaDetalle = 0;
            //
            int StockDevuelto = 0;
            //
            int RowsCount = 0;
            //
            int RowsProductsCount = (int)GridProductos.Rows.Count - 1;
            //
            #region VariablesParaComparar
            int TipoId = 0;
            int MarcaId = 0;
            string Detalles = "-";
            int SpecId = 0;
            //
            RowsCount = GridVentaDetalle.Rows.Count;
            #endregion
            //
            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea cancelar esta venta?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                var loadingView = new LoadingView(RowsCount, RowsProductsCount, unitOfWork);
                loadingView.ShowDialog();
                //
                //foreach (DataGridViewRow row in GridVentaDetalle.Rows)
                //{
                //    for (int v = 0; v < RowsCount; v++)
                //    {
                //        if (Convert.ToString(row.Cells["Pagado"].Value) == "Carrito")
                //        {
                //            IdVentaDetalle = (int)row.Cells["Id"].Value;
                //            //
                //            TipoId = (int)row.Cells["TipoProductoId"].Value;
                //            MarcaId = (int)row.Cells["MarcaId"].Value;
                //            SpecId = (int)row.Cells["SPECId"].Value;
                //            Detalles = (string)row.Cells["Detalles"].Value;
                //            //
                //            StockDevuelto = (int)row.Cells["Cantidad"].Value;
                //            //
                //        }
                //        if (IdVentaDetalle != 0)
                //        {
                //            var ventaDetalle = unitOfWork.VentaDetalleRepository.GetByID(IdVentaDetalle);
                //            //
                //            unitOfWork.VentaDetalleRepository.Delete(ventaDetalle);
                //            unitOfWork.Save();
                //            IdVentaDetalle = 0;
                //        }
                //        //
                //        foreach (DataGridViewRow trhow in GridProductos.Rows)
                //        {
                //            for (int p = 0; p < GridProductos.Rows.Count - 1; p++)
                //            {
                //                if (Convert.ToInt32(TipoId) == Convert.ToInt32(trhow.Cells["TipoProductoId"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(trhow.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(trhow.Cells["SPECId"].Value) && Convert.ToString(Detalles) == Convert.ToString(trhow.Cells["Detalles"].Value))
                //                {
                //                    if (Convert.ToInt32(trhow.Cells["Carrito"].Value) > 0)
                //                    {
                //                        IdProducto = (int)trhow.Cells["Id"].Value;
                //                    }
                //                    //
                //                    if (IdProducto != 0)
                //                    {
                //                        Producto producto = new Producto()
                //                        {
                //                            Id = IdProducto,
                //                            TipoProductoId = (int)trhow.Cells["TipoProductoId"].Value,
                //                            MarcaId = (int)trhow.Cells["MarcaId"].Value,
                //                            SPECId = (int)trhow.Cells["SPECId"].Value,
                //                            ProveedorId = (int?)trhow.Cells["ProveedorId"].Value,
                //                            Detalles = (string?)trhow.Cells["Detalles"].Value,
                //                            PrecioBulto = (decimal)trhow.Cells["PrecioBulto"].Value,
                //                            CantidadBulto = (int)trhow.Cells["CantidadBulto"].Value,
                //                            PrecioUnidad = (decimal)trhow.Cells["PrecioUnidad"].Value,
                //                            Ganancia = (int)trhow.Cells["Ganancia"].Value,
                //                            PVP = (decimal)trhow.Cells["PVP"].Value,
                //                            Stock = (int)trhow.Cells["Stock"].Value + StockDevuelto,
                //                            Modificacion = (DateTime)trhow.Cells["Modificacion"].Value,
                //                            Visible = true,
                //                            UsuarioId = (int)trhow.Cells["UsuarioId"].Value,
                //                            Imagen = (byte[]?)trhow.Cells["Imagen"].Value,
                //                            Carrito = 0
                //                        };
                //                        try
                //                        {
                //                            new ModelsValidator().Validate(producto);
                //                            //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //                            //if (respuesta == DialogResult.Yes)
                //                            //{
                //                            unitOfWork.ProductoRepository.Update(producto);
                //                            unitOfWork.Save();
                //                            IdProducto = 0;
                //                        }
                //                        catch (Exception ex)
                //                        {
                //                            Debug.Print(ex.Message);
                //                            Debug.Print(ex.InnerException?.Message);
                //                            MessageBox.Show(ex.Message);
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }

                //}
                // A continuación se actualizan la lista delos datos
                //ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                GetGridsProductsDatas();
            }
            //
            LblCantidadCarrito.Text = "0";
            //
            BtnCancelar.Enabled = false;
            //
            LblImporte.Text = " ";
            TxtDineroCliente.Text = " ";
            TxtVuelto.Text = " ";
            //
            NumCantidad.Value = 1;
            //
            tabControl1.SelectedIndex = 0;
            //
            LblCantidadCarrito.Text = "0";
            //
            PrecioAcumulado = 0;
            CantidadCarrito = 0;
            Vuelto = 0;
            //
            MessageBox.Show($"La venta se ha cancelado con éxito", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnZero_Click(object sender, EventArgs e)
        {
            var Zero = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{Zero}" + "0";
        }

        private void BtnOne_Click(object sender, EventArgs e)
        {
            var One = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{One}" + "1";
        }

        private void BtnTwo_Click(object sender, EventArgs e)
        {
            var Two = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{Two}" + "2";
        }

        private void BtnTrhee_Click(object sender, EventArgs e)
        {
            var Trhee = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{Trhee}" + "3";
        }

        private void BtnFour_Click(object sender, EventArgs e)
        {
            var Four = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{Four}" + "4";
        }

        private void BtnFive_Click(object sender, EventArgs e)
        {
            var Five = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{Five}" + "5";
        }

        private void BtnSix_Click(object sender, EventArgs e)
        {
            var Six = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{Six}" + "6";
        }

        private void BtnSeven_Click(object sender, EventArgs e)
        {
            var Seven = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{Seven}" + "7";
        }

        private void BtnEight_Click(object sender, EventArgs e)
        {
            var Eight = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{Eight}" + "8";
        }

        private void BtnNine_Click(object sender, EventArgs e)
        {
            var Nine = TxtDineroCliente.Text;
            TxtDineroCliente.Text = $"{Nine}" + "9";
        }

        private void BtnDis_Click(object sender, EventArgs e)
        {
            // Este boton mal nombrado, es el boton DEL, abreviación de delete, el cual borra el ultimo caracter ingresado en el textbox en el cual se ingresa el dinero del cliente
            if (TxtDineroCliente.Text.Length == 0)
            {

            }
            else
            {
                //
                var aux = TxtDineroCliente.Text;
                TxtDineroCliente.Text = aux.Remove(aux.Length - 1);
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            ErrorMensaje = 1;
            //
            Producto? _producto = null;
            VentaDetalle? _ventaDetalle = null;
            bool _repetido = false;
            //
            int StockActual = (int)GridProductos.CurrentRow.Cells["Stock"].Value;
            //
            int IdProducto = (int)GridProductos.CurrentRow.Cells["Id"].Value;
            //
            int CarritoActual = (int)GridProductos.CurrentRow.Cells["Carrito"].Value;
            //
            int CantBulto = 0;
            //
            int CantidadDeseada;
            CantidadDeseada = (int)NumCantidad.Value;
            //
            if (RadioBulto.Checked == true)
            {
                PrecioAcumulado = PrecioAcumulado + ((decimal)GridProductos.CurrentRow.Cells["PrecioBulto"].Value * CantidadDeseada);
            }
            else
            {
                PrecioAcumulado = PrecioAcumulado + ((decimal)GridProductos.CurrentRow.Cells["PVP"].Value * CantidadDeseada);
            }
            LblImporte.Text = "$" + PrecioAcumulado.ToString("0.00");
            //
            if (RadioBulto.Checked == true)
            {
                CantidadCarrito = CantidadCarrito + CantidadDeseada * Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value);
            }
            else
            {
                CantidadCarrito = CantidadCarrito + CantidadDeseada;
            }
            LblCantidadCarrito.Text = Convert.ToString(CantidadCarrito);
            //
            if (CantidadCarrito >= 1)
            {
                BtnCancelar.Enabled = true;
                BtnGuardarDeuda.Enabled = true;
                TxtDineroCliente.ReadOnly = false;
                //
                BtnZero.Enabled = true;
                BtnOne.Enabled = true;
                BtnTwo.Enabled = true;
                BtnTrhee.Enabled = true;
                BtnFour.Enabled = true;
                BtnFive.Enabled = true;
                BtnSix.Enabled = true;
                BtnSeven.Enabled = true;
                BtnEight.Enabled = true;
                BtnNine.Enabled = true;
            }
            //
            if (StockActual < 1)
            {
                MessageBox.Show($"El producto que intenta vender se encuentra sin stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //// Color
                //GridProductos.CurrentRow.DefaultCellStyle.BackColor = Color.FromArgb(226, 41, 41);
                //GridProductos.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 66, 66);
            }
            else
            {
                if (StockActual < CantidadDeseada || StockActual < CantidadDeseada * Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value))
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
                        VentaDetalle ventaDetalle = new VentaDetalle()
                        {
                            TipoProductoId = Convert.ToInt32(GridProductos.CurrentRow.Cells["TipoProductoId"].Value),
                            MarcaId = Convert.ToInt32(GridProductos.CurrentRow.Cells["MarcaId"].Value),
                            Detalles = (string)(GridProductos.CurrentRow.Cells["Detalles"].Value),
                            SPECId = Convert.ToInt32(GridProductos.CurrentRow.Cells["SPECId"].Value),
                            ProveedorId = Convert.ToInt32(GridProductos.CurrentRow.Cells["ProveedorId"].Value),
                            Bulto = true,
                            CantidadBultos = CantidadDeseada,
                            CantidadXBultos = Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value),
                            PrecioBulto = Convert.ToInt32(GridProductos.CurrentRow.Cells["PrecioBulto"].Value),
                            PVP = 0,
                            Cantidad = CantidadDeseada * Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value),
                            CuentaId = 1,
                            FechaDePago = DateTime.Now,
                            Pagado = "Carrito",
                            Imagen = (byte[]?)GridProductos.CurrentRow.Cells["Imagen"].Value,
                            UsuarioId = (int)ClasesCompartidas.UserId

                        };
                        _ventaDetalle = ventaDetalle;
                    }
                    else
                    {
                        VentaDetalle ventaDetalle = new VentaDetalle()
                        {
                            TipoProductoId = Convert.ToInt32(GridProductos.CurrentRow.Cells["TipoProductoId"].Value),
                            MarcaId = Convert.ToInt32(GridProductos.CurrentRow.Cells["MarcaId"].Value),
                            Detalles = (string)(GridProductos.CurrentRow.Cells["Detalles"].Value),
                            SPECId = Convert.ToInt32(GridProductos.CurrentRow.Cells["SPECId"].Value),
                            ProveedorId = Convert.ToInt32(GridProductos.CurrentRow.Cells["ProveedorId"].Value),
                            Bulto = false,
                            CantidadBultos = 0,
                            CantidadXBultos = 0,
                            PrecioBulto = 0,
                            PVP = Convert.ToDecimal(GridProductos.CurrentRow.Cells["PVP"].Value),
                            Cantidad = CantidadDeseada,
                            CuentaId = 1,
                            FechaDePago = DateTime.Now,
                            Pagado = "Carrito",
                            Imagen = (byte[]?)GridProductos.CurrentRow.Cells["Imagen"].Value,
                            UsuarioId = (int)ClasesCompartidas.UserId

                        };
                        //
                        _ventaDetalle = ventaDetalle;
                    }
                    //
                    bool bultoExistente = true;
                    bool productoExistente = true;
                    // En el siguiente bool se comprueba si el producto enviado ya se encuentra cargado en el carrito
                    // por lo tanto se deberia de agregar una unidad nueva a la cantidad de articulos 
                    if (RadioBulto.Checked == true)
                    {
                        bultoExistente = unitOfWork.VentaDetalleRepository.dbSet.Any(x => x.TipoProductoId.Equals(_ventaDetalle.TipoProductoId) && x.MarcaId.Equals(_ventaDetalle.MarcaId) && x.SPECId.Equals(_ventaDetalle.SPECId) && x.Detalles.Equals(_ventaDetalle.Detalles) && x.Pagado == _ventaDetalle.Pagado && x.Bulto == true);
                    }
                    else
                    {
                        productoExistente = unitOfWork.VentaDetalleRepository.dbSet.Any(x => x.TipoProductoId.Equals(_ventaDetalle.TipoProductoId) && x.MarcaId.Equals(_ventaDetalle.MarcaId) && x.SPECId.Equals(_ventaDetalle.SPECId) && x.Detalles.Equals(_ventaDetalle.Detalles) && x.Pagado == _ventaDetalle.Pagado && x.Bulto == false);
                    }
                    //
                    if (!bultoExistente || !productoExistente) // Nuevo producto en el carrito 
                    {
                        var productoResStock = unitOfWork.ProductoRepository.GetByID(IdProducto);
                        if (RadioUnidad.Checked == true)
                        {
                            productoResStock.Carrito = productoResStock.Carrito + CantidadDeseada;
                            productoResStock.Stock = StockActual - CantidadDeseada;
                        }
                        else
                        {
                            productoResStock.Carrito = productoResStock.Carrito + (CantidadDeseada * Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value));
                            productoResStock.Stock = StockActual - CantidadDeseada * Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value);
                        }
                        //
                        _producto = productoResStock;
                        _repetido = false;
                    }
                    else
                    {   //  ProductoRepetido
                        #region VariablesParaComparar
                        int idEncontrado = 0;
                        int TipoId = (int)GridProductos.CurrentRow.Cells["TipoProductoId"].Value;
                        int MarcaId = (int)GridProductos.CurrentRow.Cells["MarcaId"].Value;
                        string Detalles = (string)GridProductos.CurrentRow.Cells["Detalles"].Value;
                        int SpecId = (int)GridProductos.CurrentRow.Cells["SPECId"].Value;
                        string Pagado = "Carrito";
                        bool bulto = true;
                        #endregion
                        // foreach para encontrar el id del producto repetido
                        foreach (DataGridViewRow row in GridVentaDetalle.Rows)
                        {
                            if (RadioBulto.Checked == true)
                            {
                                if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && Pagado == Convert.ToString(row.Cells["Pagado"].Value) && (bool)row.Cells["Bulto"].Value == true)
                                {
                                    idEncontrado = (int)row.Cells[0].Value;
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && Pagado == Convert.ToString(row.Cells["Pagado"].Value) && (bool)row.Cells["Bulto"].Value == false)
                                {
                                    idEncontrado = (int)row.Cells[0].Value;
                                }
                            }
                        }

                        if (idEncontrado != 0) // Si se ha encontrado un id dentro de la tabla, es que se trata de un producto repetido por ende es encesario actualizarle su cantidad
                        {
                            // Se averigua la cantidad de articulos que posee el producto en el carrito
                            var productoEdit = unitOfWork.VentaDetalleRepository.GetByID(idEncontrado);
                            var bultazo = productoEdit.Bulto;
                            //
                            if (bultazo == true)
                            {
                                productoEdit.Cantidad = productoEdit.Cantidad + CantidadDeseada * Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value);
                                productoEdit.CantidadBultos = productoEdit.CantidadBultos + CantidadDeseada;
                                _ventaDetalle = productoEdit;
                            }
                            else
                            {
                                productoEdit.Cantidad = productoEdit.Cantidad + CantidadDeseada;
                                _ventaDetalle = productoEdit;
                            }
                            //VentaDetalle productoRepetido = new VentaDetalle()
                            //{
                            //    Id = (int)idEncontrado,
                            //    TipoProductoId = Convert.ToInt32(GridProductos.CurrentRow.Cells["TipoProductoId"].Value),
                            //    MarcaId = Convert.ToInt32(GridProductos.CurrentRow.Cells["MarcaId"].Value),
                            //    Detalles = (string)(GridProductos.CurrentRow.Cells["Detalles"].Value),
                            //    SPECId = Convert.ToInt32(GridProductos.CurrentRow.Cells["SPECId"].Value),
                            //    ProveedorId = Convert.ToInt32(GridProductos.CurrentRow.Cells["ProveedorId"].Value),
                            //    PVP = Convert.ToDecimal(GridProductos.CurrentRow.Cells["PVP"].Value),
                            //    Cantidad = cantidadActual + CantidadDeseada,
                            //    CuentaId = 1,
                            //    FechaDePago = DateTime.Now,
                            //    Pagado = "Carrito",
                            //    Imagen = (byte[]?)GridProductos.CurrentRow.Cells["Imagen"].Value,
                            //    UsuarioId = ClasesCompartidas.UserId
                            //};
                            var productoRestStock2 = unitOfWork.ProductoRepository.GetByID(IdProducto);
                            if (RadioUnidad.Checked == true)
                            {
                                productoRestStock2.Stock = productoRestStock2.Stock - CantidadDeseada;
                                productoRestStock2.Carrito = productoRestStock2.Carrito + CantidadDeseada;
                            }
                            else
                            {
                                productoRestStock2.Stock = productoRestStock2.Stock - CantidadDeseada * Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value);
                                productoRestStock2.Carrito = productoRestStock2.Carrito + CantidadDeseada * Convert.ToInt32(GridProductos.CurrentRow.Cells["CantidadBulto"].Value);
                            }
                            //
                            _producto = productoRestStock2;
                            _repetido = true;
                            //try
                            //{
                            //    new ModelsValidator().Validate(productoResStock);
                            //    new ModelsValidator().Validate(productoRepetido);

                            //    //
                            //    unitOfWork.ProductoRepository.Update(productoResStock);
                            //    unitOfWork.VentaDetalleRepository.Update(productoRepetido);
                            //    unitOfWork.Save();
                            //    //
                            //    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                            //    //VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");
                            //    //
                            //    GetGridsProductsDatas();
                            //}
                            //catch (Exception ex)
                            //{
                            //    Debug.Print(ex.Message);
                            //    Debug.Print(ex.InnerException?.Message);
                            //    MessageBox.Show(ex.Message);
                            //}
                        }
                    }
                    var baseLoading = new BaseLoading(_ventaDetalle, _producto, unitOfWork, _repetido);
                }
            }
            //
            GetGridsProductsDatas();
        }

        private async void BtnQuitar_Click(object sender, EventArgs e)
        {
            // Variables para enviar al LoadingView
            Producto? _producto = null;
            VentaDetalle? _ventaDetalle = null;
            bool _delete = false;
            //
            ErrorMensaje = 1;
            //
            int IdVentaDetalle = (int)GridVentaDetalle.CurrentRow.Cells["Id"].Value;
            //
            int IdProducto = 0;
            //
            //
            int CantidadDeseada;
            CantidadDeseada = (int)NumCantidad.Value;
            #region VariablesParaComparar
            int TipoId = (int)GridVentaDetalle.CurrentRow.Cells["TipoProductoId"].Value;
            int MarcaId = (int)GridVentaDetalle.CurrentRow.Cells["MarcaId"].Value;
            string Detalles = (string)GridVentaDetalle.CurrentRow.Cells["Detalles"].Value;
            int SpecId = (int)GridVentaDetalle.CurrentRow.Cells["SPECId"].Value;
            #endregion
            //
            if (RadioBulto.Checked == true)
            {
                PrecioAcumulado = PrecioAcumulado - ((decimal)GridVentaDetalle.CurrentRow.Cells["PrecioBulto"].Value * CantidadDeseada);
            }
            else
            {
                PrecioAcumulado = PrecioAcumulado - ((decimal)GridProductos.CurrentRow.Cells["PVP"].Value * CantidadDeseada);
            }
            LblImporte.Text = "$" + PrecioAcumulado.ToString("0.00");
            //
            if (RadioBulto.Checked == true)
            {
                CantidadCarrito = CantidadCarrito - CantidadDeseada * Convert.ToInt32(GridVentaDetalle.CurrentRow.Cells["CantidadXBultos"].Value);

            }
            else
            {
                CantidadCarrito = CantidadCarrito - CantidadDeseada;
            }
            LblCantidadCarrito.Text = Convert.ToString(CantidadCarrito);
            //
            if (CantidadCarrito < 1)
            {
                BtnCancelar.Enabled = false;
                BtnGuardarDeuda.Enabled = false;
                TxtDineroCliente.ReadOnly = true;
                //
                BtnZero.Enabled = false;
                BtnOne.Enabled = false;
                BtnTwo.Enabled = false;
                BtnTrhee.Enabled = false;
                BtnFour.Enabled = false;
                BtnFive.Enabled = false;
                BtnSix.Enabled = false;
                BtnSeven.Enabled = false;
                BtnEight.Enabled = false;
                BtnNine.Enabled = false;
            }
            //
            if (Convert.ToInt32(GridVentaDetalle.CurrentRow.Cells["Cantidad"].Value) < CantidadDeseada)
            {
                MessageBox.Show($"La cantidad indicada de productos para quitar, supera la cantidad regitrada en el carrito del mismo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Convert.ToInt32(GridVentaDetalle.CurrentRow.Cells["Cantidad"].Value) == CantidadDeseada || CantidadDeseada == Convert.ToInt32(GridVentaDetalle.CurrentRow.Cells["CantidadBultos"].Value) || Convert.ToInt32(GridVentaDetalle.CurrentRow.Cells["Cantidad"].Value) == Convert.ToInt32(GridVentaDetalle.CurrentRow.Cells["CantidadXBultos"].Value))
                {
                    // Delete 
                    var ventaDetalle = unitOfWork.VentaDetalleRepository.GetByID(IdVentaDetalle);
                    //unitOfWork.VentaDetalleRepository.Delete(ventaDetalle);
                    //
                    foreach (DataGridViewRow row in GridProductos.Rows)
                    {
                        if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value))
                        {
                            IdProducto = Convert.ToInt32(row.Cells["Id"].Value);
                            //
                            //int StockActual = Convert.ToInt32(row.Cells["Stock"].Value);
                        }
                    }
                    var producto = unitOfWork.ProductoRepository.GetByID(IdProducto);
                    producto.Stock = producto.Stock + (int)ventaDetalle.Cantidad;
                    producto.Carrito = 0;

                    //
                    _producto = producto;
                    _ventaDetalle = ventaDetalle;
                    _delete = true;
                    //try
                    //{
                    //    new ModelsValidator().Validate(ventaDetalle);
                    //    new ModelsValidator().Validate(producto);
                    //    //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //    //if (respuesta == DialogResult.Yes)
                    //    //{
                    //    unitOfWork.VentaDetalleRepository.Update(ventaDetalle);
                    //    unitOfWork.ProductoRepository.Update(producto);
                    //    unitOfWork.Save();
                    //    //
                    //    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    //    //VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");
                    //    GetGridsProductsDatas();

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
                    // Quitar 
                    var ventaDetalle = unitOfWork.VentaDetalleRepository.GetByID(IdVentaDetalle);
                    bool bulto = ventaDetalle.Bulto;
                    //
                    if (bulto == true)
                    {
                        ventaDetalle.CantidadBultos = ventaDetalle.CantidadBultos - CantidadDeseada;
                        ventaDetalle.Cantidad = ventaDetalle.Cantidad - CantidadDeseada * Convert.ToInt32(GridVentaDetalle.CurrentRow.Cells["CantidadxBultos"].Value);
                    }
                    else
                    {
                        ventaDetalle.Cantidad = ventaDetalle.Cantidad - CantidadDeseada;
                    }
                    //
                    foreach (DataGridViewRow row in GridProductos.Rows)
                    {
                        if (Convert.ToInt32(TipoId) == Convert.ToInt32(row.Cells["TipoProductoId"].Value) && Convert.ToInt32(MarcaId) == Convert.ToInt32(row.Cells["MarcaId"].Value) && Convert.ToInt32(SpecId) == Convert.ToInt32(row.Cells["SPECId"].Value) && Convert.ToString(Detalles) == Convert.ToString(row.Cells["Detalles"].Value))
                        {
                            IdProducto = Convert.ToInt32(row.Cells["Id"].Value);
                            //
                            //int StockActual = Convert.ToInt32(row.Cells["Stock"].Value);
                        }
                    }
                    var producto = unitOfWork.ProductoRepository.GetByID(IdProducto);
                    //
                    if (RadioBulto.Checked == true)
                    {
                        producto.Carrito = producto.Carrito - CantidadDeseada * Convert.ToInt32(GridVentaDetalle.CurrentRow.Cells["CantidadxBultos"].Value);
                        producto.Stock = producto.Stock + CantidadDeseada * Convert.ToInt32(GridVentaDetalle.CurrentRow.Cells["CantidadxBultos"].Value);
                    }
                    else
                    {
                        producto.Carrito = producto.Carrito - CantidadDeseada;
                        producto.Stock = producto.Stock + CantidadDeseada;
                    }
                    //
                    _producto = producto;
                    _ventaDetalle = ventaDetalle;
                    if (ventaDetalle.Cantidad == CantidadDeseada)
                        _delete = true;
                    else
                        _delete = false;
                    //try
                    //{
                    //    new ModelsValidator().Validate(ventaDetalle);
                    //    new ModelsValidator().Validate(producto);
                    //    //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //    //if (respuesta == DialogResult.Yes)
                    //    //{
                    //    unitOfWork.VentaDetalleRepository.Delete(ventaDetalle);
                    //    unitOfWork.ProductoRepository.Update(producto);
                    //    unitOfWork.Save();
                    //    //
                    //    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                    //    //VentaDetalle.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor), filter: q => q.Pagado == "Carrito");
                    //    GetGridsProductsDatas();

                    //}
                    //catch (Exception ex)
                    //{
                    //    Debug.Print(ex.Message);
                    //    Debug.Print(ex.InnerException?.Message);
                    //    MessageBox.Show(ex.Message);
                    //}
                }
            }
            var miniloading = new MiniLoading(_delete, _ventaDetalle, _producto, unitOfWork);
            miniloading.ShowDialog();
            //
            GetGridsProductsDatas();
        }

        private void LblImporte_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtVuelto_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtDineroCliente_TextChanged(object sender, EventArgs e)
        {
            if (TxtDineroCliente.Text.Length == 0)
            {
                BtnCalcular.Enabled = false;
                BtnDis.Enabled = false;
            }
            else
            {
                BtnCalcular.Enabled = true;
                BtnDis.Enabled = true;
            }
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

        private async void BtnGuardarDeuda_Click(object sender, EventArgs e)
        {
            var CuentaNombre = Convert.ToString(ComboCuentas.SelectedItem);
            //
            var _precioAcum = PrecioAcumulado;
            //
            var _cantidad = Convert.ToInt32(LblCantidadCarrito.Text);
            //
            var _cuentaId = (int)ComboCuentas.SelectedValue;
            //
            ErrorMensaje = 0;
            //
            int IdProducto = 0;
            //
            int IdVentaDetalle = 0;
            //
            int _codigoVenta = 0;
            //
            int RowsCount = 0;
            //
            int RowsProductCount = (int)GridProductos.Rows.Count - 1;
            //
            if (GridVentaDetalle.Rows.Count == 1)
            {
                RowsCount = GridVentaDetalle.Rows.Count;
            }
            else
            {
                RowsCount = (int)GridVentaDetalle.Rows.Count - 1;
            }
            //
            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea crear esta deuda en la cuenta {CuentaNombre}?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                var saveView = new SaveView(RowsCount, RowsProductCount, unitOfWork, _precioAcum, _cantidad, _cuentaId);
                saveView.ShowDialog();
                //
                //foreach (DataGridViewRow row in GridProductos.Rows)
                //{
                //    for (int i = 0; i < GridProductos.Rows.Count; i++)
                //    {
                //        if (Convert.ToInt32(row.Cells["Carrito"].Value) > 0)
                //        {
                //            IdProducto = (int)row.Cells["Id"].Value;
                //        }
                //        //
                //        if (IdProducto != 0)
                //        {
                //            Producto producto = new Producto()
                //            {
                //                Id = IdProducto,
                //                TipoProductoId = (int)row.Cells["TipoProductoId"].Value,
                //                MarcaId = (int)row.Cells["MarcaId"].Value,
                //                SPECId = (int)row.Cells["SPECId"].Value,
                //                ProveedorId = (int?)row.Cells["ProveedorId"].Value,
                //                Detalles = (string?)row.Cells["Detalles"].Value,
                //                PrecioBulto = (decimal)row.Cells["PrecioBulto"].Value,
                //                CantidadBulto = (int)row.Cells["CantidadBulto"].Value,
                //                PrecioUnidad = (decimal)row.Cells["PrecioUnidad"].Value,
                //                Ganancia = (int)row.Cells["Ganancia"].Value,
                //                PVP = (decimal)row.Cells["PVP"].Value,
                //                Stock = (int)row.Cells["Stock"].Value,
                //                Modificacion = (DateTime)row.Cells["Modificacion"].Value,
                //                Visible = true,
                //                UsuarioId = (int)row.Cells["UsuarioId"].Value,
                //                Imagen = (byte[]?)GridProductos.CurrentRow.Cells["Imagen"].Value,
                //                Carrito = 0
                //            };
                //            try
                //            {
                //                new ModelsValidator().Validate(producto);
                //                //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //                //if (respuesta == DialogResult.Yes)
                //                //{
                //                unitOfWork.ProductoRepository.Update(producto);
                //                unitOfWork.Save();
                //                IdProducto = 0;
                //            }
                //            catch (Exception ex)
                //            {
                //                Debug.Print(ex.Message);
                //                Debug.Print(ex.InnerException?.Message);
                //                MessageBox.Show(ex.Message);
                //            }
                //        }
                //    }
                //}
                ////
                //var oldCodigoVenta = unitOfWork.CodigoVentaRepository.GetByID(1);
                //_codigoVenta = oldCodigoVenta.Codigo + 1;
                ////
                //CodigoVenta codigoVenta = new CodigoVenta()
                //{
                //    Id = 1,
                //    Codigo = _codigoVenta
                //};
                //try
                //{
                //    new ModelsValidator().Validate(codigoVenta);
                //    //
                //    unitOfWork.CodigoVentaRepository.Update(codigoVenta);
                //    unitOfWork.Save();
                //}
                //catch (Exception ex)
                //{
                //    Debug.Print(ex.Message);
                //    Debug.Print(ex.InnerException?.Message);
                //    MessageBox.Show(ex.Message);
                //}
                ////
                //foreach (DataGridViewRow row in GridVentaDetalle.Rows)
                //{
                //    for (int i = 0; i < RowsCount; i++)
                //    {

                //    }
                //    if (Convert.ToString(row.Cells["Pagado"].Value) == "Carrito")
                //    {
                //        IdVentaDetalle = (int)row.Cells["Id"].Value;
                //    }
                //    //
                //    if (IdVentaDetalle != 0)
                //    {

                //        VentaDetalle productoRepetido = new VentaDetalle()
                //        {
                //            Id = IdVentaDetalle,
                //            TipoProductoId = (int)row.Cells["TipoProductoId"].Value,
                //            MarcaId = (int)row.Cells["MarcaId"].Value,
                //            Detalles = (string)(GridProductos.CurrentRow.Cells["Detalles"].Value),
                //            SPECId = (int)row.Cells["SPECId"].Value,
                //            ProveedorId = (int?)row.Cells["ProveedorId"].Value,
                //            PVP = (decimal)row.Cells["PVP"].Value,
                //            Cantidad = (int)row.Cells["Cantidad"].Value,
                //            CuentaId = (int)ComboCuentas.SelectedValue,
                //            FechaDePago = DateTime.Now,
                //            Pagado = "No",
                //            Imagen = (byte[]?)row.Cells["Imagen"].Value,
                //            UsuarioId = ClasesCompartidas.UserId,
                //            CodigoDeVenta = _codigoVenta
                //        };
                //        try
                //        {
                //            new ModelsValidator().Validate(productoRepetido);
                //            //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //            //if (respuesta == DialogResult.Yes)
                //            //{
                //            unitOfWork.VentaDetalleRepository.Update(productoRepetido);
                //            unitOfWork.Save();
                //            IdVentaDetalle = 0;
                //        }
                //        catch (Exception ex)
                //        {
                //            Debug.Print(ex.Message);
                //            Debug.Print(ex.InnerException?.Message);
                //            MessageBox.Show(ex.Message);
                //        }
                //    }
                //}
                ////
                //Venta venta = new Venta()
                //{
                //    Importe = PrecioAcumulado,
                //    Articulos = Convert.ToInt32(LblCantidadCarrito.Text),
                //    CuentaId = (int)ComboCuentas.SelectedValue,
                //    Estado = "Deuda",
                //    Fecha = DateTime.Now,
                //    Codigo = _codigoVenta,
                //    UsuarioId = ClasesCompartidas.UserId
                //};
                //try
                //{
                //    new ModelsValidator().Validate(venta);
                //    //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    //if (respuesta == DialogResult.Yes)
                //    //{
                //    unitOfWork.VentaRepository.Add(venta);
                //    unitOfWork.Save();
                //}
                //catch (Exception ex)
                //{
                //    Debug.Print(ex.Message);
                //    Debug.Print(ex.InnerException?.Message);
                //    MessageBox.Show(ex.Message);
                //}
                //// Es indispensable añadir el precio acumulado a la cuenta a la que se le ha agregado la deuda
                //var cuenta = unitOfWork.CuentaRepository.GetByID((int)ComboCuentas.SelectedValue);
                ////
                //if (cuenta.Deuda == null)
                //{
                //    cuenta.Deuda = PrecioAcumulado;
                //}
                //else
                //{
                //    cuenta.Deuda = cuenta.Deuda + PrecioAcumulado;
                //}
                ////
                //try
                //{
                //    new ModelsValidator().Validate(cuenta);
                //    //DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar esta venta?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    //if (respuesta == DialogResult.Yes)
                //    //{
                //    unitOfWork.CuentaRepository.Update(cuenta);
                //    unitOfWork.Save();
                //}
                //catch (Exception ex)
                //{
                //    Debug.Print(ex.Message);
                //    Debug.Print(ex.InnerException?.Message);
                //    MessageBox.Show(ex.Message);
                //}
                // A continuación se actualizan los datos recientemente cargados
                //ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                MessageBox.Show($"La venta se ha guardado como deuda en la cuenta {CuentaNombre} con éxito", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //
            // Al guardar la deuda, ciertas funciones se deshabilitan para evitar errores
            BtnGuardarDeuda.Enabled = false;
            BtnCancelar.Enabled = false;
            BtnGuardarVenta.Enabled = false;
            //
            TxtDineroCliente.ReadOnly = true;
            //
            BtnCalcular.Enabled = false;
            BtnDis.Enabled = false;
            //
            BtnZero.Enabled = false;
            BtnOne.Enabled = false;
            BtnTwo.Enabled = false;
            BtnTrhee.Enabled = false;
            BtnFour.Enabled = false;
            BtnFive.Enabled = false;
            BtnSix.Enabled = false;
            BtnSeven.Enabled = false;
            BtnEight.Enabled = false;
            BtnNine.Enabled = false;
            //
            FechaDesde.Enabled = false;
            BtnRecargarData.Enabled = false;
            BtnBuscar.Enabled = false;
            TxtBuscar.ReadOnly = true;
            //
            BtnAgregar.Enabled = false;
            BtnQuitar.Enabled = false;
            //
            BtnTerminarVenta.Enabled = true;
        }

        private void ComboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ComboSpec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboTipo_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void ComboMarca_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void ComboSpec_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void MakeSaleView_Activated(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                BtnAgregar.Enabled = true;
                //
                BtnQuitar.Enabled = false;
            }
            else
            {
                BtnAgregar.Enabled = false;
                //
                BtnQuitar.Enabled = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                BtnAgregar.Enabled = true;
                //
                BtnQuitar.Enabled = false;
            }
            else
            {
                BtnAgregar.Enabled = false;
                //
                BtnQuitar.Enabled = true;
            }
        }

        private void BtnTerminarVenta_Click(object sender, EventArgs e)
        {
            // Al terminar la venta, ciertas funciones se habilitan para dar lugar a una nueva venta
            //
            //
            BtnRealizarOtraVenta.Enabled = false;
            FechaDesde.Enabled = true;
            BtnRecargarData.Enabled = true;
            BtnBuscar.Enabled = true;
            TxtBuscar.ReadOnly = false;
            //
            BtnAgregar.Enabled = true;
            BtnQuitar.Enabled = false;
            //
            LblImporte.Text = " ";
            TxtDineroCliente.Text = " ";
            TxtVuelto.Text = " ";
            //
            NumCantidad.Value = 1;
            //
            tabControl1.SelectedIndex = 0;
            //
            LblCantidadCarrito.Text = "0";
            //
            PrecioAcumulado = 0;
            CantidadCarrito = 0;
            Vuelto = 0;
            //
            GetGridsProductsDatas();
            //
            BtnTerminarVenta.Enabled = false;
        }

        private void BtnAgregarCuenta_Click(object sender, EventArgs e)
        {
            bool editando = false;
            bool makesale = true;
            //
            var createAccount = new CreateAccount(unitOfWork, editando, makesale);
            createAccount.ShowDialog();
            //
            if (ClasesCompartidas.CuentaNueva != null)
            {
                GetComboCuentas();
            }
            //
            ClasesCompartidas.CuentaNueva = null;
        }

        private void GridProductos_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}
