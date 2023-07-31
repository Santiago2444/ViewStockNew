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
using ViewStockNew.Utils;

namespace ViewStockNew.Views
{
    public partial class LoginView : Form
    {
        IUnitOfWork unitOfWork;
        public LoginView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
        }

        private async void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            var usuarios = await unitOfWork.UsuarioRepository.GetAllAsync();
            var txtUser = TxtUser.Text;
            var password = HashPassword.ObtenerHashSha256(TxtPassword.Text);
            var listUsersCount = usuarios.Count();

            // Busca si existen los datos ingresados, si es asi, los almacena
            foreach (Usuario item in usuarios)
            {
                for (int i = 0; i < listUsersCount; i++)
                {
                    if (txtUser == item.User && password == item.Password)
                    {
                        ClasesCompartidas.UserId = item.Id;
                        ClasesCompartidas.TipoUsuarioId = item.TipoDeUsuarioId;
                    }
                }
            }

            // Al encontrar un Usuario, esta ventana se cierra, dando lugar a la de Iniciar el Turno
            if (ClasesCompartidas.UserId != null && ClasesCompartidas.TipoUsuarioId != null)
            {
                var mainMenuView = new MainMenuView();
                mainMenuView.ShowDialog();

                if (ClasesCompartidas.UserId != null)
                {
                    this.Close();
                }
            }
            else
            {
                TxtUser.Text = " ";
                TxtPassword.Text = " ";
                //MessageBox.Show("No se encontro una coincidencia con el Usuario ingresado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Usuario o Contraseña Incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs argumento)
        {
            if (argumento.KeyCode == Keys.Enter)
                BtnIniciarSesion.PerformClick();
        }
    }
}
