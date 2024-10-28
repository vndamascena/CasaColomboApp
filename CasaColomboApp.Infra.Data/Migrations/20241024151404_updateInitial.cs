using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaColomboApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "USUARIOID",
                table: "PRODUTODEPOSITO",
                newName: "USUARIOIDALTERACAO");

            migrationBuilder.AlterColumn<string>(
                name: "USUARIOIDALTERACAO",
                table: "PRODUTODEPOSITO",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "USUARIOIDALTERACAO",
                table: "PRODUTODEPOSITO",
                newName: "USUARIOID");

            migrationBuilder.AlterColumn<string>(
                name: "USUARIOID",
                table: "PRODUTODEPOSITO",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
