namespace LojaVirtual.Site.Models.Services
{
    public class Carrinho
    {
        public ICollection<CarrinhoItem> CarrinhoItens { get; set; } = [];
    }

    public class CarrinhoItem
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
