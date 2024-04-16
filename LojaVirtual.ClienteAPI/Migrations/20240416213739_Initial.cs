using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaVirtual.ClienteAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.CheckConstraint("CK_Senha", "LEN(Senha) = 64");
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "Nome", "RefreshToken", "Senha", "Sobrenome", "Usuario" },
                values: new object[] { 1L, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", null, "f98616cf7e22979eb55bb8ccfc5fbaa01a31475f45823b734b8b4c92012b4c5c", "Admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Usuario",
                table: "Clientes",
                column: "Usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
