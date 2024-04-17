using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.WebApp.Models.Services
{
    public class Cliente
    {
        public long Id { get; set; }

        [StringLength(10), MinLength(3)]
        public string Usuario { get; set; } = string.Empty;

        [StringLength(20), MinLength(3)]
        public string Senha { get; set; } = string.Empty;

        [StringLength(25), MinLength(3)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(25), MinLength(3)]
        public string Sobrenome { get; set; } = string.Empty;

        [StringLength(14)]
        public string Cpf { get; set; } = string.Empty;

        public ICollection<Email>? Emails { get; set; }
        public ICollection<Telefone>? Telefones { get; set; }
        public ICollection<Endereco>? Enderecos { get; set; }
    }
}
