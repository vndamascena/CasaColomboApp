using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaColomboApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class newtba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeProuto",
                table: "LOTE");

            migrationBuilder.AlterColumn<string>(
                name: "PEI",
                table: "PRODUTO",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOMEPRODUTO",
                table: "LOTE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOMEPRODUTO",
                table: "HISTORICOVENDA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TIPOOCORRENCIA ",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOOCORRENCIA ", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OCORRENCIA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODPRODUTO = table.Column<int>(type: "int", maxLength: 7, nullable: false),
                    PRODUTO = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FORNECEDO = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    NUMERONOTA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USUARIOID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIPOOCORRENCIA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCORRENCIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OCORRENCIA_TIPOOCORRENCIA _TIPOOCORRENCIA",
                        column: x => x.TIPOOCORRENCIA,
                        principalTable: "TIPOOCORRENCIA ",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BAIXAOCORRENCIA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODPRODUTO = table.Column<int>(type: "int", nullable: false),
                    TIPOOCORRENCIAID = table.Column<int>(type: "int", nullable: false),
                    PRODUTO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUMEROLOTE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUMERONOTA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USUARIOID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OcorrenciaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAIXAOCORRENCIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BAIXAOCORRENCIA_OCORRENCIA_OcorrenciaId",
                        column: x => x.OcorrenciaId,
                        principalTable: "OCORRENCIA",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BAIXAOCORRENCIA_OCORRENCIA_TIPOOCORRENCIAID",
                        column: x => x.TIPOOCORRENCIAID,
                        principalTable: "OCORRENCIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BAIXAOCORRENCIA_OcorrenciaId",
                table: "BAIXAOCORRENCIA",
                column: "OcorrenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_BAIXAOCORRENCIA_TIPOOCORRENCIAID",
                table: "BAIXAOCORRENCIA",
                column: "TIPOOCORRENCIAID");

            migrationBuilder.CreateIndex(
                name: "IX_OCORRENCIA_TIPOOCORRENCIA",
                table: "OCORRENCIA",
                column: "TIPOOCORRENCIA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BAIXAOCORRENCIA");

            migrationBuilder.DropTable(
                name: "OCORRENCIA");

            migrationBuilder.DropTable(
                name: "TIPOOCORRENCIA ");

            migrationBuilder.DropColumn(
                name: "NOMEPRODUTO",
                table: "LOTE");

            migrationBuilder.DropColumn(
                name: "NOMEPRODUTO",
                table: "HISTORICOVENDA");

            migrationBuilder.AlterColumn<string>(
                name: "PEI",
                table: "PRODUTO",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeProuto",
                table: "LOTE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
