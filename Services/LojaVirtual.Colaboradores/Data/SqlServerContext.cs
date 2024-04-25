using LojaVirtual.Colaboradores.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Colaboradores.Data
{
    public class SqlServerContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colaborador>().HasIndex(x => x.Usuario).IsUnique();
            modelBuilder.Entity<Colaborador>().ToTable(x => x.HasCheckConstraint("CK_Senha", "LEN(Senha) = 64"));
            modelBuilder.Entity<Colaborador>().HasData(new Colaborador()
            {
                Id = 1,
                Usuario = "admin",
                Senha = "ee5eec2a6355d4708e985fa8bc9e7b0f161fa825b106de4e899534049e4553de",
                DataCadastro = DateTime.MinValue
            });

            modelBuilder.Entity<Token>().ToTable(x => x.IsTemporal());
        }
    }
}
