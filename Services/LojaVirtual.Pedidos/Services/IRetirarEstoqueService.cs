using LojaVirtual.Pedidos.Models.Services;

namespace LojaVirtual.Pedidos.Services
{
    public interface IRetirarEstoqueService
    {
        Task RetirarEstoque(ICollection<RetirarEstoque> retirarEstoques);
    }
}
