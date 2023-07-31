using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewStockNew.Views
{
    public partial class ZformsView : Form
    {
        public ZformsView()
        {
            InitializeComponent();
            /*
             *        
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]  
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        public string? SitioWebUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? Email { get; set;}
        public byte[]? BannerImage { get; set; }
        public byte[]? LogoImage { get; set; }
        public DateTime? Modificacion { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

            private void CargarProducto(int tipoDeProdudctoTwoId, int marcaId, string detalles, int specId,
            decimal precioBulto, decimal precioUnidad, decimal porcentaje, decimal pvp, int stock, int proveedorId,
             bool visible, DateTime modificacion, int usuarioId,  ModelBuilder modelBuilder, ref int id)
        {
            modelBuilder.Entity<Producto>().HasData(
            new Producto
            {
                Id = id,
                TipoProductoTwoId = tipoDeProdudctoTwoId,
                MarcaId = marcaId,
                Detalles = detalles,
                SPECId = specId,
                PrecioBulto = precioBulto,
                PrecioUnidad = precioUnidad,
                Porcentaje = porcentaje,
                PVP = pvp,
                Stock = stock,
                ProveedorId = proveedorId,
                Visible = visible,
                Modificacion = modificacion,
                UsuarioId = usuarioId,
            });
            id++;
        }

        private void CargarProveedor(string nombre, string telefono, string email, string direccion,
            int provinciaId, int localidadId, bool visible, ModelBuilder modelBuilder, ref int id)
        {
            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor
                {
                    Id = id,
                    Nombre = nombre,
                    Telefono = telefono,
                    Email = email,
                    Direccion = direccion,
                    ProvinciaId = provinciaId,
                    LocalidadId = localidadId,
                    Visible = visible,
                });
            id++;
        }

        private void CargarUsuario(string nombre, string user, string password, string genero,
            int tipoDeUsuarioId, bool visible, ModelBuilder modelBuilder, ref int id)
        {
            modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = id,
                Nombre = nombre,
                User = user,
                Password = password,
                Genero = genero,
                TipoDeUsuarioId = tipoDeUsuarioId,
                Visible = visible,
            });
            id++;
        }

        private void CargarCuenta(string nombre, string dni, string telefono, string telefonoDos, 
            string domicilio, string email, int provinciaId, int localidadId, bool visible, decimal deuda,
            DateTime modificacion, int usuarioId, ModelBuilder modelBuilder, ref int id)
        {
            modelBuilder.Entity<Cuenta>().HasData(
            new Cuenta
            {
                Id = id,
                Nombre = nombre,
                DNI = dni,
                Telefono = telefono,
                TelefonoDos = telefonoDos,
                Domicilio   = domicilio,
                Email = email,
                ProvinciaId = provinciaId,
                LocalidadId = localidadId,
                Visible = visible,
                Deuda = deuda,
                Modificacion = modificacion,
                UsuarioId = usuarioId
            });
            id++;
        }
             */
        }
    }
}
