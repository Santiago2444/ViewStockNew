using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewStockNew.Migrations
{
    public partial class CreandoBaseDeDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "codigoDeVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_codigoDeVentas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "especificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especificaciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "localidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localidades", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marcas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "porcentaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Porcentaje = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_porcentaje", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "provincias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provincias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipoProductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoProductos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tiposDeUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiposDeUsuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Genero = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoDeUsuarioId = table.Column<int>(type: "int", nullable: false),
                    Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Imagen = table.Column<byte[]>(type: "longblob", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarios_tiposDeUsuarios_TipoDeUsuarioId",
                        column: x => x.TipoDeUsuarioId,
                        principalTable: "tiposDeUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DNI = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelefonoDos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domicilio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinciaId = table.Column<int>(type: "int", nullable: true),
                    LocalidadId = table.Column<int>(type: "int", nullable: true),
                    Imagen = table.Column<byte[]>(type: "longblob", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Deuda = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Saldo = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cuentas_localidades_LocalidadId",
                        column: x => x.LocalidadId,
                        principalTable: "localidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_cuentas_provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "provincias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_cuentas_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinciaId = table.Column<int>(type: "int", nullable: true),
                    LocalidadId = table.Column<int>(type: "int", nullable: true),
                    Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Imagen = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proveedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_proveedores_localidades_LocalidadId",
                        column: x => x.LocalidadId,
                        principalTable: "localidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_proveedores_provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "provincias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_proveedores_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Importe = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Dinero = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Vuelto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Tipo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CuentaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pagos_cuentas_CuentaId",
                        column: x => x.CuentaId,
                        principalTable: "cuentas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_pagos_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoProductoId = table.Column<int>(type: "int", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    Detalles = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SPECId = table.Column<int>(type: "int", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PrecioBulto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CantidadBulto = table.Column<int>(type: "int", nullable: false),
                    PrecioUnidad = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Ganancia = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PVP = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<byte[]>(type: "longblob", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Carrito = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productos_especificaciones_SPECId",
                        column: x => x.SPECId,
                        principalTable: "especificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productos_marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productos_proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productos_tipoProductos_TipoProductoId",
                        column: x => x.TipoProductoId,
                        principalTable: "tipoProductos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productos_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "remitos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoComprobante = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Importe = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    CantidadProductos = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_remitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_remitos_proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_remitos_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Importe = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Dinero = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Vuelto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Articulos = table.Column<int>(type: "int", nullable: false),
                    CuentaId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    PagoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ventas_cuentas_CuentaId",
                        column: x => x.CuentaId,
                        principalTable: "cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ventas_pagos_PagoId",
                        column: x => x.PagoId,
                        principalTable: "pagos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ventas_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "remitoDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoProductoId = table.Column<int>(type: "int", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    Detalles = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SPECId = table.Column<int>(type: "int", nullable: true),
                    bulto = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CantidadBultos = table.Column<int>(type: "int", nullable: true),
                    CantidadXBultos = table.Column<int>(type: "int", nullable: true),
                    PrecioBulto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CantidadTotal = table.Column<int>(type: "int", nullable: true),
                    PrecioTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RemitoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_remitoDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_remitoDetalles_especificaciones_SPECId",
                        column: x => x.SPECId,
                        principalTable: "especificaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_remitoDetalles_marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_remitoDetalles_remitos_RemitoId",
                        column: x => x.RemitoId,
                        principalTable: "remitos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_remitoDetalles_tipoProductos_TipoProductoId",
                        column: x => x.TipoProductoId,
                        principalTable: "tipoProductos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ventaDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoProductoId = table.Column<int>(type: "int", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: true),
                    Detalles = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SPECId = table.Column<int>(type: "int", nullable: true),
                    ProveedorId = table.Column<int>(type: "int", nullable: true),
                    Bulto = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CantidadBultos = table.Column<int>(type: "int", nullable: true),
                    CantidadXBultos = table.Column<int>(type: "int", nullable: true),
                    PrecioBulto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PVP = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    CuentaId = table.Column<int>(type: "int", nullable: true),
                    FechaDePago = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Pagado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodigoDeVenta = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<byte[]>(type: "longblob", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    VentaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventaDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ventaDetalles_cuentas_CuentaId",
                        column: x => x.CuentaId,
                        principalTable: "cuentas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ventaDetalles_especificaciones_SPECId",
                        column: x => x.SPECId,
                        principalTable: "especificaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ventaDetalles_marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "marcas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ventaDetalles_proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "proveedores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ventaDetalles_tipoProductos_TipoProductoId",
                        column: x => x.TipoProductoId,
                        principalTable: "tipoProductos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ventaDetalles_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ventaDetalles_ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "ventas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "codigoDeVentas",
                columns: new[] { "Id", "Codigo" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "especificaciones",
                columns: new[] { "Id", "Modificacion", "Nombre", "Visible" },
                values: new object[,]
                {
                    { 1, null, "2lt", true },
                    { 2, null, "750ml", true },
                    { 3, null, "390g", true }
                });

            migrationBuilder.InsertData(
                table: "localidades",
                columns: new[] { "Id", "Modificacion", "Nombre", "Visible" },
                values: new object[,]
                {
                    { 1, null, "San Justo", true },
                    { 2, null, "Marcelino Escalada", true },
                    { 3, null, "Llambi Cámpbell", true }
                });

            migrationBuilder.InsertData(
                table: "marcas",
                columns: new[] { "Id", "Modificacion", "Nombre", "Visible" },
                values: new object[,]
                {
                    { 1, null, "Coca-Cola", true },
                    { 2, null, "Toro", true },
                    { 3, null, "Bagley", true }
                });

            migrationBuilder.InsertData(
                table: "porcentaje",
                columns: new[] { "Id", "Porcentaje" },
                values: new object[,]
                {
                    { 1, 30m },
                    { 2, 8m }
                });

            migrationBuilder.InsertData(
                table: "provincias",
                columns: new[] { "Id", "Modificacion", "Nombre", "Visible" },
                values: new object[,]
                {
                    { 1, null, "Santa Fe", true },
                    { 2, null, "Buenos Aires", true },
                    { 3, null, "Córdoba", true },
                    { 4, null, "Mendoza", true }
                });

            migrationBuilder.InsertData(
                table: "tipoProductos",
                columns: new[] { "Id", "Modificacion", "Nombre", "Visible" },
                values: new object[,]
                {
                    { 1, null, "Gaseosas", true },
                    { 2, null, "Vinos", true },
                    { 3, null, "Masitas", true }
                });

            migrationBuilder.InsertData(
                table: "tiposDeUsuarios",
                columns: new[] { "Id", "Nombre", "Visible" },
                values: new object[,]
                {
                    { 1, "SuperUsuario", true },
                    { 2, "Admin", true },
                    { 3, "Empleado", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cuentas_LocalidadId",
                table: "cuentas",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_cuentas_ProvinciaId",
                table: "cuentas",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_cuentas_UsuarioId",
                table: "cuentas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_CuentaId",
                table: "pagos",
                column: "CuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_UsuarioId",
                table: "pagos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_MarcaId",
                table: "productos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_ProveedorId",
                table: "productos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_SPECId",
                table: "productos",
                column: "SPECId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_TipoProductoId",
                table: "productos",
                column: "TipoProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_UsuarioId",
                table: "productos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_LocalidadId",
                table: "proveedores",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_ProvinciaId",
                table: "proveedores",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_proveedores_UsuarioId",
                table: "proveedores",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_remitoDetalles_MarcaId",
                table: "remitoDetalles",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_remitoDetalles_RemitoId",
                table: "remitoDetalles",
                column: "RemitoId");

            migrationBuilder.CreateIndex(
                name: "IX_remitoDetalles_SPECId",
                table: "remitoDetalles",
                column: "SPECId");

            migrationBuilder.CreateIndex(
                name: "IX_remitoDetalles_TipoProductoId",
                table: "remitoDetalles",
                column: "TipoProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_remitos_ProveedorId",
                table: "remitos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_remitos_UsuarioId",
                table: "remitos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_TipoDeUsuarioId",
                table: "usuarios",
                column: "TipoDeUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalles_CuentaId",
                table: "ventaDetalles",
                column: "CuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalles_MarcaId",
                table: "ventaDetalles",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalles_ProveedorId",
                table: "ventaDetalles",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalles_SPECId",
                table: "ventaDetalles",
                column: "SPECId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalles_TipoProductoId",
                table: "ventaDetalles",
                column: "TipoProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalles_UsuarioId",
                table: "ventaDetalles",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalles_VentaId",
                table: "ventaDetalles",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_ventas_CuentaId",
                table: "ventas",
                column: "CuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_ventas_PagoId",
                table: "ventas",
                column: "PagoId");

            migrationBuilder.CreateIndex(
                name: "IX_ventas_UsuarioId",
                table: "ventas",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "codigoDeVentas");

            migrationBuilder.DropTable(
                name: "porcentaje");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "remitoDetalles");

            migrationBuilder.DropTable(
                name: "ventaDetalles");

            migrationBuilder.DropTable(
                name: "remitos");

            migrationBuilder.DropTable(
                name: "especificaciones");

            migrationBuilder.DropTable(
                name: "marcas");

            migrationBuilder.DropTable(
                name: "tipoProductos");

            migrationBuilder.DropTable(
                name: "ventas");

            migrationBuilder.DropTable(
                name: "proveedores");

            migrationBuilder.DropTable(
                name: "pagos");

            migrationBuilder.DropTable(
                name: "cuentas");

            migrationBuilder.DropTable(
                name: "localidades");

            migrationBuilder.DropTable(
                name: "provincias");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "tiposDeUsuarios");
        }
    }
}
