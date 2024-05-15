using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models.Tipos
{
    public enum TipoPagamento
    {
        [Display(Name = "Não informado")]
        NaoInformado = -1,
        Pix,
        [Display(Name = "Boleto bancário")]
        Boleto,
        [Display(Name = "Cartão de débito")]
        CartaoDebito,
        [Display(Name = "Cartão de crédito")]
        CartaoCredito
    }
}
