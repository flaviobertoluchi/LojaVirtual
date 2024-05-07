using LojaVirtual.Pedidos.Models;

namespace LojaVirtual.Pedidos.Data
{
    public interface IPedidoRepository
    {
        Task Adicionar(Pedido pedido);
    }
}
