using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.WebApp.Models.Services
{
    public class Email
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string EmailEndereco { get; set; } = string.Empty;

        public Cliente? Cliente { get; set; }
    }
}
