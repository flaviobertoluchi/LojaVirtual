using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Pedidos.Models.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }

        [StringLength(25), MinLength(3)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(25), MinLength(3)]
        public string Sobrenome { get; set; } = string.Empty;

        [StringLength(14)]
        public string Cpf { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(15)]
        public string Telefone { get; set; } = string.Empty;

        [StringLength(25, MinimumLength = 3)]
        public string EnderecoNome { get; set; } = string.Empty;

        [StringLength(9)]
        public string Cep { get; set; } = string.Empty;

        [StringLength(150, MinimumLength = 3)]
        public string Logradouro { get; set; } = string.Empty;

        [MaxLength(10)]
        public string EnderecoNumero { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Complemento { get; set; }

        [StringLength(25, MinimumLength = 3)]
        public string Cidade { get; set; } = string.Empty;

        [StringLength(25, MinimumLength = 3)]
        public string Bairro { get; set; } = string.Empty;

        [StringLength(2)]
        public string Uf { get; set; } = string.Empty;
    }
}
