using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;

namespace LojaVirtual.Site.Areas.Administracao.Services.Interfaces
{
    public interface ICategoriaAdministracaoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemCategorias ordem, bool desc, string pesquisa);
        Task<ResponseApi> Obter(int id);
        Task<ResponseApi> Adicionar(CategoriaViewModel model);
        Task<ResponseApi> Atualizar(int id, CategoriaViewModel model);
        Task<ResponseApi> Excluir(int id);
    }
}
