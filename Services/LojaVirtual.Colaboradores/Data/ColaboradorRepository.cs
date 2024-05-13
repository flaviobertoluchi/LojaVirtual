using LojaVirtual.Colaboradores.Models;
using LojaVirtual.Colaboradores.Models.Tipos;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Colaboradores.Data
{
    public class ColaboradorRepository(SqlServerContext context) : IColaboradorRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<Paginacao<Colaborador>> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemColaboradores ordem, bool desc, string pesquisa)
        {
            var query = context.Colaboradores.AsNoTracking().Where(x => x.Ativo).AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Usuario.Contains(pesquisa));

            if (desc)
            {
                query = ordem switch
                {
                    TipoOrdemColaboradores.Usuário => query.OrderByDescending(x => x.Usuario).ThenByDescending(x => x.Id),
                    _ => query.OrderByDescending(x => x.Id),
                };
            }
            else
            {
                query = ordem switch
                {
                    TipoOrdemColaboradores.Usuário => query.OrderBy(x => x.Usuario).ThenBy(x => x.Id),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            var totalItens = await query.CountAsync();
            var totalPaginas = (totalItens + qtdPorPagina - 1) / qtdPorPagina;

            return new Paginacao<Colaborador>()
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

        public async Task<Colaborador?> Obter(int id)
        {
            return await context.Colaboradores.Include(x => x.Permissao).AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Colaborador?> ObterPorUsuarioESenha(string usuario, string senha, bool incluirToken)
        {
            var query = context.Colaboradores.AsQueryable();

            if (incluirToken) query = query.Include(x => x.Token);

            return await query.AsNoTracking().Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Usuario == usuario && x.Senha == senha);
        }

        public async Task<Colaborador?> ObterPorRefreshToken(string refreshToken)
        {
            return await context.Colaboradores.AsNoTracking().Include(x => x.Token).Where(x => x.Ativo).FirstOrDefaultAsync(x => x.Token != null && x.Token.RefreshToken == refreshToken);
        }
        public async Task Adicionar(Colaborador colaborador)
        {
            context.Add(colaborador);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Colaborador colaborador)
        {
            context.Update(colaborador);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UsuarioExiste(string usuario)
        {
            return await context.Colaboradores.AnyAsync(x => x.Usuario == usuario);
        }
    }
}
