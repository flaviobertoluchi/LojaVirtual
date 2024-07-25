﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaVirtual.Colaboradores.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.Id);
                    table.CheckConstraint("CK_Senha", "LEN(Senha) = 64");
                });

            migrationBuilder.CreateTable(
                name: "Permissoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColaboradorId = table.Column<int>(type: "int", nullable: false),
                    VisualizarColaborador = table.Column<bool>(type: "bit", nullable: false),
                    AdicionarColaborador = table.Column<bool>(type: "bit", nullable: false),
                    EditarColaborador = table.Column<bool>(type: "bit", nullable: false),
                    ExcluirColaborador = table.Column<bool>(type: "bit", nullable: false),
                    VisualizarCliente = table.Column<bool>(type: "bit", nullable: false),
                    VisualizarCategoria = table.Column<bool>(type: "bit", nullable: false),
                    AdicionarCategoria = table.Column<bool>(type: "bit", nullable: false),
                    EditarCategoria = table.Column<bool>(type: "bit", nullable: false),
                    ExcluirCategoria = table.Column<bool>(type: "bit", nullable: false),
                    VisualizarProduto = table.Column<bool>(type: "bit", nullable: false),
                    AdicionarProduto = table.Column<bool>(type: "bit", nullable: false),
                    EditarProduto = table.Column<bool>(type: "bit", nullable: false),
                    ExcluirProduto = table.Column<bool>(type: "bit", nullable: false),
                    VisualizarPedido = table.Column<bool>(type: "bit", nullable: false),
                    AdicionarSituacaoPedido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissoes_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColaboradorId = table.Column<int>(type: "int", nullable: false),
                    BearerToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colaboradores",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "Nome", "Senha", "Sobrenome", "Usuario" },
                values: new object[] { 1, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ee5eec2a6355d4708e985fa8bc9e7b0f161fa825b106de4e899534049e4553de", null, "admin" });

            migrationBuilder.InsertData(
                table: "Permissoes",
                columns: new[] { "Id", "AdicionarCategoria", "AdicionarColaborador", "AdicionarProduto", "AdicionarSituacaoPedido", "ColaboradorId", "EditarCategoria", "EditarColaborador", "EditarProduto", "ExcluirCategoria", "ExcluirColaborador", "ExcluirProduto", "VisualizarCategoria", "VisualizarCliente", "VisualizarColaborador", "VisualizarPedido", "VisualizarProduto" },
                values: new object[] { 1, true, true, true, true, 1, true, true, true, true, true, true, true, true, true, true, true });

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_Usuario",
                table: "Colaboradores",
                column: "Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissoes_ColaboradorId",
                table: "Permissoes",
                column: "ColaboradorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ColaboradorId",
                table: "Tokens",
                column: "ColaboradorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissoes");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Colaboradores");
        }
    }
}