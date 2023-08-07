using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ViewStockNew.Interfaces;
using ViewStockNew.Models;
using ViewStockNew.Repositories;
using ViewStockNew.Utils;
using Application = System.Windows.Forms.Application;

namespace ViewStockNew.Views
{
    public partial class SplashView : Form
    {
        IUnitOfWork unitOfWork;

        bool CargarBDD = false;
        private int AppExit = 0;
        public SplashView()
        {
            InitializeComponent();
            IUnitOfWork UnitOfWork = new UnitOfWork();
            unitOfWork = UnitOfWork;
            //       
            CargaDeDatosMySqlAsync();
        }

        private void TmrTiempo_Tick(object sender, EventArgs e)
        {
            ProBarSplash.Value = ProBarSplash.Value + 2;

            if (ProBarSplash.Value == 100 || (CargarBDD))
            {
                if (CargarBDD)
                {
                    //apagamos el cronometro
                    TmrTiempo.Enabled = false;
                    // Antes se abría un formulario destinado al inicio de sesión
                    //IUnitOfWork unitOfWork = new UnitOfWork();
                    //var loginView = new LoginView(unitOfWork);
                    //loginView.Show();

                    if (TmrTiempo.Enabled == false)
                    {
                        PctLoading.Visible = false;
                        //
                        PctTittle.Visible = true;
                        LblSesion.Visible = true;
                        TxtUser.Visible = true;
                        TxtPassword.Visible = true;
                        LblContraseña.Visible = true;
                        BtnIniciarSesion.Visible = true;
                        LblMessage.Visible = true;
                    }
                }
                else
                {
                    ProBarSplash.Value = 0;
                }
            }
        }

        private void SplashView_Activated(object sender, EventArgs e)
        {


            this.FormClosing += delegate
            {
                if (AppExit == 0)
                {
                    Application.Exit();
                }
            };

        }

        private async void CargaDeDatosMySqlAsync()
        {
            // Datos varios y listas de cosas habilitadas
            ClasesCompartidas.localidadList.DataSource = await unitOfWork.LocalidadRepository.GetAllAsync();

            ClasesCompartidas.marcasList.DataSource = await unitOfWork.MarcaRepository.GetAllAsync();

            ClasesCompartidas.provinciasList.DataSource = await unitOfWork.ProvinciaRepository.GetAllAsync();

            ClasesCompartidas.specsList.DataSource = await unitOfWork.SPECRepository.GetAllAsync();

            ClasesCompartidas.tiposProductosList.DataSource = await unitOfWork.TipoProductoRepository.GetAllAsync();

            ClasesCompartidas.tiposdeUsuarioList.DataSource = await unitOfWork.TipoUsuarioRepository.GetAllAsync();

            ClasesCompartidas.ventasList.DataSource = await unitOfWork.VentaRepository.GetAllAsync(include: q => q.Include(q => q.Usuario).Include(q => q.Cuenta), filter: q => q.Estado == "Pagado");

            ClasesCompartidas.ventaDetallesList.DataSource = await unitOfWork.VentaDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Proveedor).Include(q => q.Usuario).Include(q => q.Cuenta).Include(q => q.Venta), filter: q => q.Pagado == "Si");

            ClasesCompartidas.productosList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(true));

            ClasesCompartidas.proveedoresList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(true));

            ClasesCompartidas.cuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(true));

            ClasesCompartidas.usuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(true));

            ClasesCompartidas.remitosDetalle.DataSource = await unitOfWork.RemitoDetalleRepository.GetAllAsync(include: q => q.Include(q => q.TipoProducto).Include(q => q.Marca).Include(q => q.SPEC).Include(q => q.Remito));

            ClasesCompartidas.remitos.DataSource = await unitOfWork.RemitoRepository.GetAllAsync(include: q => q.Include(q => q.Proveedor).Include(q => q.Usuario));

            // Lista de Datos Deshabilitados
            ClasesCompartidas.DesProductList.DataSource = await unitOfWork.ProductoRepository.GetAllAsync(include: c => c.Include(c => c.TipoProducto).Include(c => c.Marca).Include(c => c.SPEC).Include(c => c.Usuario).Include(c => c.Proveedor), filter: v => v.Visible.Equals(false));

            ClasesCompartidas.DesProveedorList.DataSource = await unitOfWork.ProveedorRepository.GetAllAsync(include: c => c.Include(c => c.Provincia).Include(c => c.Localidad).Include(c => c.Usuario), filter: v => v.Visible.Equals(false));

            ClasesCompartidas.DesUsuariosList.DataSource = await unitOfWork.UsuarioRepository.GetAllAsync(include: c => c.Include(c => c.TipoDeUsuario), filter: v => v.Visible.Equals(false));

            ClasesCompartidas.DesCuentasList.DataSource = await unitOfWork.CuentaRepository.GetAllAsync(include: c => c.Include(c => c.Usuario).Include(c => c.Provincia).Include(c => c.Localidad), filter: v => v.Visible.Equals(false));
            //
            CargarBDD = true;
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
                if (txtUser == item.User && password == item.Password && ClasesCompartidas.UserId == null)
                {
                    ClasesCompartidas.UserId = item.Id;
                    ClasesCompartidas.TipoUsuarioId = item.TipoDeUsuarioId;
                }
            }

            // Al encontrar un Usuario, esta ventana se cierra, dando lugar a la de Iniciar el Turno
            if (ClasesCompartidas.UserId != null && ClasesCompartidas.TipoUsuarioId != null)
            {
                var mainMenuView = new MainMenuView();
                mainMenuView.Show();
                AppExit = 1;

                if (ClasesCompartidas.UserId != null)
                {
                    Close();
                }
            }
            else
            {
                //MessageBox.Show("No se encontro una coincidencia con el Usuario ingresado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.MessageBox.Show("Usuario o Contraseña Incorrectos", "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs argumento)
        {
            if (argumento.KeyCode == Keys.Enter)
                BtnIniciarSesion.PerformClick();
        }

        private void SplashView_Load(object sender, EventArgs e)
        {

        }

        private void PctTittle_Click(object sender, EventArgs e)
        {

        }
    }
}
