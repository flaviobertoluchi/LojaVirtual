using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Site.Areas.Administracao.Models.Services
{
    public class Colaborador
    {
        public int Id { get; set; }

        [StringLength(10), MinLength(3)]
        public string Usuario { get; set; } = string.Empty;

        [StringLength(20), MinLength(3)]
        public string Senha { get; set; } = string.Empty;

        [MaxLength(25)]
        public string? Nome { get; set; } = string.Empty;

        [MaxLength(25)]
        public string? Sobrenome { get; set; } = string.Empty;
        public ICollection<Permissao>? Permissoes { get; set; }
    }
}
