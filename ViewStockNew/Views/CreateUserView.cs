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
using ViewStockNew.Utils;

namespace ViewStockNew.Views
{
    public partial class CreateUserView : Form
    {
        IUnitOfWork unitOfWork;
        bool editando;
        private int auxModify;
        private int idSeleccionado;
        private int IdUsuarioModify;
        private string _User;

        public CreateUserView(IUnitOfWork unitOfWork, bool Editando)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            this.editando = Editando;
            //
            CargarComboTipoUsuario();
            //
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.BtnGuardar, "Guardar");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.BtnCancelar, "Cancelar");
            //
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.BtnContinuar, "Continuar");
        }

        public CreateUserView(IUnitOfWork unitOfWork, bool Editando, int idSeleccionado)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            this.editando = Editando;
            // El título del label cambia
            LblTittle.Text = "Editando Proveedor";
            //
            var userEdit = unitOfWork.UsuarioRepository.GetByID(idSeleccionado);
            //
            IdUsuarioModify = idSeleccionado;
            //
            TxtNombreApellido.Text = userEdit.Nombre;
            TxtUsuario.Text = userEdit.User;
            TxtContraseña.Text = userEdit.Password;
            TxtRepeatPassword.Text = userEdit.Password;
            //
            CargarComboTipoUsuario(userEdit.TipoDeUsuarioId);
            ComboGenero.SelectedItem = userEdit.Genero;
            //
            if (userEdit.Imagen != null)
                PctImagen.Image = (Bitmap)((new ImageConverter()).ConvertFrom(userEdit.Imagen));
            //
            _User = userEdit.User;
            //
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.BtnGuardar, "Guardar");
            //
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.BtnCancelar, "Cancelar");
            //
            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.BtnContinuar, "Continuar");
        }

        private void CargarComboTipoUsuario(int? tipoUserId = 0)
        {
            ComboTipoDeUsuario.DisplayMember = "Nombre";
            ComboTipoDeUsuario.ValueMember = "Id";
            ComboTipoDeUsuario.DataSource = ClasesCompartidas.tiposdeUsuarioList;
            //
            if (tipoUserId != 0)
            {
                ComboTipoDeUsuario.SelectedValue = tipoUserId;
            }
            else
                ComboTipoDeUsuario.SelectedValue = 0;
        }

        private async void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Se Convierte la Imagen que yace en el PictureBox para ser convertida en una cade de bytes
            MemoryStream ds = new MemoryStream();
            PctImagen.Image.Save(ds, ImageFormat.Jpeg);
            byte[]? aByte = ds.ToArray();
            //
            // En las siguientes cadenas de if's se comprueba si el usuario completo ciertos campos obligatorios
            if (TxtNombreApellido.Text.Length < 1)
            {
                MessageBox.Show($"Es necesario completar el campo de 'Nombre y Apellido'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtUsuario.Text.Length < 1)
            {
                MessageBox.Show($"Es necesario completar el campo de 'Usuario'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtContraseña.Text.Length < 5)
            {
                MessageBox.Show($"Su 'Contraseña' debe poseer más seguridad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (TxtContraseña.Text != TxtRepeatPassword.Text)
            {
                MessageBox.Show($"Las Contraseñas no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ComboTipoDeUsuario.SelectedItem == null)
            {
                MessageBox.Show($"Es necesario seleccionar un Tipo para el Usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (editando)
                {
                    Usuario usuario = new Usuario()
                    {
                        Id = IdUsuarioModify,
                        Nombre = TxtNombreApellido.Text,
                        User = TxtUsuario.Text,
                        Password = HashPassword.ObtenerHashSha256(TxtContraseña.Text),
                        Genero = (string)ComboGenero.SelectedItem,
                        TipoDeUsuarioId = (int)ComboTipoDeUsuario.SelectedValue,
                        Modificacion = DateTime.Now,
                        Imagen = aByte,
                        Visible = true
                    };
                    //
                    // Se comprueba si las características esenciales del Usuario han sido alteradas
                    //
                    if (usuario.User == _User)
                    {
                        try
                        {
                            new ModelsValidator().Validate(usuario);

                            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar los cambios realizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                unitOfWork.UsuarioRepository.Update(usuario);
                                unitOfWork.Save();
                                auxModify = 0;
                                // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                // base de datos
                                ClasesCompartidas.usuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(true));
                                MessageBox.Show($"¡El usuario {usuario.User} se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        // La línea siguiente comprueba si el usuario que se está tratando de agregar ya existe entre los datos cargados
                        bool usuarioExistente = unitOfWork.UsuarioRepository.dbSet.Any(x => x.User.Equals(usuario.User));
                        //
                        if (!usuarioExistente)
                        {
                            try
                            {
                                new ModelsValidator().Validate(usuario);

                                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar los cambios realizados?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (respuesta == DialogResult.Yes)
                                {
                                    unitOfWork.UsuarioRepository.Update(usuario);
                                    unitOfWork.Save();
                                    auxModify = 0;
                                    // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                    // base de datos
                                    ClasesCompartidas.usuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(true));
                                    MessageBox.Show($"¡El usuario {usuario.User} se ha modificado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show($"El usuario {usuario.User} ya se encuentra registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    Usuario usuario = new Usuario()
                    {
                        Nombre = TxtNombreApellido.Text,
                        User = TxtUsuario.Text,
                        Password = HashPassword.ObtenerHashSha256(TxtContraseña.Text),
                        Genero = (string)ComboGenero.SelectedItem,
                        TipoDeUsuarioId = (int)ComboTipoDeUsuario.SelectedValue,
                        Modificacion = DateTime.Now,
                        Imagen = aByte,
                        Visible = true
                    };
                    // La línea siguiente comprueba si el Usuario que se está tratando de agregar ya existe entre los datos cargados
                    bool usuarioExistente = unitOfWork.UsuarioRepository.dbSet.Any(x => x.User.Equals(usuario.User));
                    //
                    if (!usuarioExistente)
                    {
                        try
                        {
                            new ModelsValidator().Validate(usuario);

                            DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea guardar este nuevo usuario?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (respuesta == DialogResult.Yes)
                            {
                                unitOfWork.UsuarioRepository.Add(usuario);
                                unitOfWork.Save();
                                auxModify = 0;
                                // Al realizar un cambio en la lista de datos, es necesario realizar otra consulta a la
                                // base de datos
                                ClasesCompartidas.usuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(true));
                                MessageBox.Show($"¡El usuario {usuario.User} se ha creado con éxito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show($"El usuario {usuario.User} ya se encuentra registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LimpiarCasillas()
        {
            TxtNombreApellido.Text = "";
            TxtUsuario.Text = "";
            TxtContraseña.Text = "";
            TxtRepeatPassword.Text = "";
            //
            ComboGenero.SelectedItem = null;
            CargarComboTipoUsuario();
            //
            var bmp = new Bitmap(Properties.Resources.SinImagen2);
            PctImagen.Image = bmp;

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtNombreApellido_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void ComboTipoDeUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboTipoDeUsuario.SelectedIndex > 0)
                auxModify = 1;
        }

        private void ComboGenero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboGenero.SelectedIndex > 0)
                auxModify = 1;
        }

        private void CreateUserView_Load(object sender, EventArgs e)
        {

        }

        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void TxtContraseña_TextChanged(object sender, EventArgs e)
        {
            auxModify = 1;
        }

        private void TxtRepeatPassword_TextChanged(object sender, EventArgs e)
        {
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

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            if (auxModify == 1)
            {
                DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea continuar sin guardar los cambios?", "Aceptar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
