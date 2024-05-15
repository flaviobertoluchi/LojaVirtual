using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models
{
    public class CarrinhoViewModel
    {
        public ICollection<CarrinhoItemViewModel> CarrinhoItens { get; set; } = [];
        public int QuantidadeItens { get; set; }
        public decimal ValorTotal { get; set; }
        public bool CarrinhoAlterado { get; set; }
    }

    public class CarrinhoItemViewModel
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public int Estoque { get; set; }

        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        public decimal Total { get; set; }
        public string Imagem { get; set; } = string.Empty;
    }
}
