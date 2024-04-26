using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IClienteService
    {
        Task<ResponseApi> Obter(int id);
        Task<ResponseApi> Entrar(string login, string senha);
        Task<ResponseApi> EntrarPorRefreshToken(string refreshToken);
        Task<ResponseApi> Adicionar(ClienteViewModel model);
        Task Sair();
    }
}
