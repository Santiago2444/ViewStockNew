using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewStockNew.Migrations
{
    public partial class RemitoInRemitoDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_remitoDetalles_RemitoId",
                table: "remitoDetalles",
                column: "RemitoId");

            migrationBuilder.AddForeignKey(
                name: "FK_remitoDetalles_remitos_RemitoId",
                table: "remitoDetalles",
                column: "RemitoId",
                principalTable: "remitos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_remitoDetalles_remitos_RemitoId",
                table: "remitoDetalles");

            migrationBuilder.DropIndex(
                name: "IX_remitoDetalles_RemitoId",
                table: "remitoDetalles");
        }
    }
}
