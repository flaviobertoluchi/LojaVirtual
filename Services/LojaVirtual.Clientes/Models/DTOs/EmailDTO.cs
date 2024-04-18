using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Clientes.Models.DTOs
{
    public class EmailDTO
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string EmailEndereco { get; set; } = string.Empty;
    }
}
