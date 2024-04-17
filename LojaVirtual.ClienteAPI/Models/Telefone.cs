using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.ClienteAPI.Models
{
    public class Telefone
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }

        [MaxLength(15)]
        public string Numero { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;

        public Cliente? Cliente { get; set; }
    }
}
