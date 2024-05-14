using LojaVirtual.Catalogo.Models;
using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Produtos.Data
{
    public class ProdutoRepository(SqlServerContext context) : IProdutoRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<Paginacao<Produto>> ObterPaginadoAdministracao(int pagina, int qtdPorPagina, TipoOrdemProdutos ordem, bool desc, string pesquisa, int categoriaId, bool semEstoque)
        {
            var query = context.Produtos.AsNoTracking().Include(x => x.Categoria).AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Nome.Contains(pesquisa));

            if (desc)
            {
                query = ordem switch
                {
                    TipoOrdemProdutos.Nome => query.OrderByDescending(x => x.Nome).ThenByDescending(x => x.Id),
                    TipoOrdemProdutos.Categoria => query.OrderByDescending(x => x.Categoria!.Nome).ThenByDescending(x => x.Id),
                    TipoOrdemProdutos.Estoque => query.OrderByDescending(x => x.Estoque).ThenByDescending(x => x.Id),
                    TipoOrdemProdutos.Preco => query.OrderByDescending(x => x.Preco).ThenByDescending(x => x.Id),
                    _ => query.OrderByDescending(x => x.Id),
                };
            }
            else
            {
                query = ordem switch
                {
                    TipoOrdemProdutos.Nome => query.OrderBy(x => x.Nome).ThenBy(x => x.Id),
                    TipoOrdemProdutos.Categoria => query.OrderBy(x => x.Categoria!.Nome).ThenBy(x => x.Id),
                    TipoOrdemProdutos.Estoque => query.OrderBy(x => x.Estoque).ThenBy(x => x.Id),
                    TipoOrdemProdutos.Preco => query.OrderBy(x => x.Preco).ThenBy(x => x.Id),
                    _ => query.OrderBy(x => x.Id),
                };
            }

            if (categoriaId > 0) query = query.Where(x => x.CategoriaId == categoriaId);
            if (!semEstoque) query = query.Where(x => x.Estoque > 0);

            var totalItens = await query.CountAsync();
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

        public async Task<Paginacao<Produto>> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa, TipoOrdemProdutosSite ordem, int categoriaId, bool incluirImagens, bool semEstoque)
        {
            var query = context.Produtos.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Nome.Contains(pesquisa));

            query = ordem switch
            {
                TipoOrdemProdutosSite.MenorPreco => query.OrderBy(x => x.Preco).ThenByDescending(x => x.Id),
                TipoOrdemProdutosSite.MaiorPreco => query.OrderByDescending(x => x.Preco).ThenByDescending(x => x.Id),
                _ => query.OrderByDescending(x => x.Id),
            };

            if (categoriaId > 0) query = query.Where(x => x.CategoriaId == categoriaId);
            if (incluirImagens) query = query.Include(x => x.Imagens);
            if (!semEstoque) query = query.Where(x => x.Estoque > 0);

            var totalItens = await query.CountAsync();
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

        public async Task<Produto?> ObterAdministracao(int id, bool comTrack)
        {
            var query = context.Produtos.AsQueryable();

            if (!comTrack) query = query.AsNoTracking();

            return await query.Include(x => x.Imagens).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Produto?> Obter(int id, bool semEstoque)
        {
            var query = context.Produtos.AsNoTracking().AsQueryable();

            if (!semEstoque) query = query.Where(x => x.Estoque > 0);

            return await query.Include(x => x.Imagens).FirstOrDefaultAsync(x => x.Id == id);
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
