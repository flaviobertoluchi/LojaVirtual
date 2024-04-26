using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Colaboradores.Models.DTOs
{
    public class ColaboradorDTO
    {
        public int Id { get; set; }

        [StringLength(10), MinLength(3)]
        public string Usuario { get; set; } = string.Empty;

        [StringLength(64), MinLength(64)]
        public string Senha { get; set; } = string.Empty;

        [MaxLength(25)]
        public string? Nome { get; set; } = string.Empty;

        [MaxLength(25)]
        public string? Sobrenome { get; set; } = string.Empty;
    }
}
