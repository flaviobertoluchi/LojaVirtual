using LojaVirtual.Pedidos.Models.Services;

namespace LojaVirtual.Pedidos.Services
{
    public interface IEstoqueService
    {
        Task AlterarEstoque(ICollection<Estoque> estoques);
    }
}
