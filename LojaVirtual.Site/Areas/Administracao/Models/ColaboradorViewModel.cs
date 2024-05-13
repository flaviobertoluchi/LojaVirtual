using LojaVirtual.Site.Areas.Administracao.Models.Services;
using LojaVirtual.Site.Config;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Areas.Administracao.Models
{
    public class ColaboradorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(10, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [Compare(nameof(Senha), ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.ConfirmarSenha))]
        [DisplayName("Confirmar senha")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        [MaxLength(25, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string? Nome { get; set; } = string.Empty;

        [MaxLength(25, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string? Sobrenome { get; set; } = string.Empty;
        public ICollection<Permissao>? Permissoes { get; set; }
    }
}
