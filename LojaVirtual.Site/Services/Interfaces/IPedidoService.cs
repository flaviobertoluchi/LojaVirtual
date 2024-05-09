using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina);
        Task<ResponseApi> Obter(int id);
        Task<ResponseApi> Adicionar(Pedido pedido);
    }
}
