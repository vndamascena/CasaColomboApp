using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaColomboApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NOME",
                table: "DEPOSITO",
                newName: "NOMEDEPOSITO");

            migrationBuilder.RenameIndex(
                name: "IX_DEPOSITO_NOME",
                table: "DEPOSITO",
                newName: "IX_DEPOSITO_NOMEDEPOSITO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NOMEDEPOSITO",
                table: "DEPOSITO",
                newName: "NOME");

            migrationBuilder.RenameIndex(
                name: "IX_DEPOSITO_NOMEDEPOSITO",
                table: "DEPOSITO",
                newName: "IX_DEPOSITO_NOME");
        }
    }
}
