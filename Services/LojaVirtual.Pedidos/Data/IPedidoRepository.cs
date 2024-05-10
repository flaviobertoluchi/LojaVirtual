using LojaVirtual.Pedidos.Models;
using LojaVirtual.Pedidos.Models.Tipos;

namespace LojaVirtual.Pedidos.Data
{
    public interface IPedidoRepository
    {
        Task<Paginacao<Pedido>> ObterPaginadoAdministracao(int pagina, int qtdPorPagina, TipoOrdemPedidos ordem, bool desc, string pesquisa, string pesquisaCpf, DateTime? dataCompraInicial, DateTime? dataCompraFinal);
        Task<int> QuantidadePedidosCliente(int clienteId);
        Task<Paginacao<Pedido>> ObterPaginado(int pagina, int qtdPorPagina, int clienteId);
        Task<Pedido?> Obter(int id);
        Task<Pedido?> ObterUltimo();
        Task Adicionar(Pedido pedido);
    }
}
