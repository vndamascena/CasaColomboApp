using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CasaColomboApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initialll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    DATAHORACADASTRO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DEPOSITO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NOMEDEPOSITO = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPOSITO", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FORNECEDOR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    DATAHORACADASTRO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDOR", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CODIGO = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    NOME = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    MARCA = table.Column<string>(type: "longtext", nullable: true),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    PEI = table.Column<string>(type: "longtext", nullable: true),
                    DESCRICAO = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    PECASCAIXA = table.Column<int>(type: "int", nullable: true),
                    MERTROQCAIXA = table.Column<string>(type: "longtext", nullable: true),
                    PRECOCAIXA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PRECOMETRO = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DATAHORACADASTRO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CATEGORIAID = table.Column<int>(type: "int", nullable: false),
                    FORNECEDORID = table.Column<int>(type: "int", nullable: false),
                    DEPOSITOID = table.Column<int>(type: "int", nullable: false),
                    IMAGEMURL = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTO_CATEGORIA_CATEGORIAID",
                        column: x => x.CATEGORIAID,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTO_DEPOSITO_DEPOSITOID",
                        column: x => x.DEPOSITOID,
                        principalTable: "DEPOSITO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTO_FORNECEDOR_FORNECEDORID",
                        column: x => x.FORNECEDORID,
                        principalTable: "FORNECEDOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LOTE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PRODUTOID = table.Column<int>(type: "int", nullable: false),
                    NUMEROLOTE = table.Column<int>(name: "NUMERO LOTE", type: "int", maxLength: 15, nullable: false),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    ALA = table.Column<string>(type: "longtext", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOTE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LOTE_PRODUTO_PRODUTOID",
                        column: x => x.PRODUTOID,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HISTORICOVENDA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LOTEID = table.Column<int>(type: "int", nullable: false),
                    NUMEROLOTE = table.Column<int>(type: "int", nullable: false),
                    USUARIOID = table.Column<string>(type: "longtext", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    DATAVENDA = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LoteId1 = table.Column<int>(type: "int", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIA_NOME",
                table: "CATEGORIA",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DEPOSITO_NOMEDEPOSITO",
                table: "DEPOSITO",
                column: "NOMEDEPOSITO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FORNECEDOR_CNPJ",
                table: "FORNECEDOR",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICOVENDA_LOTEID",
                table: "HISTORICOVENDA",
                column: "LOTEID");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICOVENDA_LoteId1",
                table: "HISTORICOVENDA",
                column: "LoteId1");

            migrationBuilder.CreateIndex(
                name: "IX_LOTE_PRODUTOID",
                table: "LOTE",
                column: "PRODUTOID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_CATEGORIAID",
                table: "PRODUTO",
                column: "CATEGORIAID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_DEPOSITOID",
                table: "PRODUTO",
                column: "DEPOSITOID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_FORNECEDORID",
                table: "PRODUTO",
                column: "FORNECEDORID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HISTORICOVENDA");

            migrationBuilder.DropTable(
                name: "LOTE");

            migrationBuilder.DropTable(
                name: "PRODUTO");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "DEPOSITO");

            migrationBuilder.DropTable(
                name: "FORNECEDOR");
        }
    }
}
