using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewStockNew.Migrations
{
    public partial class VentaInVentaDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VentaId",
                table: "ventaDetalles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalles_VentaId",
                table: "ventaDetalles",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ventaDetalles_ventas_VentaId",
                table: "ventaDetalles",
                column: "VentaId",
                principalTable: "ventas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ventaDetalles_ventas_VentaId",
                table: "ventaDetalles");

            migrationBuilder.DropIndex(
                name: "IX_ventaDetalles_VentaId",
                table: "ventaDetalles");

            migrationBuilder.DropColumn(
                name: "VentaId",
                table: "ventaDetalles");
        }
    }
}
