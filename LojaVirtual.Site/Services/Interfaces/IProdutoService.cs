using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, long categoriaId = 0);
    }
}
