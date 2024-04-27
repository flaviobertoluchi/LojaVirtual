using LojaVirtual.Site.Config;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(25, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Nome { get; set; } = string.Empty;
    }
}
