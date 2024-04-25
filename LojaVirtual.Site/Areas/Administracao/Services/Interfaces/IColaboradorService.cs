using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Areas.Administracao.Services.Interfaces
{
    public interface IColaboradorService
    {
        Task<ResponseApi> Entrar(string login, string senha);
        Task<ResponseApi> EntrarPorRefreshToken(string refreshToken);
        Task Sair();
    }
}
