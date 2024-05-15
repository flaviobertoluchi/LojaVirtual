using LojaVirtual.Site.Config;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models
{
    public class TelefoneViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MaxLength(15, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("Número")]
        public string Numero { get; set; } = string.Empty;
    }
}
