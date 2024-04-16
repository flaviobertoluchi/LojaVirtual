using LojaVirtual.Identidade.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Identidade.API.Data.Context
{
    public class IdentidadeDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(x => x.Login).IsUnique();
            modelBuilder.Entity<Usuario>().ToTable(x => x.HasCheckConstraint("CK_Senha", "LEN(Senha) = 64"));
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario()
                {
                    Id = 1,
                    Login = "admin",
                    Senha = "f98616cf7e22979eb55bb8ccfc5fbaa01a31475f45823b734b8b4c92012b4c5c",
                    Tipo = Models.Tipos.TipoUsuario.Funcionario,
                    DataCadastro = DateTime.MinValue,
                    DataAtualizacao = null,
                    Ativo = true
                });
        }
    }
}
