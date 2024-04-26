using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Produtos.Models
{
    public class Imagem
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }

        [MaxLength(500)]
        public string Local { get; set; } = string.Empty;

        public Produto? Produto { get; set; }
    }
}
