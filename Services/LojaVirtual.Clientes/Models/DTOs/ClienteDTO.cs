using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Clientes.Models.DTOs
{
    public class ClienteDTO
    {
        public long Id { get; set; }

        [StringLength(10), MinLength(3)]
        public string Usuario { get; set; } = string.Empty;

        [StringLength(20), MinLength(3)]
        public string Senha { get; set; } = string.Empty;

        [StringLength(25), MinLength(3)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(25), MinLength(3)]
        public string Sobrenome { get; set; } = string.Empty;

        [StringLength(14)]
        public string Cpf { get; set; } = string.Empty;

        public ICollection<EmailDTO>? Emails { get; set; }
        public ICollection<TelefoneDTO>? Telefones { get; set; }
        public ICollection<EnderecoDTO>? Enderecos { get; set; }
    }
}
