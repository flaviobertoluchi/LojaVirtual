using LojaVirtual.Site.Config;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models
{
    public class EmailViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.EmailAddress))]
        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("E-mail")]
        public string EmailEndereco { get; set; } = string.Empty;
    }
}
