using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Colaboradores.Models
{
    public class Colaborador
    {
        public int Id { get; set; }

        [StringLength(10), MinLength(3)]
        public string Usuario { get; set; } = string.Empty;

        [StringLength(64), MinLength(64)]
        public string Senha { get; set; } = string.Empty;

        [MaxLength(25)]
        public string? Nome { get; set; }

        [MaxLength(25)]
        public string? Sobrenome { get; set; }

        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;

        public Token? Token { get; set; }
        public Permissao? Permissao { get; set; }
    }
}
