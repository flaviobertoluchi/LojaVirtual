using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Produtos.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }

        [StringLength(15, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Descricao { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public decimal Preco { get; set; }

        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public Categoria? Categoria { get; set; }
        public ICollection<Imagem> Imagens { get; set; } = [];
    }
}
