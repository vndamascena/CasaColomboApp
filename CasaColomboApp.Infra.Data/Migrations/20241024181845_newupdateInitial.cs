using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaColomboApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class newupdateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MARCA",
                table: "PRODUTODEPOSITO",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MARCA",
                table: "PRODUTODEPOSITO");
        }
    }
}
