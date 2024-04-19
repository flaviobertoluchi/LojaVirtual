using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Produtos.Data
{
    public class ProdutoRepository(SqlServerContext context) : IProdutoRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<long> TotalItens()
        {
            return await context.Produtos.LongCountAsync();
        }

        public async Task<ICollection<Produto>> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, bool incluirImagens = false)
        {
            var query = context.Produtos.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Nome.Contains(pesquisa));

            query = ordem switch
            {
                TipoOrdemProdutos.MenorPreco => query.OrderBy(x => x.Preco).ThenByDescending(x => x.Id),
                TipoOrdemProdutos.MaiorPreco => query.OrderByDescending(x => x.Preco).ThenByDescending(x => x.Id),
                _ => query.OrderByDescending(x => x.Id),
            };

            if (incluirImagens) query = query.Include(x => x.Imagens);

            return await query.Skip(qtdPorPagina * (pagina - 1)).Take(qtdPorPagina).ToListAsync();
        }

        public async Task<Produto?> Obter(long id)
        {
            return await context.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
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
