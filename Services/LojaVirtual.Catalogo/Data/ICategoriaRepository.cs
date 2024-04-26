using LojaVirtual.Catalogo.Models;
using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Models;

namespace LojaVirtual.Produtos.Data
{
    public interface ICategoriaRepository
    {
        Task<ICollection<Categoria>> ObterTodos();
        Task<Paginacao<Categoria>> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemCategorias ordem, bool desc, string pesquisa);
        Task<Categoria?> Obter(int id);
        Task<Categoria?> ObterPorNome(string nome);
        Task Adicionar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Excluir(Categoria categoria);
    }
}
