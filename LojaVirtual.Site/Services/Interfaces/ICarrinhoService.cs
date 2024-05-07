using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface ICarrinhoService
    {
        Task<CarrinhoViewModel?> Obter();
        Carrinho ObterCookie();
        void Adicionar(CarrinhoItemViewModel model);
        void Atualizar(CarrinhoItemViewModel model);
        void Excluir(int id);
        void LimparCarrinho();
    }
}
