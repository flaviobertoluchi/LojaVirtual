using LojaVirtual.Clientes.Models;
using LojaVirtual.Clientes.Models.Tipos;

namespace LojaVirtual.Clientes.Data
{
    public interface IClienteRepository
    {
        Task<Paginacao<Cliente>> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemClientes ordem, bool desc, string pesquisa, string pesquisaCpf);
        Task<Cliente?> Obter(int id, bool incluirEmails, bool incluirTelefones, bool incluirEnderecos, bool incluirToken, bool comTrack);
        Task<Cliente?> ObterPorUsuarioESenha(string usuario, string senha, bool incluirToken);
        Task<Cliente?> ObterPorRefreshToken(string refreshToken);
        Task Adicionar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task<bool> UsuarioExiste(string usuario);
        Task<bool> CpfExiste(string cpf);
    }
}
