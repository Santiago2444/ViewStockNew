using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;
using ViewStockNew.ViewReport;

namespace ViewStockNew.Views
{
    public partial class AccountsView : Form
    {
        IUnitOfWork unitOfWork;
        private int IdCuenta;
        BindingSource comprobantePago = new BindingSource();
        BindingSource comprobanteSaldo = new BindingSource();
        //
        #region BindingSource's

        private int DataCompled;
        private string DeudaCuenta;
        private decimal? DeudaDecimal;
        private decimal? SaldoDecimal;
        private decimal _importe;
        private int IdDeuda;
        private decimal _dinero;
        private decimal _vuelto;
        private int CodigoVespecifico;
        private bool FilterDC = false;
        private bool FilterDP = false;
        private int FilterUsuariosVenta;
        private int FilterFechaVenta;
        private int FilterTipoId;
        private int FilterMarcaId;
        private int FilterSpecId;
        private int FilterProveedorId;
        private DateTime FechaHastaSend;
        private DateTime FechaDesdeSend;
        private bool Cfilter = false;
        private bool Pfilter = false;
        #endregion

        public AccountsView(IUnitOfWork unitOfWork, int idCuenta)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            this.IdCuenta = idCuenta;
            //
            GetAccountData();
            FillGrids();
            FillCombos();
            //
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.BtnSearchCompras, "Buscar");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.BtnSearchProductos, "Buscar");
            //
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.DeudaSearchCompras, "Buscar");
            //
            System.Windows.Forms.ToolTip ToolTip4 = new System.Windows.Forms.ToolTip();
            ToolTip4.SetToolTip(this.DeudaSearchProductos, "Buscar");
            //
            System.Windows.Forms.ToolTip ToolTip5 = new System.Windows.Forms.ToolTip();
            ToolTip5.SetToolTip(this.BtnCancelarPago, "Cancelar Pago");
            //
            System.Windows.Forms.ToolTip ToolTip6 = new System.Windows.Forms.ToolTip();
            ToolTip6.SetToolTip(this.BtnCalcular, "Calcular Pago");
            //
            System.Windows.Forms.ToolTip ToolTip7 = new System.Windows.Forms.ToolTip();
            ToolTip7.SetToolTip(this.BtnGuardarPago, "Guardar Pago");
            //
            System.Windows.Forms.ToolTip ToolTip8 = new System.Windows.Forms.ToolTip();
            ToolTip8.SetToolTip(this.BtnRefrescarVentas, "Recargar");
            //
            System.Windows.Forms.ToolTip ToolTip9 = new System.Windows.Forms.ToolTip();
            ToolTip9.SetToolTip(this.BtnFiltrar, "Filtrar");
        }

        private void FillCombos()
        {
            // CargaDeCombosMarcas
            ComboMarcas.DisplayMember = "Nombre";
            ComboMarcas.ValueMember = "Id";
            ComboMarcas.DataSource = ClasesCompartidas.marcasList;

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

            // CargaDeComboUsuario
            ComboUsuarios.DisplayMember = "Nombre";
            ComboUsuarios.ValueMember = "Id";
            ComboUsuarios.DataSource = ClasesCompartidas.usuariosList;
        }

        private void GetAccountData()
        {
            var cuenta = unitOfWork.CuentaRepository.GetByID(IdCuenta);
            //
            LblCuentaNombre.Text = cuenta.Nombre;
            LblDNI.Text = cuenta.DNI;
            DeudaDecimal = cuenta.Deuda;
            SaldoDecimal = cuenta.Saldo;
            decimal labelDeuda = Convert.ToDecimal(cuenta.Deuda);
            decimal labelSaldo = Convert.ToDecimal(cuenta.Saldo);
            //
            if (cuenta.Deuda != null)
            {
                LblDeuda.Text = $"${labelDeuda:0.00}";
                DeudaCuenta = $"${labelDeuda:0.00}";
            }
            //
            if (cuenta.Saldo != null)
            {
                LblSaldo.Text = $"${labelSaldo:0.00}";
            }
            //
            var SubWidth = LblCuentaNombre.Width;
            PctSubName.Width = SubWidth;
        }

        private async void GetData()
        {
            //ComprasList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado" && q.CuentaId.Equals(IdCuenta));
            //ProductosList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta));
            //// PagosList.DataSource PENDIENTE
            ////
            //ComprasDeudaList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(IdCuenta));
            //ProductosDeudaList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(IdCuenta));
            //// PagosDeudaList.DataSource PENDIENTE
            //DataCompled = 1;
        }

        private void FillGrids()
        {
            GridCompras.DataSource = ClasesCompartidas.ComprasList;
            GridProductos.DataSource = ClasesCompartidas.ProductosList;
            GridPagos.DataSource = ClasesCompartidas.PagosList;
            //
            GridComprasDeudas.DataSource = ClasesCompartidas.ComprasDeudaList;
            GridProductosDeudas.DataSource = ClasesCompartidas.ProductosDeudaList;
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

        private void GridCompras_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridCompras.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn columna in GridCompras.Columns)
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
            };
        }

        private void GridProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridProductos.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn columnaD in GridProductos.Columns)
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

        private void GridComprasDeudas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridComprasDeudas.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn columna in GridComprasDeudas.Columns)
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
            };
        }

        private void GridProductosDeudas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridProductosDeudas.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn columnaD in GridProductosDeudas.Columns)
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

        private void CheckComprasFilter_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckFiltroProductos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckDeudasCompras_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckDeudasProductos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LblSaldo_Click(object sender, EventArgs e)
        {

        }

        private void LblSaldo_TextChanged(object sender, EventArgs e)
        {
            var aux = LblDeuda.Text;
            if (aux.Length > 26)
            {
                LblDeuda.Text = $"${aux.Remove(aux.Length - 23)}";
                DeudaCuenta = LblDeuda.Text;
            }
        }

        private void GridComprasDeudas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var codigoVenta = (int)GridComprasDeudas.CurrentRow.Cells["Codigo"].Value;
            //
            var miniLoading = new MiniLoading(codigoVenta, IdCuenta, unitOfWork);
            miniLoading.ShowDialog();
            //
            GridProductosDeudas.DataSource = ClasesCompartidas.DeudaSelected;
            //
            if (RadioEspecífico.Checked == true)
            {
                TxtImporte.Text = $"${Convert.ToDecimal(GridComprasDeudas.CurrentRow.Cells["Importe"].Value):0.00}";
                CodigoVespecifico = Convert.ToInt32(GridComprasDeudas.CurrentRow.Cells["Codigo"].Value);
                _importe = Convert.ToDecimal(GridComprasDeudas.CurrentRow.Cells["Importe"].Value);
                IdDeuda = Convert.ToInt32(GridComprasDeudas.CurrentRow.Cells["Id"].Value);
            }

        }

        private void RadioTotal_CheckedChanged(object sender, EventArgs e)
        {
            TxtImporte.Text = DeudaCuenta;
            TxtDinero.ReadOnly = false;
            BtnCancelarPago.Enabled = true;
            BtnCalcular.Enabled = true;
        }

        private void RadioParcial_CheckedChanged(object sender, EventArgs e)
        {
            TxtDinero.ReadOnly = false;
            BtnCalcular.Enabled = true;
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            if (RadioTotal.Checked == true)
            {
                if (TxtDinero.Text.Length < 1)
                {
                    MessageBox.Show($"Es necesario completar el campo 'Dinero'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    decimal dinero = Convert.ToDecimal(TxtDinero.Text); ;
                    decimal importe = Convert.ToDecimal(DeudaDecimal);
                    //
                    if (SaldoDecimal != 0)
                    {
                        dinero = Convert.ToDecimal(TxtDinero.Text) + Convert.ToDecimal(SaldoDecimal);
                        //
                        var aux = LblSaldo.Text;
                        LblSaldo.Text = $"{aux}+";
                        LblSaldo.ForeColor = Color.SeaGreen;
                        //
                        TxtDinero.Text = $"${dinero:0.00}";
                    }
                    //
                    _importe = importe;
                    _dinero = dinero;
                    //
                    if (importe > dinero)
                    {
                        MessageBox.Show($"El dinero ingresado no es suficiente para saldar la deuda total", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        decimal vuelto = dinero - importe;
                        TxtVuelto.Text = $"${vuelto:0.00}";
                        //
                        _vuelto = vuelto;
                        //
                        FillGrids();
                        //
                        foreach (DataGridViewRow row in GridComprasDeudas.Rows)
                        {
                            row.DefaultCellStyle.BackColor = Color.SeaGreen;
                            row.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                        }
                        //
                        foreach (DataGridViewRow row in GridProductosDeudas.Rows)
                        {
                            row.DefaultCellStyle.BackColor = Color.SeaGreen;
                            row.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                        }
                        //
                        BtnGuardarPago.Enabled = true;
                        BtnCancelarPago.Enabled = true;
                        BtnCalcular.Enabled = false;
                        RadioNinguno.Enabled = false;
                    }
                }
            }
            else if (RadioParcial.Checked == true)
            {
                if (TxtDinero.Text.Length < 1)
                {
                    MessageBox.Show($"Es necesario completar el campo 'Dinero'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    decimal importe = 0;
                    decimal dinero = Convert.ToDecimal(TxtDinero.Text);
                    //
                    if (SaldoDecimal != 0)
                    {
                        dinero = Convert.ToDecimal(TxtDinero.Text) + Convert.ToDecimal(SaldoDecimal);
                        //
                        var aux = LblSaldo.Text;
                        LblSaldo.Text = $"{aux}+";
                        LblSaldo.ForeColor = Color.SeaGreen;
                        //
                        TxtDinero.Text = $"${dinero:0.00}";
                    }
                    //
                    _dinero = dinero;
                    //
                    FillGrids();
                    // 
                    foreach (DataGridViewRow deuda in GridComprasDeudas.Rows)
                    {
                        int VentaCodigoVenta = Convert.ToInt32(deuda.Cells["Codigo"].Value);
                        //
                        if (dinero > Convert.ToDecimal(deuda.Cells["Importe"].Value))
                        {
                            dinero = dinero - Convert.ToDecimal(deuda.Cells["Importe"].Value);
                            importe = importe + Convert.ToDecimal(deuda.Cells["Importe"].Value);
                            //
                            deuda.DefaultCellStyle.BackColor = Color.SeaGreen;
                            deuda.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                            //
                            foreach (DataGridViewRow row in GridProductosDeudas.Rows)
                            {
                                int ProductoCodigoVenta = Convert.ToInt32(row.Cells["CodigoDeVenta"].Value);
                                //
                                if (VentaCodigoVenta == ProductoCodigoVenta)
                                {
                                    row.DefaultCellStyle.BackColor = Color.SeaGreen;
                                    row.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                                }
                            }
                        }
                    }
                    _importe = importe;
                    //
                    if (importe == 0)
                    {
                        DialogResult respuesta = MessageBox.Show($"El dinero del cliente no alcanza para saldar ningua de las deudas ¿Desea agregarlo al 'Saldo' de su cuenta?", "Añadir al saldo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta == DialogResult.Yes)
                        {
                            var aux = LblSaldo.Text;
                            LblSaldo.Text = aux.Remove(aux.Length - 1);
                            LblSaldo.ForeColor = SystemColors.ControlLightLight;
                            //
                            _dinero = dinero - Convert.ToDecimal(SaldoDecimal);
                            //
                            TxtDinero.Text = $"${_dinero:0.00}";
                            //
                            TxtVuelto.Text = $"$0";
                            //
                            var saveLoading = new SaveView(unitOfWork, _dinero, IdCuenta);
                            saveLoading.ShowDialog();
                            //
                            MessageBox.Show($"El Saldo de ${_dinero:0.00} ha sido añadido a la cuenta!", "Saldo Añadido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //

                            GetAccountData();
                            FillGrids();
                        }
                    }
                    _vuelto = dinero;
                    //
                    TxtImporte.Text = $"${importe:0.00}";
                    TxtVuelto.Text = $"${dinero:0.00}";
                    //
                    BtnGuardarPago.Enabled = true;
                    BtnCancelarPago.Enabled = true;
                    BtnCalcular.Enabled = false;
                    RadioNinguno.Enabled = false;
                }
            }
            else if (RadioEspecífico.Checked == true)
            {
                if (TxtDinero.Text.Length < 1)
                {
                    MessageBox.Show($"Es necesario completar el campo 'Dinero'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    decimal dinero = Convert.ToDecimal(TxtDinero.Text);
                    //
                    if (SaldoDecimal != 0)
                    {
                        dinero = Convert.ToDecimal(TxtDinero.Text) + Convert.ToDecimal(SaldoDecimal);
                        //
                        var aux = LblSaldo.Text;
                        LblSaldo.Text = $"{aux}+";
                        LblSaldo.ForeColor = Color.SeaGreen;
                        //
                        TxtDinero.Text = $"${dinero:0.00}";
                    }
                    //
                    decimal importe = _importe;
                    _dinero = dinero;
                    //
                    if (importe > dinero)
                    {
                        MessageBox.Show($"El dinero ingresado no es suficiente para saldar la deuda total", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        decimal vuelto = dinero - importe;
                        TxtVuelto.Text = $"${vuelto:0.00}";
                        //
                        _vuelto = vuelto;
                        //
                        FillGrids();
                        //
                        foreach (DataGridViewRow row in GridComprasDeudas.Rows)
                        {
                            var codigoVenta = Convert.ToInt32(row.Cells["Codigo"].Value);
                            if (CodigoVespecifico == codigoVenta)
                            {
                                row.DefaultCellStyle.BackColor = Color.SeaGreen;
                                row.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                            }
                        }
                        //
                        foreach (DataGridViewRow row in GridProductosDeudas.Rows)
                        {
                            var codigoVenta = Convert.ToInt32(row.Cells["CodigoDeVenta"].Value);
                            if (CodigoVespecifico == codigoVenta)
                            {
                                row.DefaultCellStyle.BackColor = Color.SeaGreen;
                                row.DefaultCellStyle.SelectionBackColor = Color.MediumSeaGreen;
                            }
                        }
                        //
                        BtnGuardarPago.Enabled = true;
                        BtnCancelarPago.Enabled = true;
                        BtnCalcular.Enabled = false;
                        RadioNinguno.Enabled = false;
                    }
                }
            }
        }

        private void BtnGuardarPago_Click(object sender, EventArgs e)
        {
            if (RadioTotal.Checked == true)
            {
                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea pagar la deuda de manera total?", "Pago Total", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    var saveLoading = new SaveView(unitOfWork, _importe, _dinero, _vuelto, IdCuenta);
                    saveLoading.ShowDialog();
                    //
                    MessageBox.Show($"La deuda ha sido pagada con éxito!", "Pagado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //
                    BtnImprimir.Enabled = true;
                    BtnTerminarPago.Enabled = true;
                    BtnCalcular.Enabled = false;
                    BtnGuardarPago.Enabled = false;
                    BtnCancelarPago.Enabled = false;
                    //
                    RadioParcial.Enabled = false;
                    RadioTotal.Enabled = false;
                    RadioSaldo.Enabled = false;
                    RadioEspecífico.Enabled = false;
                    RadioNinguno.Enabled = false;
                }
            }
            else if (RadioParcial.Checked == true)
            {
                bool _saldoVuelto = false;
                //
                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea pagar la deuda de manera parcial?", "Pago Parcial", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    if (_vuelto != 0)
                    {
                        DialogResult respuesta3 = MessageBox.Show($"¿Desea añadir el vuelto al saldo de la cuenta?", "Saldo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (respuesta3 == DialogResult.Yes)
                        {
                            _saldoVuelto = true;
                        }
                    }
                    //
                    var saveLoading = new SaveView(unitOfWork, _importe, _dinero, _vuelto, IdCuenta, _saldoVuelto);
                    saveLoading.ShowDialog();
                    //
                    MessageBox.Show($"La deuda ha sido pagada parcialmente con éxito!", "Pagado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //
                    BtnImprimir.Enabled = true;
                    BtnTerminarPago.Enabled = true;
                    BtnCalcular.Enabled = false;
                    BtnGuardarPago.Enabled = false;
                    BtnCancelarPago.Enabled = false;
                    //
                    RadioParcial.Enabled = false;
                    RadioTotal.Enabled = false;
                    RadioSaldo.Enabled = false;
                    RadioEspecífico.Enabled = false;
                    RadioNinguno.Enabled = false;
                }
            }
            else if (RadioSaldo.Checked == true)
            {
                if (TxtDinero.Text.Length < 1)
                {
                    MessageBox.Show($"Es necesario completar el campo 'Dinero'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _dinero = Convert.ToDecimal(TxtDinero.Text);
                    //
                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea añadir ${_dinero:0.00} al 'Saldo' de la cuenta?", "Añadir al saldo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        var saveLoading = new SaveView(unitOfWork, _dinero, IdCuenta);
                        saveLoading.ShowDialog();
                        //
                        MessageBox.Show($"El Saldo de ${_dinero:0.00} ha sido añadido a la cuenta!", "Saldo Añadido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //
                        BtnImprimir.Enabled = true;
                        BtnTerminarPago.Enabled = true;
                        BtnCalcular.Enabled = false;
                        BtnGuardarPago.Enabled = false;
                        BtnCancelarPago.Enabled = false;
                        //
                        RadioParcial.Enabled = false;
                        RadioTotal.Enabled = false;
                        RadioSaldo.Enabled = false;
                        RadioEspecífico.Enabled = false;
                        RadioNinguno.Enabled = false;
                    }
                }
            }
            else if (RadioEspecífico.Checked == true)
            {
                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea pagar la deuda seleccionada?", "Pago Específico", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    var saveLoading = new SaveView(unitOfWork, _importe, _dinero, _vuelto, IdCuenta, CodigoVespecifico, IdDeuda);
                    saveLoading.ShowDialog();
                    //
                    MessageBox.Show($"La deuda seleccionada ha sido pagada con éxito!", "Pagado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //
                    BtnImprimir.Enabled = true;
                    BtnTerminarPago.Enabled = true;
                    BtnCalcular.Enabled = false;
                    BtnGuardarPago.Enabled = false;
                    BtnCancelarPago.Enabled = false;
                    //
                    RadioParcial.Enabled = false;
                    RadioTotal.Enabled = false;
                    RadioSaldo.Enabled = false;
                    RadioEspecífico.Enabled = false;
                    RadioNinguno.Enabled = false;
                }
            }
            //
            GridComprasDeudas.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            GridComprasDeudas.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            //
            GridProductosDeudas.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            GridProductosDeudas.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        }

        private void RadioSaldo_CheckedChanged(object sender, EventArgs e)
        {
            BtnGuardarPago.Enabled = true;
            TxtDinero.ReadOnly = false;
        }

        private void RadioEspecífico_CheckedChanged(object sender, EventArgs e)
        {
            TxtDinero.ReadOnly = false;
            BtnCancelarPago.Enabled = true;
            BtnCalcular.Enabled = true;
        }

        private void BtnCancelarPago_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea cancelar el pago?", "Pago Total", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                ResetMetodosDePago();
                FillGrids();
                //
                foreach (DataGridViewRow row in GridComprasDeudas.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                }
                //
                foreach (DataGridViewRow row in GridProductosDeudas.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                }

                //GridComprasDeudas.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                //GridComprasDeudas.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                ////
                //GridProductosDeudas.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                //GridProductosDeudas.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                //
                if (SaldoDecimal > 0)
                {
                    var aux = LblSaldo.Text;
                    LblSaldo.Text = aux.Remove(aux.Length - 1);
                    LblSaldo.ForeColor = SystemColors.ControlLightLight;
                }
                //
                RadioNinguno.Enabled = true;
                RadioNinguno.Checked = true;

            }
        }

        private void ResetMetodosDePago()
        {
            BtnCalcular.Enabled = false;
            BtnGuardarPago.Enabled = false;
            BtnCancelarPago.Enabled = false;
            //
            TxtDinero.ReadOnly = true;
            //
            TxtDinero.Text = " ";
            TxtImporte.Text = " ";
            TxtVuelto.Text = " ";
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex == 0)
            {
                RadioEspecífico.Enabled = false;
                RadioTotal.Enabled = false;
                RadioParcial.Enabled = false;
                //
                FilterDC = false;
                //
                AplicarFiltrosDeudas.BackColor = Color.FromArgb(255, 45, 45);
                AplicarFiltrosDeudas.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 40, 40);
                AplicarFiltrosDeudas.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 85, 85);
                //
                AplicarFiltrosDeudas.IconChar = FontAwesome.Sharp.IconChar.FilterCircleXmark;
                //
                FilterDP = false;
                //
                AplicarFiltroProductosDeuda.BackColor = Color.FromArgb(255, 45, 45);
                AplicarFiltroProductosDeuda.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 40, 40);
                AplicarFiltroProductosDeuda.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 85, 85);
                //
                AplicarFiltroProductosDeuda.IconChar = FontAwesome.Sharp.IconChar.FilterCircleXmark;
            }
            else if (TabControl.SelectedIndex == 2)
            {
                RadioEspecífico.Enabled = true;
                RadioTotal.Enabled = true;
                RadioParcial.Enabled = true;
                //
                Cfilter = false;
                //
                ComprasFiltroHab.BackColor = Color.FromArgb(255, 45, 45);
                ComprasFiltroHab.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 40, 40);
                ComprasFiltroHab.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 85, 85);
                //
                ComprasFiltroHab.IconChar = FontAwesome.Sharp.IconChar.FilterCircleXmark;
                //
                Pfilter = false;
                //
                ProductosFiltroHab.BackColor = Color.FromArgb(255, 45, 45);
                ProductosFiltroHab.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 40, 40);
                ProductosFiltroHab.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 85, 85);
                //
                ProductosFiltroHab.IconChar = FontAwesome.Sharp.IconChar.FilterCircleXmark;
            }
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

        }

        private void BtnRefrescarVentas_Click(object sender, EventArgs e)
        {
            if (Cfilter == true)
            {
                TxtCompras.Text = " ";
                GridCompras.DataSource = ClasesCompartidas.ComprasList;
            }
            //
            if (Pfilter == true)
            {
                TxtProductos.Text = " ";
                GridProductos.DataSource = ClasesCompartidas.ProductosList;
            }
            //
            if (FilterDC == true)
            {
                DeudaTxtCompras.Text = " ";
                GridComprasDeudas.DataSource = ClasesCompartidas.ComprasDeudaList;
            }
            //
            if (FilterDP == true)
            {
                DeudaTxtProductos.Text = " ";
                GridProductosDeudas.DataSource = ClasesCompartidas.ProductosDeudaList;
            }
            FillCombos();
            //
        }

        private void AplicarFiltroProductosDeuda_Click(object sender, EventArgs e)
        {
            if (FilterDP == false)
            {
                FilterDP = true;
                //
                AplicarFiltroProductosDeuda.BackColor = Color.FromArgb(0, 230, 87);
                AplicarFiltroProductosDeuda.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 196, 74);
                AplicarFiltroProductosDeuda.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 246, 93);
                //
                AplicarFiltroProductosDeuda.IconChar = FontAwesome.Sharp.IconChar.Filter;
            }
            else
            {
                FilterDP = false;
                //
                AplicarFiltroProductosDeuda.BackColor = Color.FromArgb(255, 45, 45);
                AplicarFiltroProductosDeuda.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 40, 40);
                AplicarFiltroProductosDeuda.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 85, 85);
                //
                AplicarFiltroProductosDeuda.IconChar = FontAwesome.Sharp.IconChar.FilterCircleXmark;
            }
            //
            if (FilterDP == true)
            {
                CheckFecha.Enabled = true;
                CheckUsuarios.Enabled = true;
                //
                CheckCboTipo.Enabled = true;
                CheckCboMarca.Enabled = true;
                CheckCboSPEC.Enabled = true;
                CheckCboProveedor.Enabled = true;
            }
            //
            if (FilterDP == true || FilterDC == true)
            {
                BtnFiltrarVentas.Enabled = true;
            }
            else if (FilterDP == false && FilterDC == false)
            {
                BtnFiltrarVentas.Enabled = false;
                //
                CheckFecha.Enabled = false;
                CheckUsuarios.Enabled = false;
                //
                CheckCboTipo.Enabled = false;
                CheckCboMarca.Enabled = false;
                CheckCboSPEC.Enabled = false;
                CheckCboProveedor.Enabled = false;
                //
                BtnFiltrarVentas.Enabled = true;
                //
                CheckFecha.Enabled = true;
                CheckUsuarios.Enabled = true;
                //
                CheckCboTipo.Enabled = true;
                CheckCboMarca.Enabled = true;
                CheckCboSPEC.Enabled = true;
                CheckCboProveedor.Enabled = true;
            }
        }

        private void AplicarFiltrosDeudas_Click(object sender, EventArgs e)
        {
            if (FilterDC == false)
            {
                FilterDC = true;
                //
                AplicarFiltrosDeudas.BackColor = Color.FromArgb(0, 230, 87);
                AplicarFiltrosDeudas.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 196, 74);
                AplicarFiltrosDeudas.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 246, 93);
                //
                AplicarFiltrosDeudas.IconChar = FontAwesome.Sharp.IconChar.Filter;
            }
            else
            {
                FilterDC = false;
                //
                AplicarFiltrosDeudas.BackColor = Color.FromArgb(255, 45, 45);
                AplicarFiltrosDeudas.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 40, 40);
                AplicarFiltrosDeudas.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 85, 85);
                //
                AplicarFiltrosDeudas.IconChar = FontAwesome.Sharp.IconChar.FilterCircleXmark;
            }
            //
            if (FilterDC == true)
            {
                CheckFecha.Enabled = true;
                CheckUsuarios.Enabled = true;
            }
            //
            if (FilterDC == true || FilterDP == true)
            {
                BtnFiltrarVentas.Enabled = true;
            }
            else if (FilterDC == false && FilterDP == false)
            {
                BtnFiltrarVentas.Enabled = false;
                //
                CheckFecha.Enabled = false;
                CheckUsuarios.Enabled = false;
                //
                CheckCboTipo.Enabled = false;
                CheckCboMarca.Enabled = false;
                CheckCboSPEC.Enabled = false;
                CheckCboProveedor.Enabled = false;
                //
                BtnFiltrarVentas.Enabled = true;
                //
                CheckFecha.Enabled = true;
                CheckUsuarios.Enabled = true;
                //
                CheckCboTipo.Enabled = true;
                CheckCboMarca.Enabled = true;
                CheckCboSPEC.Enabled = true;
                CheckCboProveedor.Enabled = true;
            }
        }

        private async void BtnFiltrar_Click(object sender, EventArgs e)
        {
            // Elementos por los cuales filtrar            
            // Usuarios
            if (CheckUsuarios.Checked == true)
            {
                FilterUsuariosVenta = (int)ComboUsuarios.SelectedValue;
            }
            // Fecha
            if (CheckFecha.Checked == true)
            {
                FilterFechaVenta = 1;
                //
                FechaDesdeSend = Convert.ToDateTime(FechaDesde.Text);
                FechaHastaSend = Convert.ToDateTime(FechaHasta.Text);
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
            var miniLoading = new MiniLoading(FilterUsuariosVenta, FilterFechaVenta, FilterTipoId, FilterMarcaId, FilterSpecId, FilterProveedorId, FilterDC, FilterDP, IdCuenta, FechaDesdeSend, FechaHastaSend, Cfilter, Pfilter, unitOfWork);
            miniLoading.ShowDialog();
            //
            if (FilterDC == true)
            {
                GridComprasDeudas.DataSource = ClasesCompartidas.ComprasDeudasListFilter;
            }
            //
            if (FilterDP == true)
            {
                GridProductosDeudas.DataSource = ClasesCompartidas.ProductosDeudasListFilter;
            }
            //
            if (Cfilter == true)
            {
                GridProductos.DataSource = ClasesCompartidas.ComprasListFilter;
            }
            //
            if (Pfilter == true)
            {
                GridCompras.DataSource = ClasesCompartidas.ProductosList;
            }
        }

        private async void DeudaSearchCompras_Click(object sender, EventArgs e)
        {
            var txtBusqueda = DeudaTxtCompras.Text;
            //
            var Grid = "Deudas";
            //
            //var miniLoading = new MiniLoading(_txtBusqueda, Grid, unitOfWork);
            //miniLoading.ShowDialog();
            ClasesCompartidas.ComprasDeudasListFilter.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Deuda" && q.CuentaId.Equals(IdCuenta) && q.Usuario.Nombre.Contains(txtBusqueda));
            //
            GridComprasDeudas.DataSource = ClasesCompartidas.ComprasDeudasListFilter;
        }

        private async void BtnSearchCompras_Click(object sender, EventArgs e)
        {
            var txtBusqueda = TxtCompras.Text;
            //
            var Grid = "Compras";
            //
            //var miniLoading = new MiniLoading(_txtBusqueda, Grid, unitOfWork);
            //miniLoading.ShowDialog();
            //
            ClasesCompartidas.ComprasListFilter.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado" && q.CuentaId.Equals(IdCuenta) && q.Usuario.Nombre.Contains(txtBusqueda));
            //
            GridCompras.DataSource = ClasesCompartidas.ComprasListFilter;
        }

        private async void DeudaSearchProductos_Click(object sender, EventArgs e)
        {
            var txtBusqueda = DeudaTxtProductos.Text;
            //
            var Grid = "ProductosDeudas";
            //
            //var miniLoading = new MiniLoading(_txtBusqueda, Grid, unitOfWork);
            //miniLoading.ShowDialog();
            ClasesCompartidas.ProductosDeudasListFilter.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.TipoProducto.Nombre.Contains(txtBusqueda) || q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.Marca.Nombre.Contains(txtBusqueda) || q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.SPEC.Nombre.Contains(txtBusqueda) || q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.Proveedor.Nombre.Contains(txtBusqueda) || q.Pagado == "No" && q.CuentaId.Equals(IdCuenta) && q.Usuario.Nombre.Contains(txtBusqueda));
            //
            GridProductosDeudas.DataSource = ClasesCompartidas.ProductosDeudasListFilter;
        }

        private async void BtnSearchProductos_Click(object sender, EventArgs e)
        {
            var _txtBusqueda = TxtProductos.Text;
            //
            var Grid = "ProductosPagados";
            //
            //var miniLoading = new MiniLoading(_txtBusqueda, Grid, unitOfWork);
            //miniLoading.ShowDialog();
            //
            ClasesCompartidas.ProductosListFilter.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.TipoProducto.Nombre.Contains(_txtBusqueda) || q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.Marca.Nombre.Contains(_txtBusqueda) || q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.SPEC.Nombre.Contains(_txtBusqueda) || q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.Proveedor.Nombre.Contains(_txtBusqueda) || q.Pagado == "Si" && q.CuentaId.Equals(IdCuenta) && q.Usuario.Nombre.Contains(_txtBusqueda));
            //
            GridProductos.DataSource = ClasesCompartidas.ProductosListFilter;
        }

        private void ComprasFiltroHab_Click(object sender, EventArgs e)
        {
            if (Cfilter == true)
            {
                FilterDC = true;
                //
                ComprasFiltroHab.BackColor = Color.FromArgb(0, 230, 87);
                ComprasFiltroHab.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 196, 74);
                ComprasFiltroHab.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 246, 93);
                //
                ComprasFiltroHab.IconChar = FontAwesome.Sharp.IconChar.Filter;
            }
            else
            {
                Cfilter = false;
                //
                ComprasFiltroHab.BackColor = Color.FromArgb(255, 45, 45);
                ComprasFiltroHab.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 40, 40);
                ComprasFiltroHab.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 85, 85);
                //
                ComprasFiltroHab.IconChar = FontAwesome.Sharp.IconChar.FilterCircleXmark;
            }
            //
            if (Cfilter == true)
            {
                CheckFecha.Enabled = true;
                CheckUsuarios.Enabled = true;
            }
            //
            if (Cfilter == true || Pfilter == true)
            {
                BtnFiltrarVentas.Enabled = true;
            }
            else if (Cfilter == false && Pfilter == false)
            {
                BtnFiltrarVentas.Enabled = false;
                //
                CheckFecha.Enabled = false;
                CheckUsuarios.Enabled = false;
                //
                CheckCboTipo.Enabled = false;
                CheckCboMarca.Enabled = false;
                CheckCboSPEC.Enabled = false;
                CheckCboProveedor.Enabled = false;
                //
                BtnFiltrarVentas.Enabled = true;
                //
                CheckFecha.Enabled = true;
                CheckUsuarios.Enabled = true;
                //
                CheckCboTipo.Enabled = true;
                CheckCboMarca.Enabled = true;
                CheckCboSPEC.Enabled = true;
                CheckCboProveedor.Enabled = true;
            }

        }

        private void ProductosFiltroHab_Click(object sender, EventArgs e)
        {
            if (Pfilter == false)
            {
                Pfilter = true;
                //
                ProductosFiltroHab.BackColor = Color.FromArgb(0, 230, 87);
                ProductosFiltroHab.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 196, 74);
                ProductosFiltroHab.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 246, 93);
                //
                ProductosFiltroHab.IconChar = FontAwesome.Sharp.IconChar.Filter;
            }
            else
            {
                Pfilter = false;
                //
                ProductosFiltroHab.BackColor = Color.FromArgb(255, 45, 45);
                ProductosFiltroHab.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 40, 40);
                ProductosFiltroHab.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 85, 85);
                //
                ProductosFiltroHab.IconChar = FontAwesome.Sharp.IconChar.FilterCircleXmark;
            }
            //
            if (Pfilter == true)
            {
                CheckFecha.Enabled = true;
                CheckUsuarios.Enabled = true;
                //
                CheckCboTipo.Enabled = true;
                CheckCboMarca.Enabled = true;
                CheckCboSPEC.Enabled = true;
                CheckCboProveedor.Enabled = true;
            }
            //
            if (Pfilter == true || Cfilter == true)
            {
                BtnFiltrarVentas.Enabled = true;
            }
            else if (FilterDP == false && FilterDC == false)
            {
                BtnFiltrarVentas.Enabled = false;
                //
                CheckFecha.Enabled = false;
                CheckUsuarios.Enabled = false;
                //
                CheckCboTipo.Enabled = false;
                CheckCboMarca.Enabled = false;
                CheckCboSPEC.Enabled = false;
                CheckCboProveedor.Enabled = false;
                //
                BtnFiltrarVentas.Enabled = true;
                //
                CheckFecha.Enabled = true;
                CheckUsuarios.Enabled = true;
                //
                CheckCboTipo.Enabled = true;
                CheckCboMarca.Enabled = true;
                CheckCboSPEC.Enabled = true;
                CheckCboProveedor.Enabled = true;
            }
        }

        private void TabPagePagadas_Click(object sender, EventArgs e)
        {

        }

        private void GridPagos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn columna in GridPagos.Columns)
            {
                // Visibilidad de columnas
                if (columna.Name == "CuentaId")
                    columna.Visible = false;
                if (columna.Name == "UsuarioId")
                    columna.Visible = false;
                // Ajustes de Columnas
                if (columna.Name == "Importe")
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                if (columna.Name == "Dinero")
                    columna.DefaultCellStyle.Format = "$" + "0.00";
                if (columna.Name == "Vuelto")
                    columna.DefaultCellStyle.Format = "$" + "0.00";
            }
        }

        private void GridCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var _codigoCompra = (int)GridCompras.CurrentRow.Cells["Codigo"].Value;
            //
            var miniLoading = new MiniLoading(_codigoCompra, unitOfWork);
            miniLoading.ShowDialog();
            //
            GridProductos.DataSource = ClasesCompartidas.CompraSelected;
        }

        private async void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (ClasesCompartidas.SaldoId != null)
            {
                comprobanteSaldo.DataSource = await unitOfWork.PagoRepository.GetAllAsync(include: q => q.Include(q => q.Cuenta).Include(q => q.Usuario), filter: q => q.Id.Equals(ClasesCompartidas.SaldoId));
                //
                SaldoViewReport saldoViewReport = new SaldoViewReport(comprobanteSaldo);
                saldoViewReport.ShowDialog();
            }
            else
            {
                comprobantePago.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta).Include(q => q.Venta).Include(q => q.Venta.Pago).Include(q => q.Venta.Pago.Usuario).Include(q => q.Venta.Cuenta), filter: q => q.Pagado == "Si" && q.Venta.PagoId.Equals(ClasesCompartidas.PagoId));
                //
                ComprobantePagoViewReport comprobantePagoViewReport = new ComprobantePagoViewReport(comprobantePago);
                comprobantePagoViewReport.ShowDialog();
            }
            //
        }

        private void AccountsView_Load(object sender, EventArgs e)
        {

        }

        private void BtnTerminarPago_Click(object sender, EventArgs e)
        {
            FillGrids();
            GetAccountData();
            //
            LblSaldo.ForeColor = SystemColors.ControlLightLight;
            //
            BtnImprimir.Enabled = false;
            BtnTerminarPago.Enabled = false;
            //
            RadioParcial.Enabled = true;
            RadioTotal.Enabled = true;
            RadioSaldo.Enabled = true;
            RadioEspecífico.Enabled = true;
            //
            TxtDinero.ReadOnly = true;
            //
            TxtDinero.Text = " ";
            TxtImporte.Text = " ";
            TxtVuelto.Text = " ";
            //
            foreach (DataGridViewRow row in GridComprasDeudas.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            }
            //
            foreach (DataGridViewRow row in GridProductosDeudas.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            }
            //
            if (SaldoDecimal > 0)
            {
                var aux = LblSaldo.Text;
                LblSaldo.Text = aux.Remove(aux.Length - 1);
                LblSaldo.ForeColor = SystemColors.ControlLightLight;
            }
            //
            RadioNinguno.Enabled = true;
            RadioNinguno.Checked = true;
            //
            ClasesCompartidas.PagoId = null;
            ClasesCompartidas.SaldoId = null;
        }

        private void RadioNinguno_CheckedChanged(object sender, EventArgs e)
        {
            BtnCalcular.Enabled = false;
            BtnCancelarPago.Enabled = false;
            BtnGuardarPago.Enabled = false;
            TxtDinero.ReadOnly = true;
            //
            TxtImporte.Text = " ";
            TxtDinero.Text = " ";
            TxtVuelto.Text = " ";
        }
    }
}
