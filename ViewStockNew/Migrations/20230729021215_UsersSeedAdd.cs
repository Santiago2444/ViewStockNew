using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewStockNew.Migrations
{
    public partial class UsersSeedAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Genero", "Imagen", "Modificacion", "Nombre", "Password", "TipoDeUsuarioId", "User", "Visible" },
                values: new object[] { 1, "Masculino", null, null, "Empleado", "0f7bfe6859999fd0ee6e4a7b725d466cebebec7ca75ddd7ef0f2e6d648db6d8f", 3, "empleado", true });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Genero", "Imagen", "Modificacion", "Nombre", "Password", "TipoDeUsuarioId", "User", "Visible" },
                values: new object[] { 2, "Masculino", null, null, "Santiago Barreto", "0f7bfe6859999fd0ee6e4a7b725d466cebebec7ca75ddd7ef0f2e6d648db6d8f", 1, "superadmin", true });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Genero", "Imagen", "Modificacion", "Nombre", "Password", "TipoDeUsuarioId", "User", "Visible" },
                values: new object[] { 3, "Femenino", null, null, "Encargado", "0f7bfe6859999fd0ee6e4a7b725d466cebebec7ca75ddd7ef0f2e6d648db6d8f", 2, "encargado", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
