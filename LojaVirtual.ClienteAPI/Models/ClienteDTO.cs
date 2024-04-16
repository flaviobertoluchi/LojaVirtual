using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.ClienteAPI.Models
{
    public class ClienteDTO
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
    }
}
