using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewStockNew.Migrations
{
    public partial class proveedoresSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "proveedores",
                columns: new[] { "Id", "Direccion", "Email", "Imagen", "LocalidadId", "Modificacion", "Nombre", "ProvinciaId", "Telefono", "UsuarioId", "Visible" },
                values: new object[] { 1, "Sammy 5213", null, null, 1, null, "FiumbFast", 1, "349862422", 1, true });

            migrationBuilder.InsertData(
                table: "proveedores",
                columns: new[] { "Id", "Direccion", "Email", "Imagen", "LocalidadId", "Modificacion", "Nombre", "ProvinciaId", "Telefono", "UsuarioId", "Visible" },
                values: new object[] { 2, "Aataualpa", null, null, 2, null, "El Loco Repartos", 1, "342567432", 1, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "proveedores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "proveedores",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
