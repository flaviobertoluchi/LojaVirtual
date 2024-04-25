using LojaVirtual.Colaboradores.Models;

namespace LojaVirtual.Colaboradores.Data
{
    public interface IColaboradorRepository
    {
        Task<Colaborador?> ObterPorUsuarioESenha(string usuario, string senha, bool incluirToken = false);
        Task<Colaborador?> ObterPorRefreshToken(string refreshToken);
        Task Atualizar(Colaborador colaborador);
    }
}
