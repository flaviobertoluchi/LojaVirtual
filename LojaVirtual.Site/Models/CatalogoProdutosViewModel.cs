using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Models
{
    public class CatalogoProdutosViewModel
    {
        public ICollection<Produto> Produtos { get; set; } = [];
        public long TotalItens { get; set; }
        public int QtdPorPagina { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int PaginaAnterior { get; set; }
        public int PaginaProxima { get; set; }
    }
}
