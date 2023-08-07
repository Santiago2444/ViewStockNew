using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewStockNew.Migrations
{
    public partial class cuentasSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cuentas",
                columns: new[] { "Id", "DNI", "Deuda", "Domicilio", "Email", "Imagen", "LocalidadId", "Modificacion", "Nombre", "ProvinciaId", "Saldo", "Telefono", "TelefonoDos", "UsuarioId", "Visible" },
                values: new object[] { 1, "00000", null, null, null, null, null, null, "Consumidor Final", null, null, null, null, null, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cuentas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
