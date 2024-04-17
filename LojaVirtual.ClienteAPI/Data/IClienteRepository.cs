using LojaVirtual.ClienteAPI.Models;

namespace LojaVirtual.ClienteAPI.Data
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObterPorUsuarioESenha(string usuario, string senha, bool incluirClienteToken = false);
        Task<Cliente?> ObterPorRefreshToken(string refreshToken);
        Task Atualizar(Cliente cliente);
    }
}
