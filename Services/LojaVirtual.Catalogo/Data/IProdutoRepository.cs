using LojaVirtual.Catalogo.Models;
using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Models;

namespace LojaVirtual.Produtos.Data
{
    public interface IProdutoRepository
    {
        Task<Paginacao<Produto>> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemProdutos ordem, bool desc, string pesquisa, int categoriaId, bool semEstoque);
        Task<Paginacao<Produto>> ObterPaginadoSite(int pagina, int qtdPorPagina, string pesquisa, TipoOrdemProdutosSite ordem, int categoriaId, bool incluirImagens, bool semEstoque);
        Task<Produto?> Obter(int id, bool comTrack);
        Task<Produto?> ObterSite(int id);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Excluir(Produto produto);
    }
}
