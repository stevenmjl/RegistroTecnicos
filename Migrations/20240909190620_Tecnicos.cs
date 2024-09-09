using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class Tecnicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tecnico",
                table: "Tecnico");

            migrationBuilder.RenameTable(
                name: "Tecnico",
                newName: "Tecnicos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tecnicos",
                table: "Tecnicos",
                column: "TecnicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tecnicos",
                table: "Tecnicos");

            migrationBuilder.RenameTable(
                name: "Tecnicos",
                newName: "Tecnico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tecnico",
                table: "Tecnico",
                column: "TecnicoId");
        }
    }
}
