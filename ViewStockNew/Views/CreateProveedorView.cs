using Microsoft.EntityFrameworkCore;
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
    public partial class CreateProveedorView : Form
    {
        IUnitOfWork unitOfWork;
        bool editando;
        private int auxModify;
        private int idSeleccionado;
        private int IdProveedorModify;
        private string _Nombre;

        public CreateProveedorView(IUnitOfWork unitOfWork, bool Editando)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            this.editando = Editando;
            //
            CargarComboLocalidad();
            CargarComboProvincia();
            //
            BtnEliminarImagen.Enabled = false;
            //
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.BtnGuardar, "Guardar");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.BtnCancelar, "Cancelar");
            //
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.BtnContinuar, "Continuar");
            //
            System.Windows.Forms.ToolTip ToolTip4 = new System.Windows.Forms.ToolTip();
            ToolTip4.SetToolTip(this.BtnAgregarLocalidad, "Agregar nueva Localidad");
            //
            System.Windows.Forms.ToolTip ToolTip5 = new System.Windows.Forms.ToolTip();
            ToolTip5.SetToolTip(this.BtnAgregarProvincia, "Agregar nueva Provincia");

        }

        public CreateProveedorView(IUnitOfWork unitOfWork, bool Editando, int idSeleccionado)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            this.editando = Editando;
            //
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.BtnGuardar, "Guardar");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.BtnCancelar, "Cancelar");
            //
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.BtnContinuar, "Continuar");
            //
            System.Windows.Forms.ToolTip ToolTip4 = new System.Windows.Forms.ToolTip();
            ToolTip4.SetToolTip(this.BtnAgregarLocalidad, "Agregar nueva Localidad");
            //
            System.Windows.Forms.ToolTip ToolTip5 = new System.Windows.Forms.ToolTip();
            ToolTip5.SetToolTip(this.BtnAgregarProvincia, "Agregar nueva Provincia");

            // El título del label cambia
            LblTittle.Text = "Editando Proveedor";
            //
            var proveedorEdit = unitOfWork.ProveedorRepository.GetByID(idSeleccionado);
            //
            IdProveedorModify = idSeleccionado;
            //
            TxtNombre.Text = proveedorEdit.Nombre;
            TxtDireccion.Text = proveedorEdit.Direccion;
            TxtEmail.Text = proveedorEdit.Email;
            TxtTelefono.Text = proveedorEdit.Telefono;
            //
            if (proveedorEdit.LocalidadId != null && proveedorEdit.ProvinciaId != null)
            {
                CargarComboLocalidad(proveedorEdit.LocalidadId);
                CargarComboProvincia(proveedorEdit.ProvinciaId);
            }
            else
            {
                CargarComboLocalidad();
                CargarComboProvincia();
            }
            //
            if (proveedorEdit.Imagen != null)
            {
                PctImagen.Image = (Bitmap)((new ImageConverter()).ConvertFrom(proveedorEdit.Imagen));
            }
            //
            _Nombre = proveedorEdit.Nombre;
        }

        private void CargarComboProvincia(int? provinciaId = 0)
        {
            ComboProvincia.DisplayMember = "Nombre";
            ComboProvincia.ValueMember = "Id";
            ComboProvincia.DataSource = ClasesCompartidas.provinciasList;
            //
            if (provinciaId != 0)
            {
                ComboProvincia.SelectedValue = provinciaId;
            }
            else
                ComboProvincia.SelectedValue = 0;
            //
            if (ClasesCompartidas.ProvinciaNueva != null)
            {
                int index = (ComboProvincia.Items.Count);
                ComboProvincia.DataSource = ClasesCompartidas.provinciasList;
                ComboProvincia.DisplayMember = "Nombre";
                ComboProvincia.SelectedIndex = index - 1;
            }
        }

        private void CargarComboLocalidad(int? localidadId = 0)
        {
            ComboLocalidad.DisplayMember = "Nombre";
            ComboLocalidad.ValueMember = "Id";
            ComboLocalidad.DataSource = ClasesCompartidas.localidadList;
            //
            if (localidadId != 0)
            {
                ComboLocalidad.SelectedValue = localidadId;
            }
            else
                ComboLocalidad.SelectedValue = 0;
            //
            if (ClasesCompartidas.LocalidadNueva != null)
            {
                int index = (ComboLocalidad.Items.Count);
                ComboLocalidad.DataSource = ClasesCompartidas.localidadList;
                ComboLocalidad.DisplayMember = "Nombre";
                ComboLocalidad.SelectedIndex = index - 1;
            }
        }

        private void CreateProveedorView_Load(object sender, EventArgs e)
        {

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            ClasesCompartidas.LocalidadNueva = null;
            ClasesCompartidas.ProvinciaNueva = null;
            //
            Close();
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Se Convierte la Imagen que yace en el PictureBox para ser convertida en una cade de bytes
            MemoryStream ds = new MemoryStream();
            PctImagen.Image.Save(ds, ImageFormat.Jpeg);
            byte[]? aByte = ds.ToArray();
            //
            // En las siguientes cadenas de if's se comprueba si el usuario completo ciertos campos obligatorios
            if (TxtNombre.Text.Length < 1)
            {
                MessageBox.Show($"Es necesario completar el campo de 'Nombre'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtTelefono.Text.Length < 1)
            {
                MessageBox.Show($"Es necesario completar el campo de 'Teléfono'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (editando)
                {
                    Proveedor proveedor = new Proveedor()
                    {
                        // Al ser un Proveedor modificado el Id se debe mantener
                        Id = IdProveedorModify,
                        Nombre = TxtNombre.Text,
                        Telefono = TxtTelefono.Text,
                        Email = TxtEmail.Text,
                        Direccion = TxtDireccion.Text,
                        ProvinciaId = (int)ComboProvincia.SelectedValue,
                        LocalidadId = (int)ComboLocalidad.SelectedValue,
                        Modificacion = DateTime.Now,
                        UsuarioId = ClasesCompartidas.UserId,
                        Visible = true,
                        Imagen = aByte
                    };
                    // A continuación se comprueba si el nombre del proveedor fue alterado
                    if (_Nombre == proveedor.Nombre)
                    {
                        try
                        {
                            new ModelsValidator().Validate(proveedor);

                            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar los cambios realizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                unitOfWork.ProveedorRepository.Update(proveedor);
                                unitOfWork.Save();
                                auxModify = 0;
                                //
                                // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                // base de datos
                                ClasesCompartidas.proveedoresList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(true));
                                MessageBox.Show($"¡El Proveedor {proveedor.Nombre} se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        // La línea siguiente comprueba si el nombre modificado existe dentro de los cargados 
                        bool proveedorExistente = unitOfWork.ProveedorRepository.dbSet.Any(x => x.Nombre.Equals(proveedor.Nombre));
                        //
                        if (!proveedorExistente)
                        {
                            try
                            {
                                new ModelsValidator().Validate(proveedor);

                                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar los cambios realizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    unitOfWork.ProveedorRepository.Update(proveedor);
                                    unitOfWork.Save();
                                    auxModify = 0;
                                    //
                                    // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                    // base de datos
                                    ClasesCompartidas.proveedoresList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(true));
                                    MessageBox.Show($"¡El Proveedor {proveedor.Nombre} se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show($"El proveedor que está intentando modificar posee características idénticas a un proeedor ya existente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    Proveedor proveedor = new Proveedor()
                    {
                        // Al ser un Proveedor nuevo el Id es autoincremental
                        Nombre = TxtNombre.Text,
                        Telefono = TxtTelefono.Text,
                        Email = TxtEmail.Text,
                        Direccion = TxtDireccion.Text,
                        ProvinciaId = (int?)ComboProvincia.SelectedValue,
                        LocalidadId = (int?)ComboLocalidad.SelectedValue,
                        Modificacion = DateTime.Now,
                        UsuarioId = ClasesCompartidas.UserId,
                        Visible = true,
                        Imagen = aByte
                    };
                    //
                    // La línea siguiente comprueba si el proveedor que se está tratando de agregar ya existe entre los datos cargados
                    bool proveedorExistente = unitOfWork.ProveedorRepository.dbSet.Any(x => x.Nombre.Equals(proveedor.Nombre));
                    //
                    if (!proveedorExistente)
                    {
                        try
                        {
                            new ModelsValidator().Validate(proveedor);

                            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar este nuevo proveedor?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                ClasesCompartidas.ProveedorNuevo = TxtNombre.Text;
                                unitOfWork.ProveedorRepository.Add(proveedor);
                                unitOfWork.Save();
                                auxModify = 0;
                                // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                // base de datos
                                ClasesCompartidas.proveedoresList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(true));
                                MessageBox.Show($"¡El proveedor se ha creado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //
                                if (ClasesCompartidas.ProveedorNuevo != null)
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
                        MessageBox.Show($"El proveedor que está intentando crear posee características idénticas a un proeedor ya existente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LimpiarCasillas()
        {
            // Todas las casillas se recargan
            // Combos
            CargarComboLocalidad();
            CargarComboProvincia();
            // Txt
            TxtNombre.Text = "";
            TxtDireccion.Text = "";
            TxtEmail.Text = "";
            TxtTelefono.Text = "";
            // Imagen
            var notImage = new Bitmap(ViewStockNew.Properties.Resources.SinImagen2);
            PctImagen.Image = notImage;
            // Variable auxiliar de advertencia de datos no guardados
            auxModify = 0;
        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void TxtTelefono_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void TxtDireccion_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void TxtEmail_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void ComboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboProvincia.SelectedIndex > 0)
                auxModify = 1;
        }

        private void ComboLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboLocalidad.SelectedIndex > 0)
                auxModify = 1;
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

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            if (auxModify == 1)
            {
                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea continuar sin guardar los cambios?", "Aceptar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    ClasesCompartidas.LocalidadNueva = null;
                    ClasesCompartidas.ProvinciaNueva = null;
                    //
                    this.Close();
                }
            }
            else
            {
                ClasesCompartidas.LocalidadNueva = null;
                ClasesCompartidas.ProvinciaNueva = null;
                //
                this.Close();
            }
        }

        private void BtnAgregarProvincia_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var dataSend = "provincia";
            //
            var createDataView = new CreateDataView(dataSend, unitOfWork);
            createDataView.ShowDialog();
            //
            CargarComboProvincia();
        }

        private void BtnAgregarLocalidad_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var dataSend = "localidad";
            //
            var createDataView = new CreateDataView(dataSend, unitOfWork);
            createDataView.ShowDialog();
            //
            CargarComboLocalidad();
        }
    }
}
