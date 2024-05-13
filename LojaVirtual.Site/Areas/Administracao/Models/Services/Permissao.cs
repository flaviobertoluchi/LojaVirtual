using LojaVirtual.Site.Areas.Administracao.Models.Tipos;

namespace LojaVirtual.Site.Areas.Administracao.Models.Services
{
    public class Permissao
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public TipoPermissaoColaborador Tipo { get; set; }
    }
}
