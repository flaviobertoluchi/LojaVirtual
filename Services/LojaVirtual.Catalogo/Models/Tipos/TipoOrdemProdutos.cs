using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Catalogo.Models.Tipos
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
