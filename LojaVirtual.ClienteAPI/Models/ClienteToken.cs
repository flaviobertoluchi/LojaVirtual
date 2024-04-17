using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.ClienteAPI.Models
{
    public class ClienteToken
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }
        public string BearerToken { get; set; } = string.Empty;
        public DateTime Validade { get; set; }

        [StringLength(64), MinLength(64)]
        public string? RefreshToken { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
