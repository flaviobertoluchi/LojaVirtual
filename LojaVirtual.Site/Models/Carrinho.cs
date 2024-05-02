namespace LojaVirtual.Site.Models
{
    public class Carrinho
    {
        public ICollection<CarrinhoItem> CarrinhoItens { get; set; } = [];
        public int QuantidadeItens { get; set; }
    }

    public class CarrinhoItem
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
