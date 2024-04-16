using LojaVirtual.WebApp.Config;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.WebApp.Models
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(10, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Login { get; set; } = string.Empty;


        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Senha { get; set; } = string.Empty;
    }
}
