using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Produtos.Models.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }

        [StringLength(15, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Descricao { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public decimal Preco { get; set; }

        public ICollection<string> Imagens { get; set; } = [];
    }
}
