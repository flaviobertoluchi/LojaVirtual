using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.WebApp.Models
{
    public class UsuarioViewModel
    {
        [Required]
        [StringLength(10), MinLength(3)]
        public string Login { get; set; } = string.Empty;


        [Required]
        [StringLength(20), MinLength(3)]
        public string Senha { get; set; } = string.Empty;
    }
}
