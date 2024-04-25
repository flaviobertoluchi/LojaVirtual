using LojaVirtual.Catalogo.Models;
using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Produtos.Data
{
    public class ProdutoRepository(SqlServerContext context) : IProdutoRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<Paginacao<Produto>> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, long categoriaId = 0, bool incluirImagens = false, bool semEstoque = false)
        {
            var query = context.Produtos.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Nome.Contains(pesquisa));

            query = ordem switch
            {
                TipoOrdemProdutos.MenorPreco => query.OrderBy(x => x.Preco).ThenByDescending(x => x.Id),
                TipoOrdemProdutos.MaiorPreco => query.OrderByDescending(x => x.Preco).ThenByDescending(x => x.Id),
                _ => query.OrderByDescending(x => x.Id),
            };

            if (categoriaId > 0) query = query.Where(x => x.CategoriaId == categoriaId);
            if (incluirImagens) query = query.Include(x => x.Imagens);
            if (!semEstoque) query = query.Where(x => x.Estoque > 0);

            var totalItens = await query.LongCountAsync();
            var totalPaginas = (totalItens + qtdPorPagina - 1) / qtdPorPagina;

            return new Paginacao<Produto>()
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

        public async Task<Produto?> Obter(long id)
        {
            return await context.Produtos.AsNoTracking().Include(x => x.Imagens).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Adicionar(Produto produto)
        {
            context.Add(produto);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Produto produto)
        {
            context.Update(produto);
            await context.SaveChangesAsync();
        }

        public async Task Excluir(Produto produto)
        {
            context.Remove(produto);
            await context.SaveChangesAsync();
        }
    }
}
