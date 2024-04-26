using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Produtos.Models.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }

        [StringLength(25, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;
    }
}
