using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemProdutos ordem, bool desc, string pesquisa);
        Task<ResponseApi> ObterPaginadoSite(int pagina, int qtdPorPagina, string pesquisa, TipoOrdemProdutosSite ordem, int categoriaId);
        Task<ResponseApi> ObterSite(int id);
        Task<ResponseApi> Obter(int id);
        Task<ResponseApi> Adicionar(ProdutoViewModel model);
        Task<ResponseApi> Atualizar(int id, ProdutoViewModel model);
        Task<ResponseApi> Excluir(int id);
    }
}
