using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaVirtual.Pedidos.Migrations
{
    /// <inheritdoc />
    public partial class ClietesSituacoesPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Pedidos_PedidoId",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_SituacaoPedido_Pedidos_PedidoId",
                table: "SituacaoPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SituacaoPedido",
                table: "SituacaoPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "SituacaoPedido",
                newName: "SituacoesPedido");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.RenameIndex(
                name: "IX_SituacaoPedido_PedidoId",
                table: "SituacoesPedido",
                newName: "IX_SituacoesPedido_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_PedidoId",
                table: "Clientes",
                newName: "IX_Clientes_PedidoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SituacoesPedido",
                table: "SituacoesPedido",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Pedidos_PedidoId",
                table: "Clientes",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SituacoesPedido_Pedidos_PedidoId",
                table: "SituacoesPedido",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Pedidos_PedidoId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_SituacoesPedido_Pedidos_PedidoId",
                table: "SituacoesPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SituacoesPedido",
                table: "SituacoesPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "SituacoesPedido",
                newName: "SituacaoPedido");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_SituacoesPedido_PedidoId",
                table: "SituacaoPedido",
                newName: "IX_SituacaoPedido_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Clientes_PedidoId",
                table: "Cliente",
                newName: "IX_Cliente_PedidoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SituacaoPedido",
                table: "SituacaoPedido",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Pedidos_PedidoId",
                table: "Cliente",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SituacaoPedido_Pedidos_PedidoId",
                table: "SituacaoPedido",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
