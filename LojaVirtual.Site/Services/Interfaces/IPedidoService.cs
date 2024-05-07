using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<ResponseApi> Adicionar(Pedido pedido);
    }
}
