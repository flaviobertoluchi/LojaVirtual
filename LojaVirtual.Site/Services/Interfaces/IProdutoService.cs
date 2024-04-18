using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina);
    }
}
