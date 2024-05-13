using LojaVirtual.Colaboradores.Models.Tipos;
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
                    Tipo = table.Column<int>(type: "int", nullable: false)
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
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TokensHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    ColaboradorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TokensHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    BearerToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TokensHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TokensHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    RefreshToken = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TokensHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TokensHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "TokensHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
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
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "TokensHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.InsertData(
                table: "Colaboradores",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "Nome", "Senha", "Sobrenome", "Usuario" },
                values: new object[] { 1, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "f360ca3fef5aa0422ee9c2489a09bcb28efeeb751150ab6c2a08ca37a419cd46", null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_Usuario",
                table: "Colaboradores",
                column: "Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissoes_ColaboradorId",
                table: "Permissoes",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ColaboradorId",
                table: "Tokens",
                column: "ColaboradorId",
                unique: true);

            migrationBuilder.InsertData(
                table: "Permissoes",
                columns: ["Id", "ColaboradorId", "Tipo"],
                values: new object[,]
                {
                    { 1, 1, (int)TipoPermissaoColaborador.VisualizarColaborador },
                    { 2, 1, (int)TipoPermissaoColaborador.AdicionarColaborador },
                    { 3, 1, (int)TipoPermissaoColaborador.EditarColaborador },
                    { 4, 1, (int)TipoPermissaoColaborador.ExcluirColaborador },
                    { 5, 1, (int)TipoPermissaoColaborador.VisualizarCliente },
                    { 6, 1, (int)TipoPermissaoColaborador.VisualizarCategoria },
                    { 7, 1, (int)TipoPermissaoColaborador.AdicionarCategoria },
                    { 8, 1, (int)TipoPermissaoColaborador.EditarCategoria },
                    { 9, 1, (int)TipoPermissaoColaborador.ExcluirCategoria },
                    { 10, 1, (int)TipoPermissaoColaborador.VisualizarProduto },
                    { 11, 1, (int)TipoPermissaoColaborador.AdicionarProduto },
                    { 12, 1, (int)TipoPermissaoColaborador.EditarProduto },
                    { 13, 1, (int)TipoPermissaoColaborador.ExcluirProduto },
                    { 14, 1, (int)TipoPermissaoColaborador.VizualizarPedido },
                    { 15, 1, (int)TipoPermissaoColaborador.AdicionarSituacaoPedido }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissoes");

            migrationBuilder.DropTable(
                name: "Tokens")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "TokensHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropTable(
                name: "Colaboradores");
        }
    }
}
