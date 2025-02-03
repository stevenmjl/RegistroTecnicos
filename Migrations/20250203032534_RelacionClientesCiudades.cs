using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class RelacionClientesCiudades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CiudadId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CiudadId",
                table: "Clientes",
                column: "CiudadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Ciudades_CiudadId",
                table: "Clientes",
                column: "CiudadId",
                principalTable: "Ciudades",
                principalColumn: "CiudadId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Ciudades_CiudadId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_CiudadId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "Clientes");
        }
    }
}
