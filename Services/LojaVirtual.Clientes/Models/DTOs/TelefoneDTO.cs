using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Clientes.Models.DTOs
{
    public class TelefoneDTO
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }

        [MaxLength(15)]
        public string Numero { get; set; } = string.Empty;
    }
}
