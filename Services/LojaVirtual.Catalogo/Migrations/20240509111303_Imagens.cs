using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaVirtual.Catalogo.Migrations
{
    /// <inheritdoc />
    public partial class Imagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagem_Produtos_ProdutoId",
                table: "Imagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Imagem",
                table: "Imagem");

            migrationBuilder.RenameTable(
                name: "Imagem",
                newName: "Imagens");

            migrationBuilder.RenameIndex(
                name: "IX_Imagem_ProdutoId",
                table: "Imagens",
                newName: "IX_Imagens_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Imagens",
                table: "Imagens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Imagens_Produtos_ProdutoId",
                table: "Imagens",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagens_Produtos_ProdutoId",
                table: "Imagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Imagens",
                table: "Imagens");

            migrationBuilder.RenameTable(
                name: "Imagens",
                newName: "Imagem");

            migrationBuilder.RenameIndex(
                name: "IX_Imagens_ProdutoId",
                table: "Imagem",
                newName: "IX_Imagem_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Imagem",
                table: "Imagem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Imagem_Produtos_ProdutoId",
                table: "Imagem",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
