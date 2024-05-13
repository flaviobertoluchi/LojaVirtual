using LojaVirtual.Colaboradores.Models;
using LojaVirtual.Colaboradores.Models.Tipos;

namespace LojaVirtual.Colaboradores.Data
{
    public interface IColaboradorRepository
    {
        Task<Paginacao<Colaborador>> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemColaboradores ordem, bool desc, string pesquisa);
        Task<Colaborador?> Obter(int id);
        Task<Colaborador?> ObterPorUsuarioESenha(string usuario, string senha, bool incluirToken);
        Task<Colaborador?> ObterPorRefreshToken(string refreshToken);
        Task Adicionar(Colaborador colaborador);
        Task Atualizar(Colaborador colaborador);
        Task<bool> UsuarioExiste(string usuario);
    }
}
