namespace LojaVirtual.Colaboradores.Models.DTOs
{
    public class TokenDTO
    {
        public string BearerToken { get; set; } = string.Empty;
        public int ColaboradorId { get; set; }
        public string ColaboradorUsuario { get; set; } = string.Empty;
        public Permissao Permissao { get; set; } = new();
        public DateTime Validade { get; set; }
        public string? RefreshToken { get; set; }
    }
}
