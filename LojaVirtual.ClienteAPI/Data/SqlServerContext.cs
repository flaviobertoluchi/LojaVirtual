using LojaVirtual.ClienteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ClienteAPI.Data
{
    public class SqlServerContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteToken> ClienteTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasIndex(x => x.Usuario).IsUnique();
            modelBuilder.Entity<Cliente>().ToTable(x => x.HasCheckConstraint("CK_Senha", "LEN(Senha) = 64"));
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente()
                {
                    Id = 1,
                    Usuario = "admin",
                    Senha = "f98616cf7e22979eb55bb8ccfc5fbaa01a31475f45823b734b8b4c92012b4c5c",
                    Nome = "Admin",
                    Sobrenome = "Admin",
                    DataCadastro = DateTime.MinValue,
                    DataAtualizacao = null,
                    Ativo = true,
                });

            modelBuilder.Entity<ClienteToken>().ToTable(x => x.IsTemporal());
        }
    }
}
