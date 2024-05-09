using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<ResponseApi> QuantidadePedidosCliente();
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina);
        Task<ResponseApi> Obter(int id);
        Task<ResponseApi> ObterUltimo();
        Task<ResponseApi> Adicionar(Pedido pedido);
    }
}
