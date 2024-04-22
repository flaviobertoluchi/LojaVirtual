using LojaVirtual.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Produtos.Data
{
    public class CategoriaRepository(SqlServerContext context) : ICategoriaRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<long> TotalItens()
        {
            return await context.Categorias.LongCountAsync();
        }

        public async Task<ICollection<Categoria>> ObterTodos()
        {
            return await context.Categorias.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<ICollection<Categoria>> ObterPaginado(int pagina, int qtdPorPagina)
        {
            return await context.Categorias.AsNoTracking().OrderBy(x => x.Nome).Skip(qtdPorPagina * (pagina - 1)).Take(qtdPorPagina).ToListAsync();
        }

        public async Task<Categoria?> Obter(long id)
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
