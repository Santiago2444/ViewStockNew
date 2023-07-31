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

    public partial class CreateAccount : Form
    {
        IUnitOfWork unitOfWork;
        bool editando;
        private int auxModify;
        private int idSeleccionado;
        private int IdCuentaModify;
        private string _Nombre;
        private string _DNI;

        public CreateAccount(IUnitOfWork unitOfWork, bool Editando)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            this.editando = Editando;
            //
            CargarComboProvincia();
            CargarComboLocalidad();
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

        public CreateAccount(IUnitOfWork unitOfWork, bool Editando, int idSeleccionado)
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
            LblTittle.Text = "Editando Cuenta";
            //
            var cuentaEdit = unitOfWork.CuentaRepository.GetByID(idSeleccionado);
            //
            IdCuentaModify = idSeleccionado;
            //
            TxtNombre.Text = cuentaEdit.Nombre;
            TxtDni.Text = cuentaEdit.DNI;
            TxtDomicilio.Text = cuentaEdit.Domicilio;
            TxtTelefono.Text = cuentaEdit.Telefono;
            TxtTelefonoTwo.Text = cuentaEdit.TelefonoDos;
            TxtEmail.Text = cuentaEdit.Email;
            //
            CargarComboProvincia(cuentaEdit.ProvinciaId);
            CargarComboLocalidad(cuentaEdit.LocalidadId);
            //
            if (cuentaEdit.Imagen != null)
                PctImagen.Image = (Bitmap)((new ImageConverter()).ConvertFrom(cuentaEdit.Imagen));
            //
            _Nombre = cuentaEdit.Nombre;
            _DNI = cuentaEdit.DNI;
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

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Booleanos necesarios más abajo xD
            bool cuentaExistenteDNI;
            bool cuentaExistente;
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
            else if (TxtDni.Text.Length < 1)
            {
                MessageBox.Show($"Es necesario completar el campo de 'DNI'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (editando)
                {
                    Cuenta cuenta = new Cuenta()
                    {
                        Id = IdCuentaModify,
                        Nombre = TxtNombre.Text,
                        DNI = TxtDni.Text,
                        Telefono = TxtTelefono.Text,
                        TelefonoDos = TxtTelefonoTwo.Text,
                        Domicilio = TxtDomicilio.Text,
                        Email = TxtEmail.Text,
                        ProvinciaId = (int)ComboProvincia.SelectedValue,
                        LocalidadId = (int)ComboLocalidad.SelectedValue,
                        Visible = true,
                        Imagen = aByte,
                        UsuarioId = ClasesCompartidas.UserId,
                        Modificacion = DateTime.Now

                    };
                    // Se comprueba si el DNI y el Nombre de la cuenta han sufrido algun tipo de alteración
                    if (_Nombre == cuenta.Nombre && _DNI == cuenta.DNI)
                    {
                        try
                        {
                            new ModelsValidator().Validate(cuenta);

                            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar la cuenta {cuenta.Nombre}?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                unitOfWork.CuentaRepository.Update(cuenta);
                                unitOfWork.Save();
                                auxModify = 0;
                                // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                // base de datos
                                ClasesCompartidas.cuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(true));
                                MessageBox.Show($"¡La Cuenta {cuenta.Nombre} se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        if (_Nombre != cuenta.Nombre)
                        {
                            cuentaExistente = unitOfWork.CuentaRepository.dbSet.Any(x => x.Nombre.Equals(cuenta.Nombre));
                        }
                        else
                        {
                            cuentaExistente = false;
                        }
                        //
                        if (_DNI != cuenta.DNI)
                        {
                            cuentaExistenteDNI = unitOfWork.CuentaRepository.dbSet.Any(x => x.DNI.Equals(cuenta.DNI));
                        }
                        else
                        {
                            cuentaExistenteDNI = false;
                        }
                        //
                        if (!cuentaExistente && !cuentaExistenteDNI)
                        {
                            // Una vez comprobado que el Nombre y el de DNI de la cuenta propuesto se encuentran disponibles para usar
                            // Se procede a guardar los cambios ♥
                            try
                            {
                                new ModelsValidator().Validate(cuenta);

                                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar la cuenta {cuenta.Nombre}?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    unitOfWork.CuentaRepository.Update(cuenta);
                                    unitOfWork.Save();
                                    auxModify = 0;
                                    // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                    // base de datos
                                    ClasesCompartidas.cuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(true));
                                    MessageBox.Show($"¡La Cuenta {cuenta.Nombre} se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            if (cuentaExistente == true && cuentaExistenteDNI == true)
                            {
                                MessageBox.Show($"La cuenta {cuenta.Nombre} y el DNI:{cuenta.DNI} ya se encuentra registrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (cuentaExistente == true)
                            {
                                MessageBox.Show($"La cuenta {cuenta.Nombre} ya se encuentra registrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else if (cuentaExistenteDNI == true)
                            {
                                MessageBox.Show($"El DNI:{cuenta.DNI} pertenece a una de las cuentas registradas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            //
                        }
                    }
                }
                else
                {
                    Cuenta cuenta = new Cuenta()
                    {
                        Nombre = TxtNombre.Text,
                        DNI = TxtDni.Text,
                        Telefono = TxtTelefono.Text,
                        TelefonoDos = TxtTelefonoTwo.Text,
                        Domicilio = TxtDomicilio.Text,
                        Email = TxtEmail.Text,
                        ProvinciaId = (int)ComboProvincia.SelectedValue,
                        LocalidadId = (int)ComboLocalidad.SelectedValue,
                        Visible = true,
                        Imagen = aByte,
                        UsuarioId = ClasesCompartidas.UserId,
                        Deuda = 0,
                        Saldo = 0,
                        Modificacion = DateTime.Now
                    };
                    // La línea siguiente comprueba si la Cuenta que se está tratando de agregar ya existe entre los datos cargados
                    cuentaExistente = unitOfWork.CuentaRepository.dbSet.Any(x => x.Nombre.Equals(cuenta.Nombre));
                    //
                    if (!cuentaExistente)
                    {
                        // Una vez comprobado que el nombre de la cuenta propuesto se encuentra disponible para usar
                        // se compara el DNI con los cargados, por si acaso
                        cuentaExistenteDNI = unitOfWork.CuentaRepository.dbSet.Any(x => x.DNI.Equals(cuenta.DNI));
                        //
                        if (!cuentaExistenteDNI)
                        {
                            try
                            {
                                new ModelsValidator().Validate(cuenta);

                                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar la cuenta {cuenta.Nombre}?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    unitOfWork.CuentaRepository.Add(cuenta);
                                    unitOfWork.Save();
                                    auxModify = 0;
                                    // Esto sirve para el formulario MakeSaleView, es decir al crear una cuenta nueva desde allí
                                    ClasesCompartidas.CuentaNueva = TxtNombre.Text;
                                    // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                    // base de datos
                                    ClasesCompartidas.cuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(true));
                                    MessageBox.Show($"¡La Cuenta {cuenta.Nombre} se ha creado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    // Si el formulario es abierto desde el gestor de ventas, al terminar de crear la nueva cuenta el formulario se cierra
                                    if (ClasesCompartidas.CuentaNueva != null)
                                    {
                                        Close();
                                    }
                                    else
                                    {
                                        LimpiarCasillas();
                                    }
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
                            MessageBox.Show($"El DNI:{cuenta.DNI} pertenece a una de las cuentas registradas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"La cuenta {cuenta.Nombre} ya se encuentra registrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            //
            ClasesCompartidas.ProvinciaNueva = null;
            ClasesCompartidas.LocalidadNueva = null;
        }

        private void LimpiarCasillas()
        {
            TxtNombre.Text = "";
            TxtDni.Text = "";
            TxtTelefono.Text = "";
            TxtTelefonoTwo.Text = "";
            TxtEmail.Text = "";
            TxtDomicilio.Text = "";
            //
            CargarComboLocalidad();
            CargarComboProvincia();
            //
            var notImage = new Bitmap(ViewStockNew.Properties.Resources.SinImagen2);
            PctImagen.Image = notImage;
            // Variable auxiliar de advertencia de datos no guardados
            auxModify = 0;
        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void TxtDni_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void TxtTelefono_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void TxtTelefonoTwo_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void TxtDomicilio_TextChanged(object sender, EventArgs e)
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

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            ClasesCompartidas.LocalidadNueva = null;
            ClasesCompartidas.ProvinciaNueva = null;
            //
            this.Close();
        }
    }
}
