using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewStockNew.Migrations
{
    public partial class SeedNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "localidades",
                columns: new[] { "Id", "Modificacion", "Nombre", "Visible" },
                values: new object[] { 4, null, "null", true });

            migrationBuilder.InsertData(
                table: "provincias",
                columns: new[] { "Id", "Modificacion", "Nombre", "Visible" },
                values: new object[] { 5, null, "null", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "localidades",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "provincias",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
