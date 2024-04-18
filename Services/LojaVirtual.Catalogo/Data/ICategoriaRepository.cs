using LojaVirtual.Produtos.Models;

namespace LojaVirtual.Produtos.Data
{
    public interface ICategoriaRepository
    {
        Task<long> TotalItens();
        Task<ICollection<Categoria>> ObterPaginado(int pagina, int qtdPorPagina);
        Task<Categoria?> Obter(long id);
        Task<Categoria?> ObterPorNome(string nome);
        Task Adicionar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Excluir(Categoria categoria);
    }
}
