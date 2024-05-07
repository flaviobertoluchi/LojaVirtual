using LojaVirtual.Site.Models.Tipos;

namespace LojaVirtual.Site.Models.Services
{
    public class Pedido
    {
        public int Id { get; set; }
        public int QuantidadeItens { get; set; }
        public decimal ValorTotal { get; set; }
        public ICollection<PedidoItem> PedidoItens { get; set; } = [];
        public ICollection<SituacaoPedido> SituacoesPedido { get; set; } = [];
        public PedidoCliente Cliente { get; set; } = new();
        public TipoPagamento TipoPagamento { get; set; }
    }
}
