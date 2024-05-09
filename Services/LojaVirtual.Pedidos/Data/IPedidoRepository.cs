using LojaVirtual.Pedidos.Models;

namespace LojaVirtual.Pedidos.Data
{
    public interface IPedidoRepository
    {
        Task<int> QuantidadePedidosCliente(int clienteId);
        Task<Paginacao<Pedido>> ObterPaginado(int pagina, int qtdPorPagina, int clienteId);
        Task<Pedido?> Obter(int id);
        Task Adicionar(Pedido pedido);
    }
}
