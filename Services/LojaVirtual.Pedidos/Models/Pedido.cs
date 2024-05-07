using LojaVirtual.Pedidos.Models.Tipos;

namespace LojaVirtual.Pedidos.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int QuantidadeItens { get; set; }
        public decimal ValorTotal { get; set; }
        public ICollection<PedidoItem> PedidoItens { get; set; } = [];
        public ICollection<SituacaoPedido> SituacoesPedido { get; set; } = [];
        public Cliente Cliente { get; set; } = new();
        public TipoPagamento TipoPagamento { get; set; }
    }
}
