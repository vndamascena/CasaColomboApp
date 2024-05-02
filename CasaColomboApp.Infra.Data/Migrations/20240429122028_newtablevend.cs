using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaColomboApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class newtablevend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HISTORICOVENDA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LOTEID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NUMEROLOTE = table.Column<int>(type: "int", nullable: false),
                    USUARIOID = table.Column<int>(type: "int", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    DATAVENDA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoteId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HISTORICOVENDA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HISTORICOVENDA_LOTE_LOTEID",
                        column: x => x.LOTEID,
                        principalTable: "LOTE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HISTORICOVENDA_LOTE_LoteId1",
                        column: x => x.LoteId1,
                        principalTable: "LOTE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICOVENDA_LOTEID",
                table: "HISTORICOVENDA",
                column: "LOTEID");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICOVENDA_LoteId1",
                table: "HISTORICOVENDA",
                column: "LoteId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HISTORICOVENDA");
        }
    }
}
