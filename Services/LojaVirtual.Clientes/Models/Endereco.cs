using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Clientes.Models
{
    public class Endereco
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }

        [StringLength(25, MinimumLength = 3)]
        public string EnderecoNome { get; set; } = string.Empty;

        [StringLength(9)]
        public string Cep { get; set; } = string.Empty;

        [StringLength(150, MinimumLength = 3)]
        public string Logradouro { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Numero { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Complemento { get; set; }

        [StringLength(25, MinimumLength = 3)]
        public string Cidade { get; set; } = string.Empty;

        [StringLength(25, MinimumLength = 3)]
        public string Bairro { get; set; } = string.Empty;

        [StringLength(2)]
        public string Uf { get; set; } = string.Empty;

        public Cliente? Cliente { get; set; }
    }
}
