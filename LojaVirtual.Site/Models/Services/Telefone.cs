using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.WebApp.Models.Services
{
    public class Telefone
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }

        [MaxLength(15)]
        public string Numero { get; set; } = string.Empty;

        public Cliente? Cliente { get; set; }
    }
}
