using LojaVirtual.Site.Config;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Models
{
    public class EnderecoViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(25, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("Nome do endereço")]
        public string EnderecoNome { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(9, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(9, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("CEP")]
        public string Cep { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(150, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MaxLength(10, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("Número")]
        public string Numero { get; set; } = string.Empty;

        [MaxLength(150, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string? Complemento { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(25, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(25, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(2, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(2, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("UF")]
        public string Uf { get; set; } = string.Empty;
    }
}
