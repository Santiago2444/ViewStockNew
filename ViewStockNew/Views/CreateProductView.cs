using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
    public partial class CreateProductView : Form
    {
        IUnitOfWork unitOfWork;
        // Variables Auxiliares
        bool editando;
        private int idProductModify;
        private int auxModify = 0;
        // Variables para mantener ciertos datos de un producto editado
        private int? _TipoId;
        private int? _MarcaId;
        private int? _SpecId;
        private string? _Detalles;
        // Variables dedicadas a las valores númericos
        private decimal? _PrecioBulto;
        private decimal? _Porcentaje;
        private decimal? _PrecioUnidad;
        private int? _Cantidad;
        private int? _Stock;
        private byte[]? NotImage;
        private decimal Ganancia;
        private decimal _Descuento;
        private decimal Descuento;
        private bool? remito = null;

        public CreateProductView(IUnitOfWork unitOfWork, bool Editando)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.editando = Editando;
            //
            CargarComboTipo();
            CargarComboMarca();
            CargarComboProveedor();
            CargarComboSPEC();
            BtnEliminarImagen.Enabled = false;
            //
            var ganancia = unitOfWork.PorcentajeGananciaRepository.GetByID(1);
            Ganancia = ganancia.Porcentaje;
            TxtPorcetaje.Text = ganancia.Porcentaje.ToString("0.00");
            //
            var descuento = unitOfWork.PorcentajeGananciaRepository.GetByID(2);
            Descuento = descuento.Porcentaje;
            TxtDescuento.Text = descuento.Porcentaje.ToString("0.00");
        }

        private void CargarComboTipo(int? tipoProductoId = 0)
        {
            ComboTipo.DisplayMember = "Nombre";
            ComboTipo.ValueMember = "Id";
            ComboTipo.DataSource = ClasesCompartidas.tiposProductosList;
            //
            if (tipoProductoId != 0)
                ComboTipo.SelectedValue = tipoProductoId;
            else
                ComboTipo.SelectedValue = 0;
            //
            if (ClasesCompartidas.TipoNuevo != null)
            {
                int index = (ComboTipo.Items.Count);
                ComboTipo.DataSource = ClasesCompartidas.tiposProductosList;
                ComboTipo.DisplayMember = "Nombre";
                ComboTipo.SelectedIndex = index - 1;
            }
        }

        private void CargarComboMarca(int? marcaId = 0)
        {

            ComboMarca.DisplayMember = "Nombre";
            ComboMarca.ValueMember = "Id";
            ComboMarca.DataSource = ClasesCompartidas.marcasList;
            //
            if (marcaId != 0)
                ComboMarca.SelectedValue = marcaId;
            else
                ComboMarca.SelectedValue = 0;
            //
            if (ClasesCompartidas.MarcaNueva != null)
            {
                int index = (ComboMarca.Items.Count);
                ComboMarca.DataSource = ClasesCompartidas.marcasList;
                ComboMarca.DisplayMember = "Nombre";
                ComboMarca.SelectedIndex = index - 1;
            }
        }

        private void CargarComboProveedor(int? proveedorId = 0)
        {

            ComboProveedor.DisplayMember = "Nombre";
            ComboProveedor.ValueMember = "Id";
            ComboProveedor.DataSource = ClasesCompartidas.proveedoresList;
            //
            if (proveedorId != 0)
                ComboProveedor.SelectedValue = proveedorId;
            else
                ComboProveedor.SelectedValue = 0;
            //
            if (ClasesCompartidas.ProveedorNuevo != null)
            {
                int index = (ComboProveedor.Items.Count);
                ComboProveedor.DataSource = ClasesCompartidas.specsList;
                ComboProveedor.DisplayMember = "Nombre";
                ComboProveedor.SelectedIndex = index - 1;
            }

        }

        private void CargarComboSPEC(int? specId = 0)
        {

            ComboSPEC.DisplayMember = "Nombre";
            ComboSPEC.ValueMember = "Id";
            ComboSPEC.DataSource = ClasesCompartidas.specsList;
            //
            if (specId != 0)
                ComboSPEC.SelectedValue = specId;
            else
                ComboSPEC.SelectedValue = 0;
            //
            if (ClasesCompartidas.SpecNuevo != null)
            {
                int index = (ComboSPEC.Items.Count);
                ComboSPEC.DataSource = ClasesCompartidas.specsList;
                ComboSPEC.DisplayMember = "Nombre";
                ComboSPEC.SelectedIndex = index - 1;
            }
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Invoco la imagen default el "Sin Imagen" para compararla con la que tiene el PictureBox
            var notImage = new Bitmap(ViewStockNew.Properties.Resources.SinImagen2);
            MemoryStream ms = new MemoryStream();
            notImage.Save(ms, ImageFormat.Jpeg);
            byte[]? NotImage = ms.ToArray();
            //
            // Se almacena la imagen del PictureBox
            //
            MemoryStream ds = new MemoryStream();
            PctImagen.Image.Save(ds, ImageFormat.Jpeg);
            byte[]? aByte = ds.ToArray();
            //
            // Se comparan los dos archivos byte[]
            //
            if (aByte == NotImage)
            {
                aByte = null;
            }
            //
            if (ComboTipo.SelectedValue == null)
            {
                MessageBox.Show($"Es necesario asignarle un Tipo al producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ComboMarca.SelectedValue == null)
            {
                MessageBox.Show($"Es necesario asignarle una Marca al producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtPrecioVenta.Text.Length < 1)
            {
                MessageBox.Show($"Es necesario determinar un Precio para el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Si el usuario no ingresa valores para ciertos elementos, a estos se le asigna 0
                if (TxtPrecioBulto.Text.Length < 1 || TxtCantidad.Text.Length < 1 || TxtPrecioUnidad.Text.Length < 1 || TxtPorcetaje.Text.Length < 1 || TxtStock.Text.Length < 1 || TxtDescuento.Text.Length < 1)
                {
                    if (editando)
                    {
                        // En la siguientes líneas, se desea encontrar cual de estos valores se encuentra vacío
                        #region PrecioBulto
                        if (TxtPrecioBulto.Text.Length < 1)
                        {
                            _PrecioBulto = 0;
                        }
                        else
                        {
                            _PrecioBulto = Convert.ToDecimal(TxtPrecioBulto.Text);
                        }

                        #endregion
                        //
                        #region Cantidad
                        if (TxtCantidad.Text.Length < 1)
                        {
                            _Cantidad = 0;
                        }
                        else
                        {
                            _Cantidad = Convert.ToInt32(TxtCantidad.Text);
                        }
                        #endregion
                        //
                        #region PrecioUnidad
                        if (TxtPrecioUnidad.Text.Length < 1)
                        {
                            _PrecioUnidad = 0;
                        }
                        else
                        {
                            _PrecioUnidad = Convert.ToDecimal(TxtPrecioUnidad.Text);
                        }

                        #endregion
                        //
                        #region Porcentaje
                        if (TxtPorcetaje.Text.Length < 1)
                        {
                            _Porcentaje = 0;
                        }
                        else
                        {
                            _Porcentaje = Convert.ToDecimal(TxtPorcetaje.Text);
                        }
                        #endregion
                        //
                        #region Stock
                        if (TxtStock.Text.Length < 1)
                        {
                            _Stock = 0;
                        }
                        else
                        {
                            _Stock = Convert.ToInt32(TxtStock.Text);
                        }
                        #endregion
                        //
                        #region Descuento
                        if (TxtDescuento.Text.Length < 1)
                        {
                            _Descuento = 0;
                        }
                        else
                        {
                            _Descuento = Convert.ToDecimal(TxtDescuento.Text);
                        }
                        #endregion
                        //
                        Producto producto = new Producto()
                        {
                            // Al modificar un producto, se le debe mantener su id
                            Id = idProductModify,
                            TipoProductoId = (int)ComboTipo.SelectedValue,
                            MarcaId = (int)ComboMarca.SelectedValue,
                            SPECId = (int)ComboSPEC.SelectedValue,
                            ProveedorId = (int?)ComboProveedor.SelectedValue,
                            Detalles = TxtDetalles.Text,
                            PrecioBulto = (decimal)_PrecioBulto,
                            CantidadBulto = (int)_Cantidad,
                            PrecioUnidad = (decimal)_PrecioUnidad,
                            Ganancia = (int)_Porcentaje,
                            Descuento = (decimal)_Descuento,
                            PVP = Convert.ToDecimal(TxtPrecioVenta.Text),
                            Stock = (int)_Stock,
                            Modificacion = DateTime.Now,
                            Visible = true,
                            UsuarioId = ClasesCompartidas.UserId,
                            Imagen = aByte
                        };
                        //
                        // Se comprueba si las características esenciales del producto han sido alteradas
                        //
                        if (_TipoId == producto.TipoProductoId && _MarcaId == producto.MarcaId && _SpecId == producto.SPECId & _Detalles == producto.Detalles)
                        {
                            try
                            {
                                new ModelsValidator().Validate(producto);

                                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar los cambios realizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    unitOfWork.ProductoRepository.Update(producto);
                                    unitOfWork.Save();
                                    auxModify = 0;
                                    //
                                    // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                    // base de datos
                                    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                                    MessageBox.Show($"¡El producto se ha modificado con éxito! (1)", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Close();
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
                            //
                            // La línea siguiente comprueba si el producto que se está tratando de agregar ya existe entre los datos cargados
                            bool productoExistente = unitOfWork.ProductoRepository.dbSet.Any(x => x.TipoProductoId.Equals(producto.TipoProductoId) && x.MarcaId.Equals(producto.MarcaId) && x.SPECId.Equals(producto.SPECId) && x.Detalles.Equals(producto.Detalles));
                            //
                            if (!productoExistente)
                            {
                                try
                                {
                                    new ModelsValidator().Validate(producto);

                                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar los cambios realizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta == DialogResult.Yes)
                                    {
                                        unitOfWork.ProductoRepository.Update(producto);
                                        unitOfWork.Save();
                                        auxModify = 0;
                                        //
                                        // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                        // base de datos
                                        ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                                        MessageBox.Show($"¡El producto se ha modificado con éxito!(2)", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Close();
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
                                MessageBox.Show($"El producto que está intentando modificar posee características idénticas a un producto ya existente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        #region PrecioBulto
                        if (TxtPrecioBulto.Text.Length < 1)
                        {
                            _PrecioBulto = 0;
                        }
                        else
                        {
                            _PrecioBulto = Convert.ToDecimal(TxtPrecioBulto.Text);
                        }

                        #endregion
                        //
                        #region Cantidad
                        if (TxtCantidad.Text.Length < 1)
                        {
                            _Cantidad = 0;
                        }
                        else
                        {
                            _Cantidad = Convert.ToInt32(TxtCantidad.Text);
                        }
                        #endregion
                        //
                        #region PrecioUnidad
                        if (TxtPrecioUnidad.Text.Length < 1)
                        {
                            _PrecioUnidad = 0;
                        }
                        else
                        {
                            _PrecioUnidad = Convert.ToDecimal(TxtPrecioUnidad.Text);
                        }

                        #endregion
                        //
                        #region Porcentaje
                        if (TxtPorcetaje.Text.Length < 1)
                        {
                            _Porcentaje = 0;
                        }
                        else
                        {
                            _Porcentaje = Convert.ToDecimal(TxtPorcetaje.Text);
                        }
                        #endregion
                        //
                        #region Stock
                        if (TxtStock.Text.Length < 1)
                        {
                            _Stock = 0;
                        }
                        else
                        {
                            _Stock = Convert.ToInt32(TxtStock.Text);
                        }
                        #endregion
                        //
                        #region Descuento
                        if (TxtDescuento.Text.Length < 1)
                        {
                            _Descuento = 0;
                        }
                        else
                        {
                            _Descuento = Convert.ToDecimal(TxtDescuento.Text);
                        }
                        #endregion
                        //
                        Producto producto = new Producto()
                        {
                            // Producto Nuevo, el id es autoincremental 
                            TipoProductoId = (int)ComboTipo.SelectedValue,
                            MarcaId = (int)ComboMarca.SelectedValue,
                            SPECId = (int)ComboSPEC.SelectedValue,
                            ProveedorId = (int?)ComboProveedor.SelectedValue,
                            Detalles = TxtDetalles.Text.Length < 1 ? "null" : TxtDetalles.Text,
                            PrecioBulto = (decimal)_PrecioBulto,
                            CantidadBulto = (int)_Cantidad,
                            PrecioUnidad = (decimal)_PrecioUnidad,
                            Ganancia = (decimal)_Porcentaje,
                            Descuento = (decimal)_Descuento,
                            PVP = Convert.ToDecimal(TxtPrecioVenta.Text),
                            Stock = (int)_Stock,
                            Modificacion = DateTime.Now,
                            Visible = true,
                            UsuarioId = ClasesCompartidas.UserId,
                            Imagen = aByte
                        };
                        //
                        // La línea siguiente comprueba si el producto que se está tratando de agregar ya existe entre los datos cargados
                        bool productoExistente = unitOfWork.ProductoRepository.dbSet.Any(x => x.TipoProductoId.Equals(producto.TipoProductoId) && x.MarcaId.Equals(producto.MarcaId) && x.SPECId.Equals(producto.SPECId) && x.Detalles.Equals(producto.Detalles));
                        //
                        if (!productoExistente)
                        {
                            try
                            {
                                new ModelsValidator().Validate(producto);

                                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar este nuevo producto?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    ClasesCompartidas.ProductoId = producto.Id;
                                    unitOfWork.ProductoRepository.Add(producto);
                                    unitOfWork.Save();
                                    auxModify = 0;
                                    // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                    // base de datos
                                    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                                    MessageBox.Show($"¡El producto se ha creado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //
                                    if (ClasesCompartidas.ProductoId != null)
                                        Close();
                                    else
                                        LimpiarCasillas();
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
                            MessageBox.Show($"El producto que está intentando agregar ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    if (editando)
                    {
                        Producto producto = new Producto()
                        {
                            // Al modificar un producto, se le debe mantener su id
                            Id = idProductModify,
                            TipoProductoId = (int)ComboTipo.SelectedValue,
                            MarcaId = (int)ComboMarca.SelectedValue,
                            SPECId = (int)ComboSPEC.SelectedValue,
                            ProveedorId = (int?)ComboProveedor.SelectedValue,
                            Detalles = TxtDetalles.Text,
                            PrecioBulto = Convert.ToDecimal(TxtPrecioBulto.Text),
                            CantidadBulto = Convert.ToInt32(TxtCantidad.Text),
                            PrecioUnidad = Convert.ToDecimal(TxtPrecioUnidad.Text),
                            Ganancia = Convert.ToDecimal(TxtPorcetaje.Text),
                            Descuento = Convert.ToDecimal(TxtDescuento.Text),
                            PVP = Convert.ToDecimal(TxtPrecioVenta.Text),
                            Stock = Convert.ToInt32(TxtStock.Text),
                            Modificacion = DateTime.Now,
                            Visible = true,
                            UsuarioId = ClasesCompartidas.UserId,
                            Imagen = aByte


                        };
                        //
                        // Se comprueba si las características esenciales del producto han sido alteradas
                        //
                        if (_TipoId == producto.TipoProductoId && _MarcaId == producto.MarcaId && _SpecId == producto.SPECId & _Detalles == producto.Detalles)
                        {
                            try
                            {
                                new ModelsValidator().Validate(producto);

                                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar los cambios realizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    unitOfWork.ProductoRepository.Update(producto);
                                    unitOfWork.Save();
                                    auxModify = 0;
                                    // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                    // base de datos
                                    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                                    MessageBox.Show($"¡El producto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LimpiarCasillas();
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
                            //
                            // La línea siguiente comprueba si el producto que se está tratando de agregar ya existe entre los datos cargados
                            bool productoExistente = unitOfWork.ProductoRepository.dbSet.Any(x => x.TipoProductoId.Equals(producto.TipoProductoId) && x.MarcaId.Equals(producto.MarcaId) && x.SPECId.Equals(producto.SPECId) && x.Detalles.Equals(producto.Detalles));
                            //
                            if (!productoExistente)
                            {
                                try
                                {
                                    new ModelsValidator().Validate(producto);

                                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar los cambios realizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (respuesta == DialogResult.Yes)
                                    {
                                        unitOfWork.ProductoRepository.Update(producto);
                                        unitOfWork.Save();
                                        auxModify = 0;
                                        // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                        // base de datos
                                        ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                                        MessageBox.Show($"¡El producto se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Close();
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
                                MessageBox.Show($"El producto que está intentando modificar posee características idénticas a un producto ya existente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        Producto producto = new Producto()
                        {
                            // Producto Nuevo, el id es autoincremental 
                            TipoProductoId = (int)ComboTipo.SelectedValue,
                            MarcaId = (int)ComboMarca.SelectedValue,
                            SPECId = (int)ComboSPEC.SelectedValue,
                            ProveedorId = (int?)ComboProveedor.SelectedValue,
                            Detalles = TxtDetalles.Text.Length < 1 ? "null" : TxtDetalles.Text,
                            PrecioBulto = Convert.ToDecimal(TxtPrecioBulto.Text),
                            CantidadBulto = Convert.ToInt32(TxtCantidad.Text),
                            PrecioUnidad = Convert.ToDecimal(TxtPrecioUnidad.Text),
                            Ganancia = Convert.ToDecimal(TxtPorcetaje.Text),
                            Descuento = Convert.ToDecimal(TxtDescuento.Text),
                            PVP = Convert.ToDecimal(TxtPrecioVenta.Text),
                            Stock = Convert.ToInt32(TxtStock.Text),
                            Modificacion = DateTime.Now,
                            Visible = true,
                            UsuarioId = ClasesCompartidas.UserId,
                            Imagen = aByte
                        };
                        //
                        // La línea siguiente comprueba si el producto que se está tratando de agregar ya existe entre los datos cargados
                        bool productoExistente = unitOfWork.ProductoRepository.dbSet.Any(x => x.TipoProductoId.Equals(producto.TipoProductoId) && x.MarcaId.Equals(producto.MarcaId) && x.SPECId.Equals(producto.SPECId) && x.Detalles.Equals(producto.Detalles));
                        //
                        if (!productoExistente)
                        {
                            try
                            {
                                new ModelsValidator().Validate(producto);

                                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar este nuevo producto?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    unitOfWork.ProductoRepository.Add(producto);
                                    unitOfWork.Save();
                                    ClasesCompartidas.ProductoId = remito != null ? producto.Id : null;
                                    auxModify = 0;
                                    //
                                    // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                    // base de datos
                                    ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));
                                    MessageBox.Show($"¡El producto se ha creado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //
                                    if (ClasesCompartidas.ProductoId != null)
                                        Close();
                                    else
                                        LimpiarCasillas();

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
                            MessageBox.Show($"El producto que está intentando agregar ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
        }

        private void LimpiarCasillas()
        {
            // Todas las casillas se recargan
            // Combos
            CargarComboTipo();
            CargarComboMarca();
            CargarComboProveedor();
            CargarComboSPEC();
            // Txt
            TxtDetalles.Text = "";
            TxtPrecioBulto.Text = "";
            TxtCantidad.Text = "";
            TxtPrecioUnidad.Text = "";
            TxtPorcetaje.Text = "";
            TxtPrecioVenta.Text = "";
            TxtStock.Text = "";
            TxtPorcetaje.Text = Ganancia.ToString("0.00");
            TxtDescuento.Text = Descuento.ToString("0.00");
            // Imagen
            var notImage = new Bitmap(ViewStockNew.Properties.Resources.SinImagen2);
            PctImagen.Image = notImage;
            // Variable auxiliar de advertencia de datos no guardados
            auxModify = 0;
        }

        private void TxtPrecioBulto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten caracteres númericos en este campo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void TxtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten caracteres númericos en este campo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void TxtPrecioUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten caracteres númericos en este campo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void TxtPorcetaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten caracteres númericos en este campo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void TxtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten caracteres númericos en este campo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void TxtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten caracteres númericos en este campo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            if (auxModify == 1)
            {
                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea continuar sin guardar los cambios?", "Aceptar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    ClasesCompartidas.TipoNuevo = null;
                    ClasesCompartidas.MarcaNueva = null;
                    ClasesCompartidas.SpecNuevo = null;
                    ClasesCompartidas.ProveedorNuevo = null;
                    //
                    this.Close();
                }
            }
            else
            {
                ClasesCompartidas.TipoNuevo = null;
                ClasesCompartidas.MarcaNueva = null;
                ClasesCompartidas.SpecNuevo = null;
                ClasesCompartidas.ProveedorNuevo = null;
                //
                this.Close();
            }
        }

        private void BtnAgregarTipo_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var dataSend = "tipoDeProductos";
            var createDataView = new CreateDataView(dataSend, unitOfWork);
            createDataView.ShowDialog();
            CargarComboTipo();
            //
            ClasesCompartidas.TipoNuevo = null;
        }

        private void BtnAgregarMarca_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var dataSend = "marcas";
            var createDataView = new CreateDataView(dataSend, unitOfWork);
            createDataView.ShowDialog();
            CargarComboMarca();
            //
            ClasesCompartidas.MarcaNueva = null;
        }

        private void BtnAgregarSPEC_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var dataSend = "specs";
            var createDataView = new CreateDataView(dataSend, unitOfWork);
            createDataView.ShowDialog();
            CargarComboSPEC();
            //
            ClasesCompartidas.SpecNuevo = null;
        }

        private void BtnAgregarProveedor_Click(object sender, EventArgs e)
        {
            bool editando = false;
            IUnitOfWork unitOfWork = new UnitOfWork();
            var createProveedor = new CreateProveedorView(unitOfWork, editando);
            createProveedor.ShowDialog();
            CargarComboProveedor();
            //
            ClasesCompartidas.ProveedorNuevo = null;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            ClasesCompartidas.TipoNuevo = null;
            ClasesCompartidas.MarcaNueva = null;
            ClasesCompartidas.SpecNuevo = null;
            ClasesCompartidas.ProveedorNuevo = null;
            //
            this.Close();
        }

        public CreateProductView(IUnitOfWork unitOfWork, bool Editando, int idSeleccionado)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            this.editando = Editando;
            // El título del label cambia
            LblTitle.Text = "Editando Producto";

            var productoEdit = unitOfWork.ProductoRepository.GetByID(idSeleccionado);
            //
            idProductModify = idSeleccionado;
            //
            //
            CargarComboTipo(productoEdit.TipoProductoId);
            CargarComboMarca(productoEdit.MarcaId);
            CargarComboProveedor(productoEdit.ProveedorId);
            CargarComboSPEC(productoEdit.SPECId);
            //
            TxtDetalles.Text = productoEdit.Detalles;
            TxtPrecioBulto.Text = productoEdit.PrecioBulto.ToString("0.00");
            TxtCantidad.Text = Convert.ToString(productoEdit.CantidadBulto);
            TxtPrecioUnidad.Text = productoEdit.PrecioUnidad.ToString("0.00");
            TxtDescuento.Text = productoEdit.Descuento.ToString("0.00");
            TxtPorcetaje.Text = productoEdit.Ganancia.ToString("0.00");
            TxtPrecioVenta.Text = productoEdit.PVP.ToString("0.00");
            TxtStock.Text = Convert.ToString(productoEdit.Stock);
            //
            PctImagen.Image = (Bitmap)((new ImageConverter()).ConvertFrom(productoEdit.Imagen));
            //
            // Ciertos valores se almacenan, para ser comprobados cuando se efectuan cambios
            //
            _TipoId = productoEdit.TipoProductoId;
            _MarcaId = productoEdit.MarcaId;
            _SpecId = productoEdit.SPECId;
            _Detalles = productoEdit.Detalles;

        }

        public CreateProductView(IUnitOfWork unitOfWork, bool Editando, bool remito) : this(unitOfWork, Editando)
        {
            InitializeComponent();
            //
            this.unitOfWork = unitOfWork;
            this.editando = Editando;
            this.remito = remito;
            //
            CargarComboTipo();
            CargarComboMarca();
            CargarComboProveedor();
            CargarComboSPEC();
            BtnEliminarImagen.Enabled = false;
            //
            var ganancia = unitOfWork.PorcentajeGananciaRepository.GetByID(1);
            Ganancia = ganancia.Porcentaje;
            TxtPorcetaje.Text = ganancia.Porcentaje.ToString("0.00");
            //
            var descuento = unitOfWork.PorcentajeGananciaRepository.GetByID(2);
            Descuento = descuento.Porcentaje;
            TxtDescuento.Text = descuento.Porcentaje.ToString("0.00");
        }

        private void BtnSubirImagen_Click(object sender, EventArgs e)
        {
            #region GetImage
            OpenFileDialog getImage = new OpenFileDialog();
            getImage.InitialDirectory = "C:\\";
            getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG (*.png)|*.png|GIF (*.gif)|*.gif";
            getImage.Title = "Seleccionar Imagen";
            if (getImage.ShowDialog() == DialogResult.OK)
            {
                PctImagen.Image = Image.FromFile(getImage.FileName);
                BtnEliminarImagen.Enabled = true;
                auxModify = 1;
            }
            else
            {
                MessageBox.Show("No se a seleccionado una imagen", "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            #endregion
        }

        private void BtnEliminarImagen_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea eliminar la imagen'?", "Eliminar ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                var notImage = new Bitmap(ViewStockNew.Properties.Resources.SinImagen2);
                PctImagen.Image = notImage;
                auxModify = 1;
            }
        }

        private void ComboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboTipo.SelectedIndex > 0)
            {
                auxModify = 1;
            }
        }

        private void ComboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboMarca.SelectedIndex > 0)
            {
                auxModify = 1;
            }
        }

        private void ComboSPEC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboSPEC.SelectedIndex > 0)
            {
                auxModify = 1;
            }
        }

        private void ComboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboProveedor.SelectedIndex > 0)
            {
                auxModify = 1;
            }
        }

        private void TxtDetalles_TextChanged(object sender, EventArgs e)
        {
            if (TxtDetalles.Text.Length > 0)
            {
                auxModify = 1;
            }
        }

        private void TxtPrecioBulto_TextChanged(object sender, EventArgs e)
        {
            if (TxtPrecioBulto.Text.Length > 0)
            {
                auxModify = 1;
            }
        }

        private void TxtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (TxtCantidad.Text.Length > 0)
            {
                auxModify = 1;
            }
        }

        private void TxtPrecioUnidad_TextChanged(object sender, EventArgs e)
        {
            if (TxtPrecioUnidad.Text.Length > 0)
            {
                auxModify = 1;
            }
        }

        private void TxtPorcetaje_TextChanged(object sender, EventArgs e)
        {
            if (TxtPorcetaje.Text.Length > 0)
            {
                auxModify = 1;
            }
        }

        private void TxtPrecioVenta_TextChanged(object sender, EventArgs e)
        {
            if (TxtPrecioVenta.Text.Length > 0)
            {
                auxModify = 1;
            }
        }

        private void TxtStock_TextChanged(object sender, EventArgs e)
        {
            if (TxtStock.Text.Length > 0)
            {
                auxModify = 1;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LimpiarCasillas();
        }

        private void BtnCalcularPVP_Click(object sender, EventArgs e)
        {
            if (TxtPrecioUnidad.Text.Length < 1 || TxtPrecioUnidad.Text == "0,00")
            {
                MessageBox.Show($"Es necesario completar el campo 'Precio Unidad'", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtPorcetaje.Text.Length < 1 || TxtPorcetaje.Text == "0")
            {
                MessageBox.Show($"Es necesario completar el campo 'Porcentaje'", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var precioUnidad = Convert.ToDecimal(TxtPrecioUnidad.Text);
                var porcentaje = Convert.ToDecimal(TxtPorcetaje.Text);
                var PVP = precioUnidad + ((precioUnidad * porcentaje) / 100);
                TxtPrecioVenta.Text = Convert.ToString(PVP);
            }
        }

        private void BtnCalcularPVPbulto_Click(object sender, EventArgs e)
        {
            if (TxtPrecioVenta.Text.Length < 1)
            {
                MessageBox.Show($"Es necesario calcular el el 'PVP'", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtCantidad.Text.Length < 1)
            {
                MessageBox.Show($"Es necesario completar el campo 'Cantidad'", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtDescuento.Text.Length < 1)
            {
                MessageBox.Show($"Es necesario completar el campo 'Descuento'", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                decimal unidad = Convert.ToDecimal(TxtPrecioUnidad.Text);
                int cantidad = Convert.ToInt32(TxtCantidad.Text);
                decimal descuento = Convert.ToDecimal(TxtDescuento.Text);
                decimal bulto = cantidad * unidad;
                //
                decimal precioPVPbulto = bulto - ((bulto * descuento) / 100);
                TxtPrecioBulto.Text = precioPVPbulto.ToString("0.00");
            }
        }

        private void TxtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten caracteres númericos en este campo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void CreateProductView_Load(object sender, EventArgs e)
        {

        }
    }
}
