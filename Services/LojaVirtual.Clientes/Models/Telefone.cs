using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Clientes.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }

        [MaxLength(15)]
        public string Numero { get; set; } = string.Empty;

        public Cliente? Cliente { get; set; }
    }
}
