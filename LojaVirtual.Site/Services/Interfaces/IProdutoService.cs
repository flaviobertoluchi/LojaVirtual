using LojaVirtual.WebApp.Models.Services;

namespace LojaVirtual.WebApp.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina);
    }
}
