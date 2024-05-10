using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Pedidos.Models.Tipos
{
    public enum TipoOrdemPedidos
    {
        Id,
        [Display(Name = "Quantidade de itens")]
        QuantidadeItens,
        [Display(Name = "Valor total")]
        ValorTotal,
        [Display(Name = "Data da compra")]
        DataCompra
    }
}
