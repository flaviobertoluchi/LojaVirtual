using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Clientes.Models
{
    public class Email
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string EmailEndereco { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;

        public Cliente? Cliente { get; set; }
    }
}
