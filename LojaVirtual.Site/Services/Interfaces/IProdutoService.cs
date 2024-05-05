using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa, TipoOrdemProdutosSite ordem, int categoriaId);
        Task<ResponseApi> Obter(int id);
    }
}
