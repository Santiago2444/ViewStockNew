using Desktop.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using ViewStockNew.Models;
using ViewStockNew.Utils;

namespace ViewStockNew.Data
{
    public class DBViewStockContext : DbContext
    {

        // Conexión con la base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.LogTo(m => Debug.WriteLine(m), new[] { DbLoggerCategory.Database.Name }, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .UseMySql(Helper.GetConnectionString(),
                    ServerVersion.AutoDetect(Helper.GetConnectionString()),
                    options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                   errorNumbersToAdd: null));
                
                /*
                optionsBuilder.UseMySql("Server=bcixupzwypo8hnmj6ifv-mysql.services.clever-cloud.com;Database=bcixupzwypo8hnmj6ifv;Uid=ujogvmdkuv7kdus8;Pwd=9vjHMjwC2v902MwDC3Vc;", ServerVersion.AutoDetect("Server=-bcixupzwypo8hnmj6ifv-mysql.services.clever-cloud.com;Database=bcixupzwypo8hnmj6ifv;Uid=ujogvmdkuv7kdus8;Pwd=9vjHMjwC2v902MwDC3Vc;"),
                 options => options.EnableRetryOnFailure(
                 maxRetryCount: 5,
                 maxRetryDelay: System.TimeSpan.FromSeconds(30),
                 errorNumbersToAdd: null));
                */
            }
        }

        // Los DbSet de los modelos 
        public DbSet<CodigoVenta> CodigosVentas { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<SPEC> SPECS { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }
        public DbSet<TipoProducto> TipoProducto { get; set; }
        public DbSet<TipoDeUsuario> TipoDeUsuarios { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<PorcentajeGanancia> PorcentajeGanancias { get; set; }
        public DbSet<Remito> Remitos { get; set; }
        public DbSet<RemitoDetalle> RemitoDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CodigoVenta>(entity =>
            {
                entity.ToTable("codigoDeVentas");
            });
            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.ToTable("localidades");
            });
            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.ToTable("provincias");
            });
            //modelBuilder.Entity<Usuario>(entity =>
            //{
            //    entity.ToTable("usuarios");
            //});
            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("marcas");
            });
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");
            });
            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.ToTable("tipoDeProducto");
            });
            modelBuilder.Entity<SPEC>(entity =>
            {
                entity.ToTable("especificaciones");
            });
            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.ToTable("tipoProductos");
            });
            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("proveedores");
            });
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("ventas");
            });
            modelBuilder.Entity<VentaDetalle>(entity =>
            {
                entity.ToTable("ventaDetalles");
            });
            modelBuilder.Entity<TipoDeUsuario>(entity =>
            {
                entity.ToTable("tiposDeUsuarios");
            });          
            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.ToTable("cuentas");
            });
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("pagos");
            });
            modelBuilder.Entity<PorcentajeGanancia>(entity =>
            {
                entity.ToTable("porcentaje");
            });
            modelBuilder.Entity<Remito>(entity =>
            {
                entity.ToTable("remitos");
            });
            modelBuilder.Entity<RemitoDetalle>(entity =>
            {
                entity.ToTable("remitoDetalles");
            });

            //-------------------
            #region CargadeLocalidades
            modelBuilder.Entity<Localidad>().HasData(
                new Localidad
                {
                    Id = 1,
                    Nombre = "San Justo",
                    Visible = true

                },
                new Localidad
                {
                    Id = 2,
                    Nombre = "Marcelino Escalada",
                    Visible = true

                },
                new Localidad
                {
                    Id = 3,
                    Nombre = "Llambi Cámpbell",
                    Visible = true

                }
            );
            #endregion
            //-------------------
            #region CargadeProvincias
            modelBuilder.Entity<Provincia>().HasData(
                new Provincia
                {
                    Id = 1,
                    Nombre = "Santa Fe",
                    Visible = true

                },
                new Provincia
                {
                    Id = 2,
                    Nombre = "Buenos Aires",
                    Visible = true

                },
                new Provincia
                {
                    Id = 3,
                    Nombre = "Córdoba",
                    Visible = true

                },
                new Provincia
                {
                    Id = 4,
                    Nombre = "Mendoza",
                    Visible = true

                }
                );
            #endregion
            //------------------
            #region CargadeTiposDeProducto
            modelBuilder.Entity<TipoProducto>().HasData(
                new TipoProducto
                {
                    Id = 1,
                    Nombre = "Gaseosas",
                    Visible = true

                },
                new TipoProducto
                {
                    Id = 2,
                    Nombre = "Vinos",
                    Visible = true

                },
                new TipoProducto
                {
                    Id = 3,
                    Nombre = "Masitas",
                    Visible = true

                }
                );
            #endregion
            //-------------------
            #region CargaDeMarcas
            modelBuilder.Entity<Marca>().HasData(
                new Marca
                {
                    Id = 1,
                    Nombre = "Coca-Cola",
                    Visible = true

                },
                new Marca
                {
                    Id = 2,
                    Nombre = "Toro",
                    Visible = true

                },
                new Marca
                {
                    Id = 3,
                    Nombre = "Bagley",
                    Visible = true

                }
                );
            #endregion
            //-------------------
            #region CargaDeSPEC'S
            modelBuilder.Entity<SPEC>().HasData(
                new SPEC
                {
                    Id = 1,
                    Nombre = "2lt",
                    Visible = true

                },
                new SPEC
                {
                    Id = 2,
                    Nombre = "750ml",
                    Visible = true

                },
                new SPEC
                {
                    Id = 3,
                    Nombre = "390g",
                    Visible = true

                }
                );
            #endregion
            //-------------------
            #region CargaDeTiposDeUsuario
            modelBuilder.Entity<TipoDeUsuario>().HasData(
                new TipoDeUsuario
                {
                    Id = 1,
                    Nombre = "SuperUsuario",
                    Visible = true

                },
                new TipoDeUsuario
                {
                    Id = 2,
                    Nombre = "Admin",
                    Visible = true

                },
                new TipoDeUsuario
                {
                    Id = 3,
                    Nombre = "Empleado",
                    Visible = true
                }
                );
            #endregion
            //-------------------
            #region CargaDeUsuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 2,
                    Nombre = "Santiago Barreto",
                    User = "superadmin",
                    Password = HashPassword.ObtenerHashSha256("1234560"),
                    Genero = "Masculino",
                    TipoDeUsuarioId = 1,
                    Visible = true
                },
                new Usuario
                {
                    Id = 3,
                    Nombre = "Encargado",
                    User = "encargado",
                    Password = HashPassword.ObtenerHashSha256("1234560"),
                    Genero = "Femenino",
                    TipoDeUsuarioId = 2,
                    Visible = true
                },
                new Usuario
                {
                    Id = 1,
                    Nombre = "Empleado",
                    User = "empleado",
                    Password = HashPassword.ObtenerHashSha256("1234560"),
                    Genero = "Masculino",
                    TipoDeUsuarioId = 3,
                    Visible = true
                }
            );
            #endregion
            //-------------------
            #region CargaDeProveedores
            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor
                {
                    Id = 1,
                    Nombre = "FiumbFast",
                    Telefono = "349862422",
                    Direccion = "Sammy 5213",
                    ProvinciaId = 1,
                    LocalidadId = 1,
                    UsuarioId = 1,
                    Visible = true
                },
                new Proveedor
                {
                    Id = 2,
                    Nombre = "El Loco Repartos",
                    Telefono = "342567432",
                    Direccion = "Aataualpa",
                    ProvinciaId = 1,
                    LocalidadId = 2,
                    UsuarioId = 1,
                    Visible = true
                }
            );
            #endregion
            //-------------------
            #region CargaDeCuentas
            modelBuilder.Entity<Cuenta>().HasData(
                new Cuenta
                {
                    Id = 1,
                    Nombre = "Consumidor Final",
                    DNI = "00000",
                    Visible = true
                }
                );
            #endregion
            //-------------------
            #region CodigoVentas
            modelBuilder.Entity<CodigoVenta>().HasData(
                new CodigoVenta
                {
                    Id = 1,
                    Codigo = 1,
                }
                );
            #endregion
            //-------------------
            #region Porcentaje
            modelBuilder.Entity<PorcentajeGanancia>().HasData(
                new PorcentajeGanancia
                {
                    Id = 1,
                    Porcentaje = 30,
                },
                new PorcentajeGanancia
                {
                    Id = 2,
                    Porcentaje = 8,
                }
                );
            #endregion
        }


        // Constructor vacio para que funcione
        public DBViewStockContext()
        {

        }


    }
}
