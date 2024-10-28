using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaColomboApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DATAHORACADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DEPOSITO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMEDEPOSITO = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPOSITO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DEPOSITOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DATAHORACADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPOSITOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FORNECEDOR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DATAHORACADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDOR", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FORNECEDORGERAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VENDEDOR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FORNECEDORPRODUTO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TIPO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TELVENDEDOR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TELFORNECEDOR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATAHORACADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDORGERAL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Loja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loja", x => x.Id);
                });

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
                name: "PRODUTOGERAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MARCA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: true),
                    Un = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CODIGOSISTEMA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMAGEMURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATAHORACADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDFORNECEDOR = table.Column<int>(type: "int", nullable: false),
                    IDCATEGORIA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTOGERAL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTOGERAL_CATEGORIA_IDCATEGORIA",
                        column: x => x.IDCATEGORIA,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTOGERAL_FORNECEDORGERAL_IDFORNECEDOR",
                        column: x => x.IDFORNECEDOR,
                        principalTable: "FORNECEDORGERAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTOPISO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MARCA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: true),
                    PEI = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DESCRICAO = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    PECASCAIXA = table.Column<int>(type: "int", nullable: true),
                    MERTROQCAIXA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PRECOCAIXA = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PRECOMETRO = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DATAHORACADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false),
                    CATEGORIAID = table.Column<int>(type: "int", nullable: false),
                    FORNECEDORID = table.Column<int>(type: "int", nullable: false),
                    DEPOSITOID = table.Column<int>(type: "int", nullable: false),
                    IMAGEMURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FornecedorId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTOPISO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTOPISO_CATEGORIA_CATEGORIAID",
                        column: x => x.CATEGORIAID,
                        principalTable: "CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTOPISO_DEPOSITO_DEPOSITOID",
                        column: x => x.DEPOSITOID,
                        principalTable: "DEPOSITO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTOPISO_FORNECEDORGERAL_FORNECEDORID",
                        column: x => x.FORNECEDORID,
                        principalTable: "FORNECEDORGERAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUTOPISO_FORNECEDOR_FornecedorId1",
                        column: x => x.FornecedorId1,
                        principalTable: "FORNECEDOR",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OCORRENCIA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODPRODUTO = table.Column<int>(type: "int", maxLength: 7, nullable: false),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false),
                    PRODUTO = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FORNECEDO = table.Column<int>(type: "int", maxLength: 70, nullable: false),
                    NUMERONOTA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USUARIOID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LOJA = table.Column<int>(type: "int", nullable: false),
                    TIPOOCORRENCIA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCORRENCIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OCORRENCIA_FORNECEDORGERAL_FORNECEDO",
                        column: x => x.FORNECEDO,
                        principalTable: "FORNECEDORGERAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OCORRENCIA_Loja_LOJA",
                        column: x => x.LOJA,
                        principalTable: "Loja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OCORRENCIA_TIPOOCORRENCIA _TIPOOCORRENCIA",
                        column: x => x.TIPOOCORRENCIA,
                        principalTable: "TIPOOCORRENCIA ",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTODEPOSITO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUTOGERALID = table.Column<int>(type: "int", nullable: false),
                    DEPOSITOIID = table.Column<int>(type: "int", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    NOMEDEPOSITO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CODIGOSISTEMA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NOMEPRODUTO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QTDENTRADA = table.Column<int>(type: "int", nullable: false),
                    USUARIOIDCADASTRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USUARIOID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DATAENTRADA = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTODEPOSITO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTODEPOSITO_DEPOSITOS_DEPOSITOIID",
                        column: x => x.DEPOSITOIID,
                        principalTable: "DEPOSITOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUTODEPOSITO_PRODUTOGERAL_PRODUTOGERALID",
                        column: x => x.PRODUTOGERALID,
                        principalTable: "PRODUTOGERAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LOTE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIOID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUTOID = table.Column<int>(type: "int", nullable: false),
                    CODIGO = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    NUMEROLOTE = table.Column<string>(name: "NUMERO LOTE", type: "nvarchar(10)", maxLength: 10, nullable: false),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false),
                    NOMEPRODUTO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ALA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATAHORAALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DATAENTRADA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MARCA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QTDENTRADA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOTE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LOTE_PRODUTOPISO_PRODUTOID",
                        column: x => x.PRODUTOID,
                        principalTable: "PRODUTOPISO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BAIXAOCORRENCIA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODPRODUTO = table.Column<int>(type: "int", nullable: false),
                    OCORRENCIAID = table.Column<int>(type: "int", nullable: false),
                    TIPOOCORRENCIAID = table.Column<int>(type: "int", nullable: false),
                    LojaId = table.Column<int>(type: "int", nullable: false),
                    PRODUTO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FORNECEDO = table.Column<int>(type: "int", nullable: false),
                    NUMERONOTA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USUARIOID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OcorrenciaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BAIXAOCORRENCIA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BAIXAOCORRENCIA_OCORRENCIA_OCORRENCIAID",
                        column: x => x.OCORRENCIAID,
                        principalTable: "OCORRENCIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BAIXAOCORRENCIA_OCORRENCIA_OcorrenciaId1",
                        column: x => x.OcorrenciaId1,
                        principalTable: "OCORRENCIA",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "VENDAPRODUTOGERAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIOID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MARCA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CODIGO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NOMEPRODUTO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    DATAVENDA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATAUPLOADVENDA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DATAVENDAMANUAL = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NOMEDEPOSITO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DEPOSITOID = table.Column<int>(type: "int", nullable: false),
                    ProdutoDepositoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENDAPRODUTOGERAL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VENDAPRODUTOGERAL_PRODUTODEPOSITO_DEPOSITOID",
                        column: x => x.DEPOSITOID,
                        principalTable: "PRODUTODEPOSITO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VENDAPRODUTOGERAL_PRODUTODEPOSITO_ProdutoDepositoId1",
                        column: x => x.ProdutoDepositoId1,
                        principalTable: "PRODUTODEPOSITO",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "HISTORICOVENDA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOTEID = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NOMEPRODUTO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUMEROLOTE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USUARIOID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MARCA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    DATAVENDA = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_BAIXAOCORRENCIA_OCORRENCIAID",
                table: "BAIXAOCORRENCIA",
                column: "OCORRENCIAID");

            migrationBuilder.CreateIndex(
                name: "IX_BAIXAOCORRENCIA_OcorrenciaId1",
                table: "BAIXAOCORRENCIA",
                column: "OcorrenciaId1");

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
                name: "IX_OCORRENCIA_FORNECEDO",
                table: "OCORRENCIA",
                column: "FORNECEDO");

            migrationBuilder.CreateIndex(
                name: "IX_OCORRENCIA_LOJA",
                table: "OCORRENCIA",
                column: "LOJA");

            migrationBuilder.CreateIndex(
                name: "IX_OCORRENCIA_TIPOOCORRENCIA",
                table: "OCORRENCIA",
                column: "TIPOOCORRENCIA");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTODEPOSITO_DEPOSITOIID",
                table: "PRODUTODEPOSITO",
                column: "DEPOSITOIID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTODEPOSITO_PRODUTOGERALID",
                table: "PRODUTODEPOSITO",
                column: "PRODUTOGERALID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOGERAL_IDCATEGORIA",
                table: "PRODUTOGERAL",
                column: "IDCATEGORIA");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOGERAL_IDFORNECEDOR",
                table: "PRODUTOGERAL",
                column: "IDFORNECEDOR");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOPISO_CATEGORIAID",
                table: "PRODUTOPISO",
                column: "CATEGORIAID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOPISO_DEPOSITOID",
                table: "PRODUTOPISO",
                column: "DEPOSITOID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOPISO_FORNECEDORID",
                table: "PRODUTOPISO",
                column: "FORNECEDORID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOPISO_FornecedorId1",
                table: "PRODUTOPISO",
                column: "FornecedorId1");

            migrationBuilder.CreateIndex(
                name: "IX_VENDAPRODUTOGERAL_DEPOSITOID",
                table: "VENDAPRODUTOGERAL",
                column: "DEPOSITOID");

            migrationBuilder.CreateIndex(
                name: "IX_VENDAPRODUTOGERAL_ProdutoDepositoId1",
                table: "VENDAPRODUTOGERAL",
                column: "ProdutoDepositoId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BAIXAOCORRENCIA");

            migrationBuilder.DropTable(
                name: "HISTORICOVENDA");

            migrationBuilder.DropTable(
                name: "VENDAPRODUTOGERAL");

            migrationBuilder.DropTable(
                name: "OCORRENCIA");

            migrationBuilder.DropTable(
                name: "LOTE");

            migrationBuilder.DropTable(
                name: "PRODUTODEPOSITO");

            migrationBuilder.DropTable(
                name: "Loja");

            migrationBuilder.DropTable(
                name: "TIPOOCORRENCIA ");

            migrationBuilder.DropTable(
                name: "PRODUTOPISO");

            migrationBuilder.DropTable(
                name: "DEPOSITOS");

            migrationBuilder.DropTable(
                name: "PRODUTOGERAL");

            migrationBuilder.DropTable(
                name: "DEPOSITO");

            migrationBuilder.DropTable(
                name: "FORNECEDOR");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "FORNECEDORGERAL");
        }
    }
}
