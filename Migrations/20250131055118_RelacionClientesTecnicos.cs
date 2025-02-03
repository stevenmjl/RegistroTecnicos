using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class RelacionClientesTecnicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TecnicoId",
                table: "Clientes",
                column: "TecnicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Tecnicos_TecnicoId",
                table: "Clientes",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Tecnicos_TecnicoId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_TecnicoId",
                table: "Clientes");
        }
    }
}
