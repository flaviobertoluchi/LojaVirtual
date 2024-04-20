using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Models;

namespace LojaVirtual.Produtos.Data
{
    public interface IProdutoRepository
    {
        Task<long> TotalItens(string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, long categoriaId = 0, bool semEstoque = false);
        Task<ICollection<Produto>> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, long categoriaId = 0, bool incluirImagens = false, bool semEstoque = false);
        Task<Produto?> Obter(long id);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Excluir(Produto produto);
    }
}
