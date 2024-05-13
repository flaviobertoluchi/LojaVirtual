using LojaVirtual.Clientes.Models;
using LojaVirtual.Clientes.Models.Tipos;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Clientes.Data
{
    public class ClienteRepository(SqlServerContext context) : IClienteRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<Paginacao<Cliente>> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemClientes ordem, bool desc, string pesquisa, string pesquisaCpf)
        {
            var query = context.Clientes.AsNoTracking().Where(x => x.Ativo).AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Usuario.Contains(pesquisa));
            if (!string.IsNullOrEmpty(pesquisaCpf)) query = query.Where(x => x.Cpf.Contains(pesquisaCpf));

            if (desc)
            {
                query = ordem switch
                {
                    TipoOrdemClientes.Usuário => query.OrderByDescending(x => x.Usuario).ThenByDescending(x => x.Id),
                    TipoOrdemClientes.Nome => query.OrderByDescending(x => x.Nome).ThenByDescending(x => x.Id),
                    TipoOrdemClientes.Sobrenome => query.OrderByDescending(x => x.Sobrenome).ThenByDescending(x => x.Id),
                    _ => query.OrderByDescending(x => x.Id),
                };
            }
            else
            {
                query = ordem switch
                {
                    TipoOrdemClientes.Usuário => query.OrderBy(x => x.Usuario).ThenBy(x => x.Id),
                    TipoOrdemClientes.Nome => query.OrderBy(x => x.Nome).ThenBy(x => x.Id),
                    TipoOrdemClientes.Sobrenome => query.OrderBy(x => x.Sobrenome).ThenBy(x => x.Id),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            var totalItens = await query.CountAsync();
            var totalPaginas = (totalItens + qtdPorPagina - 1) / qtdPorPagina;

            return new Paginacao<Cliente>()
            {
                Data = await query.Skip(qtdPorPagina * (pagina - 1)).Take(qtdPorPagina).ToListAsync(),
                Info = new()
                {
                    TotalItens = totalItens,
                    TotalPaginas = totalPaginas,
                    QtdPorPagina = qtdPorPagina,
                    PaginaAtual = pagina,
                    PaginaAnterior = totalItens > 1 && pagina > 1 ? pagina - 1 : null,
                    PaginaProxima = pagina < totalPaginas ? pagina + 1 : null
                }
            };
        }

        public async Task<Cliente?> Obter(int id, bool incluirEmails, bool incluirTelefones, bool incluirEnderecos, bool incluirToken, bool comTrack)
        {
            var query = context.Clientes.AsQueryable();

            if (incluirEmails) query = query.Include(x => x.Emails);
            if (incluirTelefones) query = query.Include(x => x.Telefones);
            if (incluirEnderecos) query = query.Include(x => x.Enderecos);
            if (incluirToken) query = query.Include(x => x.Token);
            if (!comTrack) query = query.AsNoTracking();

            return await query.Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Id == id);
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
