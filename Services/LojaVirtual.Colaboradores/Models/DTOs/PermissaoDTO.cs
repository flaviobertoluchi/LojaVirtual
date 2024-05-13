using LojaVirtual.Colaboradores.Models.Tipos;

namespace LojaVirtual.Colaboradores.Models.DTOs
{
    public class PermissaoDTO
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public TipoPermissaoColaborador Tipo { get; set; }
    }
}
