using LojaVirtual.Catalogo.Models;
using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Models;

namespace LojaVirtual.Produtos.Data
{
    public interface IProdutoRepository
    {
        Task<Paginacao<Produto>> ObterPaginadoAdministracao(int pagina, int qtdPorPagina, TipoOrdemProdutos ordem, bool desc, string pesquisa, int categoriaId, bool semEstoque);
        Task<Paginacao<Produto>> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa, TipoOrdemProdutosSite ordem, int categoriaId, bool incluirImagens, bool semEstoque);
        Task<Produto?> ObterAdministracao(int id, bool comTrack);
        Task<Produto?> Obter(int id);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Excluir(Produto produto);
    }
}
