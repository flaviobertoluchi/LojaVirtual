using LojaVirtual.Colaboradores.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Colaboradores.Data
{
    public class ColaboradorRepository(SqlServerContext context) : IColaboradorRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<Colaborador?> ObterPorUsuarioESenha(string usuario, string senha, bool incluirToken = false)
        {
            var query = context.Colaboradores.AsQueryable();

            if (incluirToken) query = query.Include(x => x.Token);

            return await query.AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Usuario == usuario && x.Senha == senha);
        }

        public async Task<Colaborador?> ObterPorRefreshToken(string refreshToken)
        {
            return await context.Colaboradores.AsNoTracking().Include(x => x.Token).Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Token != null && x.Token.RefreshToken == refreshToken);
        }
        public async Task Atualizar(Colaborador colaborador)
        {
            context.Update(colaborador);
            await context.SaveChangesAsync();
        }
    }
}
