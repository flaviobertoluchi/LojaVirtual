using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models.Tipos
{
    public enum TipoOrdemProdutos
    {
        Id,
        Categoria,
        Nome,
        Estoque,
        [Display(Name = "Preço")]
        Preco
    }
}
