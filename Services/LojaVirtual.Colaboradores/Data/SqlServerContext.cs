using LojaVirtual.Colaboradores.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Colaboradores.Data
{
    public class SqlServerContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colaborador>().HasIndex(x => x.Usuario).IsUnique();
            modelBuilder.Entity<Colaborador>().ToTable(x => x.HasCheckConstraint("CK_Senha", "LEN(Senha) = 64"));
            modelBuilder.Entity<Colaborador>().HasData(new Colaborador()
            {
                Id = 1,
                Usuario = "admin",
                Senha = "f360ca3fef5aa0422ee9c2489a09bcb28efeeb751150ab6c2a08ca37a419cd46",
                DataCadastro = DateTime.MinValue
            });

            modelBuilder.Entity<Permissao>().HasData(new Permissao()
            {
                Id = 1,
                ColaboradorId = 1,
                VisualizarColaborador = true,
                AdicionarColaborador = true,
                EditarColaborador = true,
                ExcluirColaborador = true,
                VisualizarCliente = true,
                VisualizarCategoria = true,
                AdicionarCategoria = true,
                EditarCategoria = true,
                ExcluirCategoria = true,
                VisualizarProduto = true,
                AdicionarProduto = true,
                EditarProduto = true,
                ExcluirProduto = true,
                VisualizarPedido = true,
                AdicionarSituacaoPedido = true
            });

            modelBuilder.Entity<Token>().ToTable(x => x.IsTemporal());
        }
    }
}
