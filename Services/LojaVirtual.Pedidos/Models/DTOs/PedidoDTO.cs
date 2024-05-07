using LojaVirtual.Pedidos.Models.Tipos;

namespace LojaVirtual.Pedidos.Models.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public int QuantidadeItens { get; set; }
        public decimal ValorTotal { get; set; }
        public ICollection<PedidoItemDTO> PedidoItens { get; set; } = [];
        public ICollection<SituacaoPedidoDTO> SituacoesPedido { get; set; } = [];
        public ClienteDTO Cliente { get; set; } = new();
        public TipoPagamento TipoPagamento { get; set; }
    }
}
