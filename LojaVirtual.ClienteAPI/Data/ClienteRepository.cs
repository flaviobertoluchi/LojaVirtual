using LojaVirtual.ClienteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ClienteAPI.Data
{
    public class ClienteRepository(SqlServerContext context) : IClienteRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<Cliente?> ObterPorUsuarioESenha(string usuario, string senha)
        {
            return await context.Clientes.AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Usuario == usuario && x.Senha == senha);
        }

        public async Task<Cliente?> ObterPorRefreshToken(string refreshToken)
        {
            return await context.Clientes.AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        }

        public async Task Atualizar(Cliente cliente)
        {
            context.Update(cliente);
            await context.SaveChangesAsync();
        }
    }
}
