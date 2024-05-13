using LojaVirtual.Site.Areas.Administracao.Models;
using LojaVirtual.Site.Areas.Administracao.Models.Tipos;
using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Areas.Administracao.Services.Interfaces
{
    public interface IColaboradorService
    {
        Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemColaboradores ordem, bool desc, string pesquisa);
        Task<ResponseApi> Entrar(string login, string senha);
        Task<ResponseApi> EntrarPorRefreshToken(string refreshToken);
        Task Sair();
        Task<ResponseApi> Obter();
        Task<ResponseApi> Obter(int id);
        Task<ResponseApi> Adicionar(ColaboradorViewModel model);
        Task<ResponseApi> Atualizar(int id, ColaboradorViewModel model);
        Task<ResponseApi> Excluir(int id);
    }
}
