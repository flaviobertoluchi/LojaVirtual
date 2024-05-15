using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;

namespace LojaVirtual.Site.Areas.Administracao.Services.Interfaces
{
    public interface IPedidoAdministracaoService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemPedidos ordem, bool desc, string pesquisa, string pesquisaCpf, DateTime? dataCompraInicial, DateTime? dataCompraFinal);
        Task<ResponseApi> Obter(int id);
        Task<ResponseApi> AdicionarSituacao(SituacaoPedidoViewModel situacao);
    }
}
