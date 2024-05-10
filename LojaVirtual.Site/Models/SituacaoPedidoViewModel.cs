using LojaVirtual.Site.Models.Tipos;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models
{
    public class SituacaoPedidoViewModel
    {
        public int PedidoId { get; set; }
        public TipoSituacaoPedido TipoSituacaoPedido { get; set; }

        [MaxLength(500)]
        public string Mensagem { get; set; } = string.Empty;
    }
}
