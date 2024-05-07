using LojaVirtual.Pedidos.Models.Tipos;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Pedidos.Models
{
    public class SituacaoPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public TipoSituacaoPedido TipoSituacaoPedido { get; set; }
        public DateTime Data { get; set; }

        [MaxLength(500)]
        public string Mensagem { get; set; } = string.Empty;

        public Pedido? Pedido { get; set; }
    }
}
