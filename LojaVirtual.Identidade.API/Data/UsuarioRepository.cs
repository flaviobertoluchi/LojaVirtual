using LojaVirtual.Identidade.API.Data.Context;
using LojaVirtual.Identidade.API.Data.Interfaces;
using LojaVirtual.Identidade.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Identidade.API.Data
{
    public class UsuarioRepository(IdentidadeDbContext context) : IUsuarioRepository
    {
        private readonly IdentidadeDbContext context = context;

        public async Task<ICollection<Usuario>> ObterTodos()
        {
            return await context.Usuarios.AsNoTracking().Where(x => x.Ativo).ToListAsync();
        }

        public async Task<Usuario?> ObterPorId(long id)
        {
            return await context.Usuarios.AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario?> ObterPorLogin(string login)
        {
            return await context.Usuarios.AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<Usuario?> ObterPorLoginESenha(string login, string senha)
        {
            return await context.Usuarios.AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Login == login && x.Senha == senha);
        }

        public async Task Adicionar(Usuario usuario)
        {
            context.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Usuario usuario)
        {
            context.Update(usuario);
            await context.SaveChangesAsync();
        }
    }
}
