using LojaVirtual.Catalogo.Models;
using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Produtos.Data
{
    public class CategoriaRepository(SqlServerContext context) : ICategoriaRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<ICollection<Categoria>> ObterTodos()
        {
            return await context.Categorias.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<Paginacao<Categoria>> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemCategorias ordem, bool desc, string pesquisa)
        {
            var query = context.Categorias.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Nome.Contains(pesquisa));

            if (desc)
            {
                query = ordem switch
                {
                    TipoOrdemCategorias.Nome => query.OrderByDescending(x => x.Nome).ThenByDescending(x => x.Id),
                    _ => query.OrderByDescending(x => x.Id),
                };
            }
            else
            {
                query = ordem switch
                {
                    TipoOrdemCategorias.Nome => query.OrderBy(x => x.Nome).ThenBy(x => x.Id),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            var totalItens = await query.CountAsync();
            var totalPaginas = (totalItens + qtdPorPagina - 1) / qtdPorPagina;

            return new Paginacao<Categoria>()
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

        public async Task<Categoria?> Obter(int id)
        {
            return await context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Categoria?> ObterPorNome(string nome)
        {
            return await context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Nome == nome);
        }

        public async Task Adicionar(Categoria categoria)
        {
            context.Add(categoria);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Categoria categoria)
        {
            context.Update(categoria);
            await context.SaveChangesAsync();
        }

        public async Task Excluir(Categoria categoria)
        {
            context.Remove(categoria);
            await context.SaveChangesAsync();
        }
    }
}
