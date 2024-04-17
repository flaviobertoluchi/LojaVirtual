using LojaVirtual.ClienteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ClienteAPI.Data
{
    public class ClienteRepository(SqlServerContext context) : IClienteRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<Cliente?> Obter(long id, bool incluirEmails = false, bool incluirTelefones = false, bool incluirEnderecos = false, bool incluirToken = false)
        {
            var query = context.Clientes.AsQueryable();

            if (incluirEmails) query = query.Include(x => x.Emails);
            if (incluirTelefones) query = query.Include(x => x.Telefones);
            if (incluirEnderecos) query = query.Include(x => x.Enderecos);
            if (incluirToken) query = query.Include(x => x.Token);

            return await query.AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cliente?> ObterPorUsuarioESenha(string usuario, string senha, bool incluirToken = false)
        {
            var query = context.Clientes.AsQueryable();

            if (incluirToken) query = query.Include(x => x.Token);

            return await query.AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Usuario == usuario && x.Senha == senha);
        }

        public async Task<Cliente?> ObterPorRefreshToken(string refreshToken)
        {
            return await context.Clientes.AsNoTracking().Include(x => x.Token).Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Token != null && x.Token.RefreshToken == refreshToken);
        }

        public async Task Adicionar(Cliente cliente)
        {
            context.Add(cliente);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Cliente cliente)
        {
            context.Update(cliente);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UsuarioExiste(string usuario)
        {
            return await context.Clientes.AnyAsync(x => x.Usuario == usuario);
        }

        public async Task<bool> CpfExiste(string cpf)
        {
            return await context.Clientes.AnyAsync(x => x.Cpf == cpf);
        }
    }
}
