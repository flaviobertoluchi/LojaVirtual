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

        public async Task<ICollection<Produto>> ObterPaginado(int pagina, int qtdPorPagina)
        {
            return await context.Produtos.AsNoTracking().Skip(qtdPorPagina * (pagina - 1)).Take(qtdPorPagina).ToListAsync();
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
