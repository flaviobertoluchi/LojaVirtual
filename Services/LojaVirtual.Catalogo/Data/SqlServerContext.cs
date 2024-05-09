using LojaVirtual.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Produtos.Data
{
    public class SqlServerContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().Property(x => x.Preco).HasPrecision(18, 2);
            modelBuilder.Entity<Categoria>().HasIndex(x => x.Nome).IsUnique();
        }
    }
}
