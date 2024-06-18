using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaColomboApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class upbaixaocorrencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BAIXAOCORRENCIA_OCORRENCIA_OcorrenciaId",
                table: "BAIXAOCORRENCIA");

            migrationBuilder.DropForeignKey(
                name: "FK_BAIXAOCORRENCIA_OCORRENCIA_TIPOOCORRENCIAID",
                table: "BAIXAOCORRENCIA");

            migrationBuilder.DropIndex(
                name: "IX_BAIXAOCORRENCIA_TIPOOCORRENCIAID",
                table: "BAIXAOCORRENCIA");

            migrationBuilder.RenameColumn(
                name: "OcorrenciaId",
                table: "BAIXAOCORRENCIA",
                newName: "OCORRENCIAID");

            migrationBuilder.RenameColumn(
                name: "NUMEROLOTE",
                table: "BAIXAOCORRENCIA",
                newName: "FORNECEDO");

            migrationBuilder.RenameIndex(
                name: "IX_BAIXAOCORRENCIA_OcorrenciaId",
                table: "BAIXAOCORRENCIA",
                newName: "IX_BAIXAOCORRENCIA_OCORRENCIAID");

            migrationBuilder.AlterColumn<int>(
                name: "OCORRENCIAID",
                table: "BAIXAOCORRENCIA",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BAIXAOCORRENCIA_OCORRENCIA_OCORRENCIAID",
                table: "BAIXAOCORRENCIA",
                column: "OCORRENCIAID",
                principalTable: "OCORRENCIA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BAIXAOCORRENCIA_OCORRENCIA_OCORRENCIAID",
                table: "BAIXAOCORRENCIA");

            migrationBuilder.RenameColumn(
                name: "OCORRENCIAID",
                table: "BAIXAOCORRENCIA",
                newName: "OcorrenciaId");

            migrationBuilder.RenameColumn(
                name: "FORNECEDO",
                table: "BAIXAOCORRENCIA",
                newName: "NUMEROLOTE");

            migrationBuilder.RenameIndex(
                name: "IX_BAIXAOCORRENCIA_OCORRENCIAID",
                table: "BAIXAOCORRENCIA",
                newName: "IX_BAIXAOCORRENCIA_OcorrenciaId");

            migrationBuilder.AlterColumn<int>(
                name: "OcorrenciaId",
                table: "BAIXAOCORRENCIA",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_BAIXAOCORRENCIA_TIPOOCORRENCIAID",
                table: "BAIXAOCORRENCIA",
                column: "TIPOOCORRENCIAID");

            migrationBuilder.AddForeignKey(
                name: "FK_BAIXAOCORRENCIA_OCORRENCIA_OcorrenciaId",
                table: "BAIXAOCORRENCIA",
                column: "OcorrenciaId",
                principalTable: "OCORRENCIA",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BAIXAOCORRENCIA_OCORRENCIA_TIPOOCORRENCIAID",
                table: "BAIXAOCORRENCIA",
                column: "TIPOOCORRENCIAID",
                principalTable: "OCORRENCIA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
