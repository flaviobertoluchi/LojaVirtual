using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Clientes.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [StringLength(10), MinLength(3)]
        public string Usuario { get; set; } = string.Empty;

        [StringLength(64), MinLength(64)]
        public string Senha { get; set; } = string.Empty;

        [StringLength(25), MinLength(3)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(25), MinLength(3)]
        public string Sobrenome { get; set; } = string.Empty;

        [StringLength(14)]
        public string Cpf { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;

        public Token? Token { get; set; }
        public ICollection<Email>? Emails { get; set; }
        public ICollection<Telefone>? Telefones { get; set; }
        public ICollection<Endereco>? Enderecos { get; set; }
    }
}
