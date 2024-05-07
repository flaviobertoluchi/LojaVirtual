using LojaVirtual.Pedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Pedidos.Data
{
    public class SqlServerContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>().Property(x => x.ValorTotal).HasPrecision(18, 2);
            modelBuilder.Entity<PedidoItem>().Property(x => x.Preco).HasPrecision(18, 2);
            modelBuilder.Entity<PedidoItem>().Property(x => x.Total).HasPrecision(18, 2);
        }
    }
}
