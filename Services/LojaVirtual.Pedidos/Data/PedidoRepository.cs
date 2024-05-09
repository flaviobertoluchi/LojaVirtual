using LojaVirtual.Pedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Pedidos.Data
{
    public class PedidoRepository(SqlServerContext context) : IPedidoRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<Paginacao<Pedido>> ObterPaginado(int pagina, int qtdPorPagina, int clienteId)
        {
            var query = context.Pedidos.AsNoTracking().Include(x => x.SituacoesPedido).Where(x => x.Cliente.ClienteId == clienteId).OrderByDescending(x => x.Id).AsQueryable();

            var totalItens = await query.CountAsync();
            var totalPaginas = (totalItens + qtdPorPagina - 1) / qtdPorPagina;

            return new Paginacao<Pedido>()
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

        public async Task<Pedido?> Obter(int id)
        {
            return await context.Pedidos.AsNoTracking().Include(x => x.PedidoItens).Include(x => x.SituacoesPedido).Include(x => x.Cliente).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Adicionar(Pedido pedido)
        {
            context.Add(pedido);
            await context.SaveChangesAsync();
        }
    }
}
