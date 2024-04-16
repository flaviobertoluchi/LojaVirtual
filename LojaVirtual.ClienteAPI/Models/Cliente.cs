using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.ClienteAPI.Models
{
    public class Cliente
    {
        public long Id { get; set; }

        [StringLength(10), MinLength(3)]
        public string Usuario { get; set; } = string.Empty;

        [StringLength(64), MinLength(64)]
        public string Senha { get; set; } = string.Empty;

        [StringLength(64), MinLength(64)]
        public string? RefreshToken { get; set; }

        [StringLength(25), MinLength(3)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(25), MinLength(3)]
        public string Sobrenome { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
