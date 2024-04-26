using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Clientes.Models
{
    public class Token
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string BearerToken { get; set; } = string.Empty;
        public DateTime Validade { get; set; }

        [StringLength(64), MinLength(64)]
        public string? RefreshToken { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
