using LojaVirtual.Identidade.API.Models;

namespace LojaVirtual.Identidade.API.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<ICollection<Usuario>> ObterTodos();
        Task<Usuario?> ObterPorId(long id);
        Task<Usuario?> ObterPorLogin(string login);
        Task<Usuario?> ObterPorLoginESenha(string login, string senha);
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
    }
}
