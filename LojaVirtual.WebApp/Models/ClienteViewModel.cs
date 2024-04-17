using LojaVirtual.WebApp.Config;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.WebApp.Models
{
    public class ClienteViewModel
    {
        public long Id { get; set; }

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

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(25, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(25, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        public string Sobrenome { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MinLength(14, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MinLength))]
        [MaxLength(14, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("CPF")]
        public string Cpf { get; set; } = string.Empty;

        // Contato

        [EmailAddress(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.EmailAddress))]
        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("E-mail")]
        public string EmailEndereco { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.Required))]
        [MaxLength(15, ErrorMessageResourceType = typeof(Mensagens), ErrorMessageResourceName = nameof(Mensagens.MaxLength))]
        [DisplayName("Telefone")]
        public string TelefoneNumero { get; set; } = string.Empty;


        // Endereço

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
        public string EnderecoNumero { get; set; } = string.Empty;

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
