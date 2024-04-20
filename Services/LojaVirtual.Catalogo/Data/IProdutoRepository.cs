using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Models;

namespace LojaVirtual.Produtos.Data
{
    public interface IProdutoRepository
    {
        Task<long> TotalItens(string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, bool semEstoque = false);
        Task<ICollection<Produto>> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, bool incluirImagens = false, bool semEstoque = false);
        Task<Produto?> Obter(long id);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Excluir(Produto produto);
    }
}
