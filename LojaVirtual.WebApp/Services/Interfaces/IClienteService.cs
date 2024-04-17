using LojaVirtual.WebApp.Models;
using LojaVirtual.WebApp.Models.Services;

namespace LojaVirtual.WebApp.Services.Interfaces
{
    public interface IClienteService
    {
        Task<ResponseApi> Obter(long id);
        Task<ResponseApi> Entrar(string login, string senha);
        Task<ResponseApi> EntrarPorRefreshToken(string refreshToken);
        Task<ResponseApi> Adicionar(ClienteViewModel model);
        Task Sair();
    }
}
