using LojaVirtual.Colaboradores.Models.Tipos;

namespace LojaVirtual.Colaboradores.Models
{
    public class Permissao
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public TipoPermissaoColaborador Tipo { get; set; }
        public Colaborador? Colaborador { get; set; }
    }
}
