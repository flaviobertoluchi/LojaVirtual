using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models.Tipos
{
    public enum TipoSituacaoPedido
    {
        [Display(Name = "Aguardando pagamento")]
        Recebido,
        Aprovado,
        Enviado,
        Entregue,
        Finalizado,
        Cancelado
    }
}
