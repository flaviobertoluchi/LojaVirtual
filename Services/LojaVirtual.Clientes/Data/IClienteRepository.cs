using LojaVirtual.Clientes.Models;

namespace LojaVirtual.Clientes.Data
{
    public interface IClienteRepository
    {
        Task<Cliente?> Obter(int id, bool incluirEmails, bool incluirTelefones, bool incluirEnderecos, bool incluirToken);
        Task<Cliente?> ObterPorUsuarioESenha(string usuario, string senha, bool incluirToken);
        Task<Cliente?> ObterPorRefreshToken(string refreshToken);
        Task Adicionar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task<bool> UsuarioExiste(string usuario);
        Task<bool> CpfExiste(string cpf);
    }
}
