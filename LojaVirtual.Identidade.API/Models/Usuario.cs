using LojaVirtual.Identidade.API.Models.Tipos;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Identidade.API.Models
{
    public class Usuario
    {
        public long Id { get; set; }

        [StringLength(10), MinLength(3)]
        public string Login { get; set; } = string.Empty;

        [StringLength(64), MinLength(64)]
        public string Senha { get; set; } = string.Empty;

        public TipoUsuario Tipo { get; set; } = TipoUsuario.Cliente;
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
