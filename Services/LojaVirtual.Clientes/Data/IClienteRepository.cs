using LojaVirtual.Clientes.Models;

namespace LojaVirtual.Clientes.Data
{
    public interface IClienteRepository
    {
        Task<Cliente?> Obter(long id, bool incluirEmails = false, bool incluirTelefones = false, bool incluirEnderecos = false, bool incluirToken = false);
        Task<Cliente?> ObterPorUsuarioESenha(string usuario, string senha, bool incluirToken = false);
        Task<Cliente?> ObterPorRefreshToken(string refreshToken);
        Task Adicionar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task<bool> UsuarioExiste(string usuario);
        Task<bool> CpfExiste(string cpf);
    }
}
