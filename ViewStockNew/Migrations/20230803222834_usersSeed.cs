using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewStockNew.Migrations
{
    public partial class usersSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "Id", "Genero", "Imagen", "Modificacion", "Nombre", "Password", "TipoDeUsuarioId", "User", "Visible" },
                values: new object[,]
                {
                    { 1, "Masculino", null, null, "Empleado", "0f7bfe6859999fd0ee6e4a7b725d466cebebec7ca75ddd7ef0f2e6d648db6d8f", 3, "empleado", true },
                    { 2, "Masculino", null, null, "Santiago Barreto", "0f7bfe6859999fd0ee6e4a7b725d466cebebec7ca75ddd7ef0f2e6d648db6d8f", 1, "superadmin", true },
                    { 3, "Femenino", null, null, "Encargado", "0f7bfe6859999fd0ee6e4a7b725d466cebebec7ca75ddd7ef0f2e6d648db6d8f", 2, "encargado", true },
                    { 4, "Masculino", null, null, "WebStockUser", "0f7bfe6859999fd0ee6e4a7b725d466cebebec7ca75ddd7ef0f2e6d648db6d8f", 3, "superadmin", true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
