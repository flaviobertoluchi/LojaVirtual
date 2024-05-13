using LojaVirtual.Site.Areas.Administracao.Models.Tipos;
using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Areas.Administracao.Services.Interfaces
{
    public interface IClienteAdministracaoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemClientes ordem, bool desc, string pesquisa, string pesquisaCpf);
    }
}
