using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;

namespace LojaVirtual.Site.Areas.Administracao.Services.Interfaces
{
    public interface IProdutoAdministracaoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemProdutos ordem, bool desc, string pesquisa, int categoriaId, bool semEstoque);
        Task<ResponseApi> Obter(int id);
        Task<ResponseApi> Adicionar(ProdutoViewModel model);
        Task<ResponseApi> Atualizar(int id, ProdutoViewModel model);
        Task<ResponseApi> Excluir(int id);
    }
}
