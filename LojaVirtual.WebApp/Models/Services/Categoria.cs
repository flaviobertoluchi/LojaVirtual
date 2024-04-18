using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.WebApp.Models.Services
{
    public class Categoria
    {
        public long Id { get; set; }

        [StringLength(25, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;
    }
}
