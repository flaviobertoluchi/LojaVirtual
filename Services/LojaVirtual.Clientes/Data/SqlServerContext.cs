using LojaVirtual.Clientes.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Clientes.Data
{
    public class SqlServerContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasIndex(x => x.Usuario).IsUnique();
            modelBuilder.Entity<Cliente>().HasIndex(x => x.Cpf).IsUnique();
            modelBuilder.Entity<Cliente>().ToTable(x => x.HasCheckConstraint("CK_Senha", "LEN(Senha) = 64"));

            modelBuilder.Entity<Token>().ToTable(x => x.IsTemporal());
        }
    }
}
