using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IClienteService
    {
        Task<ResponseApi> Entrar(string login, string senha);
        Task<ResponseApi> EntrarPorRefreshToken(string refreshToken);
        Task<ResponseApi> Adicionar(ClienteAdicionarViewModel model);
        Task Sair();
        Task<ResponseApi> Obter();
        Task<ResponseApi> Atualizar(int id, ClienteViewModel model);
        Task<ResponseApi> Excluir(int id);

    }
}
