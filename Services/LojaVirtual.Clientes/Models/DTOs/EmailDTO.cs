using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Clientes.Models.DTOs
{
    public class EmailDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string EmailEndereco { get; set; } = string.Empty;
    }
}
