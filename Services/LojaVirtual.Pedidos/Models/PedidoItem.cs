using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Pedidos.Models
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }

        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        public decimal Total { get; set; }

        [MaxLength(500)]
        public string Imagem { get; set; } = string.Empty;

        public Pedido? Pedido { get; set; }
    }
}
