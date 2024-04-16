using LojaVirtual.Identidade.API.Models.Tipos;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Identidade.API.Models.DTOs
{
    public class UsuarioDTO
    {
        public long Id { get; set; }

        [Required]
        [StringLength(10), MinLength(3)]
        public string Login { get; set; } = string.Empty;


        [Required]
        [StringLength(20), MinLength(3)]
        public string Senha { get; set; } = string.Empty;

        public TipoUsuario Tipo { get; set; } = TipoUsuario.Cliente;
    }
}
