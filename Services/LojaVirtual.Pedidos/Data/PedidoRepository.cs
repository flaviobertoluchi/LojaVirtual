using LojaVirtual.Pedidos.Models;
using LojaVirtual.Pedidos.Models.Tipos;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Pedidos.Data
{
    public class PedidoRepository(SqlServerContext context) : IPedidoRepository
    {
        private readonly SqlServerContext context = context;

        public async Task<Paginacao<Pedido>> ObterPaginadoAdministracao(int pagina, int qtdPorPagina, TipoOrdemPedidos ordem, bool desc, string pesquisa, string pesquisaCpf, DateTime? dataCompraInicial, DateTime? dataCompraFinal)
        {
            var query = context.Pedidos.AsNoTracking().Include(x => x.Cliente).Include(x => x.SituacoesPedido).AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa)) query = query.Where(x => x.Id.ToString().Contains(pesquisa));
            if (!string.IsNullOrEmpty(pesquisaCpf)) query = query.Where(x => x.Cliente.Cpf.Contains(pesquisaCpf));
            if (dataCompraInicial is not null) query = query.Where(x => x.SituacoesPedido.OrderBy(x => x.Id).FirstOrDefault()!.Data >= dataCompraInicial);
            if (dataCompraFinal is not null) query = query.Where(x => x.SituacoesPedido.OrderBy(x => x.Id).FirstOrDefault()!.Data < dataCompraFinal);

            if (desc)
            {
                query = ordem switch
                {
                    TipoOrdemPedidos.QuantidadeItens => query.OrderByDescending(x => x.QuantidadeItens).ThenByDescending(x => x.Id),
                    TipoOrdemPedidos.ValorTotal => query.OrderByDescending(x => x.ValorTotal).ThenByDescending(x => x.Id),
                    TipoOrdemPedidos.DataCompra => query.OrderByDescending(x => x.SituacoesPedido.OrderBy(x => x.Id).FirstOrDefault()!.Data).ThenByDescending(x => x.Id),
                    _ => query.OrderByDescending(x => x.Id),
                };
            }
            else
            {
                query = ordem switch
                {
                    TipoOrdemPedidos.QuantidadeItens => query.OrderBy(x => x.QuantidadeItens).ThenBy(x => x.Id),
                    TipoOrdemPedidos.ValorTotal => query.OrderBy(x => x.ValorTotal).ThenBy(x => x.Id),
                    TipoOrdemPedidos.DataCompra => query.OrderBy(x => x.SituacoesPedido.OrderBy(x => x.Id).FirstOrDefault()!.Data).ThenBy(x => x.Id),
                    _ => query.OrderBy(x => x.Id),
                };
            }

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

        public async Task<int> QuantidadePedidosCliente(int clienteId)
        {
            return await context.Pedidos.AsNoTracking().CountAsync(x => x.Cliente.ClienteId == clienteId);
        }

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

        public async Task<Pedido?> Obter(int id, bool comTrack)
        {
            var query = context.Pedidos.AsQueryable();

            if (!comTrack) query = query.AsNoTracking();

            return await query.Include(x => x.PedidoItens).Include(x => x.SituacoesPedido).Include(x => x.Cliente).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pedido?> ObterUltimo(int clienteId)
        {
            return await context.Pedidos.AsNoTracking().Include(x => x.PedidoItens).Include(x => x.SituacoesPedido).Include(x => x.Cliente).OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Cliente.Id == clienteId);
        }

        public async Task Adicionar(Pedido pedido)
        {
            context.Add(pedido);
            await context.SaveChangesAsync();
        }

        public async Task Atualizar(Pedido pedido)
        {
            context.Update(pedido);
            await context.SaveChangesAsync();
        }
    }
}
