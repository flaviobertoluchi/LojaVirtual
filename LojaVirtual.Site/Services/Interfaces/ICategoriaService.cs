using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<ResponseApi> ObterTodos();
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemCategorias ordem, bool desc, string pesquisa);
        Task<ResponseApi> Obter(int id);
        Task<ResponseApi> Adicionar(Categoria categoria);
        Task<ResponseApi> Atualizar(int id, Categoria categoria);
        Task<ResponseApi> Excluir(int id);
    }
}
