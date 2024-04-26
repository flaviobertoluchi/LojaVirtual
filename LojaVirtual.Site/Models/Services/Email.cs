using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models.Services
{
    public class Email
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string EmailEndereco { get; set; } = string.Empty;

        public Cliente? Cliente { get; set; }
    }
}
