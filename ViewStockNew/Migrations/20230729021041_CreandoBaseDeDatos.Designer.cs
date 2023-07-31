﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViewStockNew.Data;

#nullable disable

namespace ViewStockNew.Migrations
{
    [DbContext(typeof(DBViewStockContext))]
    [Migration("20230729021041_CreandoBaseDeDatos")]
    partial class CreandoBaseDeDatos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ViewStockNew.Models.CodigoVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("codigoDeVentas", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Codigo = 1
                        });
                });

            modelBuilder.Entity("ViewStockNew.Models.Cuenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Deuda")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Domicilio")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("longblob");

                    b.Property<int?>("LocalidadId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ProvinciaId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Saldo")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Telefono")
                        .HasColumnType("longtext");

                    b.Property<string>("TelefonoDos")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("LocalidadId");

                    b.HasIndex("ProvinciaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("cuentas", (string)null);
                });

            modelBuilder.Entity("ViewStockNew.Models.Localidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("localidades", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "San Justo",
                            Visible = true
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Marcelino Escalada",
                            Visible = true
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Llambi Cámpbell",
                            Visible = true
                        });
                });

            modelBuilder.Entity("ViewStockNew.Models.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("marcas", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Coca-Cola",
                            Visible = true
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Toro",
                            Visible = true
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Bagley",
                            Visible = true
                        });
                });

            modelBuilder.Entity("ViewStockNew.Models.Pago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CuentaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Dinero")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Importe")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Tipo")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<decimal>("Vuelto")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CuentaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("pagos", (string)null);
                });

            modelBuilder.Entity("ViewStockNew.Models.PorcentajeGanancia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Porcentaje")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("porcentaje", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Porcentaje = 30m
                        },
                        new
                        {
                            Id = 2,
                            Porcentaje = 8m
                        });
                });

            modelBuilder.Entity("ViewStockNew.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CantidadBulto")
                        .HasColumnType("int");

                    b.Property<int>("Carrito")
                        .HasColumnType("int");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Detalles")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Ganancia")
                        .HasColumnType("decimal(65,30)");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("longblob");

                    b.Property<int>("MarcaId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("PVP")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PrecioBulto")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PrecioUnidad")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("ProveedorId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("SPECId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("TipoProductoId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.HasIndex("ProveedorId");

                    b.HasIndex("SPECId");

                    b.HasIndex("TipoProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("productos", (string)null);
                });

            modelBuilder.Entity("ViewStockNew.Models.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("longblob");

                    b.Property<int?>("LocalidadId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ProvinciaId")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("LocalidadId");

                    b.HasIndex("ProvinciaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("proveedores", (string)null);
                });

            modelBuilder.Entity("ViewStockNew.Models.Provincia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("provincias", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Santa Fe",
                            Visible = true
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Buenos Aires",
                            Visible = true
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Córdoba",
                            Visible = true
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Mendoza",
                            Visible = true
                        });
                });

            modelBuilder.Entity("ViewStockNew.Models.Remito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CantidadProductos")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Importe")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<string>("TipoComprobante")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("remitos", (string)null);
                });

            modelBuilder.Entity("ViewStockNew.Models.RemitoDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CantidadBultos")
                        .HasColumnType("int");

                    b.Property<int?>("CantidadTotal")
                        .HasColumnType("int");

                    b.Property<int?>("CantidadXBultos")
                        .HasColumnType("int");

                    b.Property<string>("Detalles")
                        .HasColumnType("longtext");

                    b.Property<int>("MarcaId")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioBulto")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PrecioTotal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("RemitoId")
                        .HasColumnType("int");

                    b.Property<int?>("SPECId")
                        .HasColumnType("int");

                    b.Property<int>("TipoProductoId")
                        .HasColumnType("int");

                    b.Property<bool>("bulto")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.HasIndex("SPECId");

                    b.HasIndex("TipoProductoId");

                    b.ToTable("remitoDetalles", (string)null);
                });

            modelBuilder.Entity("ViewStockNew.Models.SPEC", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("especificaciones", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "2lt",
                            Visible = true
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "750ml",
                            Visible = true
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "390g",
                            Visible = true
                        });
                });

            modelBuilder.Entity("ViewStockNew.Models.TipoDeUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("tiposDeUsuarios", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "SuperUsuario",
                            Visible = true
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Admin",
                            Visible = true
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Empleado",
                            Visible = true
                        });
                });

            modelBuilder.Entity("ViewStockNew.Models.TipoProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("tipoProductos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Gaseosas",
                            Visible = true
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Vinos",
                            Visible = true
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Masitas",
                            Visible = true
                        });
                });

            modelBuilder.Entity("ViewStockNew.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("longblob");

                    b.Property<DateTime?>("Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TipoDeUsuarioId")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("TipoDeUsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ViewStockNew.Models.Venta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Articulos")
                        .HasColumnType("int");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<int>("CuentaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Dinero")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Importe")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("PagoId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<decimal>("Vuelto")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CuentaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ventas", (string)null);
                });

            modelBuilder.Entity("ViewStockNew.Models.VentaDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Bulto")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int?>("CantidadBultos")
                        .HasColumnType("int");

                    b.Property<int?>("CantidadXBultos")
                        .HasColumnType("int");

                    b.Property<int>("CodigoDeVenta")
                        .HasColumnType("int");

                    b.Property<int?>("CuentaId")
                        .HasColumnType("int");

                    b.Property<string>("Detalles")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("FechaDePago")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("longblob");

                    b.Property<int?>("MarcaId")
                        .HasColumnType("int");

                    b.Property<decimal>("PVP")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Pagado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("PrecioBulto")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<int?>("SPECId")
                        .HasColumnType("int");

                    b.Property<int?>("TipoProductoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CuentaId");

                    b.HasIndex("MarcaId");

                    b.HasIndex("ProveedorId");

                    b.HasIndex("SPECId");

                    b.HasIndex("TipoProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ventaDetalles", (string)null);
                });

            modelBuilder.Entity("ViewStockNew.Models.Cuenta", b =>
                {
                    b.HasOne("ViewStockNew.Models.Localidad", "Localidad")
                        .WithMany()
                        .HasForeignKey("LocalidadId");

                    b.HasOne("ViewStockNew.Models.Provincia", "Provincia")
                        .WithMany()
                        .HasForeignKey("ProvinciaId");

                    b.HasOne("ViewStockNew.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Localidad");

                    b.Navigation("Provincia");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ViewStockNew.Models.Pago", b =>
                {
                    b.HasOne("ViewStockNew.Models.Cuenta", "Cuenta")
                        .WithMany()
                        .HasForeignKey("CuentaId");

                    b.HasOne("ViewStockNew.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Cuenta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ViewStockNew.Models.Producto", b =>
                {
                    b.HasOne("ViewStockNew.Models.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViewStockNew.Models.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViewStockNew.Models.SPEC", "SPEC")
                        .WithMany()
                        .HasForeignKey("SPECId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViewStockNew.Models.TipoProducto", "TipoProducto")
                        .WithMany()
                        .HasForeignKey("TipoProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViewStockNew.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marca");

                    b.Navigation("Proveedor");

                    b.Navigation("SPEC");

                    b.Navigation("TipoProducto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ViewStockNew.Models.Proveedor", b =>
                {
                    b.HasOne("ViewStockNew.Models.Localidad", "Localidad")
                        .WithMany()
                        .HasForeignKey("LocalidadId");

                    b.HasOne("ViewStockNew.Models.Provincia", "Provincia")
                        .WithMany()
                        .HasForeignKey("ProvinciaId");

                    b.HasOne("ViewStockNew.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Localidad");

                    b.Navigation("Provincia");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ViewStockNew.Models.Remito", b =>
                {
                    b.HasOne("ViewStockNew.Models.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViewStockNew.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Proveedor");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ViewStockNew.Models.RemitoDetalle", b =>
                {
                    b.HasOne("ViewStockNew.Models.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViewStockNew.Models.SPEC", "SPEC")
                        .WithMany()
                        .HasForeignKey("SPECId");

                    b.HasOne("ViewStockNew.Models.TipoProducto", "TipoProducto")
                        .WithMany()
                        .HasForeignKey("TipoProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marca");

                    b.Navigation("SPEC");

                    b.Navigation("TipoProducto");
                });

            modelBuilder.Entity("ViewStockNew.Models.Usuario", b =>
                {
                    b.HasOne("ViewStockNew.Models.TipoDeUsuario", "TipoDeUsuario")
                        .WithMany()
                        .HasForeignKey("TipoDeUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoDeUsuario");
                });

            modelBuilder.Entity("ViewStockNew.Models.Venta", b =>
                {
                    b.HasOne("ViewStockNew.Models.Cuenta", "Cuenta")
                        .WithMany()
                        .HasForeignKey("CuentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViewStockNew.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Cuenta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ViewStockNew.Models.VentaDetalle", b =>
                {
                    b.HasOne("ViewStockNew.Models.Cuenta", "Cuenta")
                        .WithMany()
                        .HasForeignKey("CuentaId");

                    b.HasOne("ViewStockNew.Models.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId");

                    b.HasOne("ViewStockNew.Models.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId");

                    b.HasOne("ViewStockNew.Models.SPEC", "SPEC")
                        .WithMany()
                        .HasForeignKey("SPECId");

                    b.HasOne("ViewStockNew.Models.TipoProducto", "TipoProducto")
                        .WithMany()
                        .HasForeignKey("TipoProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViewStockNew.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuenta");

                    b.Navigation("Marca");

                    b.Navigation("Proveedor");

                    b.Navigation("SPEC");

                    b.Navigation("TipoProducto");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
