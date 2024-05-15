namespace LojaVirtual.Site.Areas.Administracao.Models.Services
{
    public class ColaboradorToken
    {
        public string BearerToken { get; set; } = string.Empty;
        public int ColaboradorId { get; set; }
        public string ColaboradorUsuario { get; set; } = string.Empty;
        public Permissao Permissao { get; set; } = new();
        public DateTime Validade { get; set; }
        public string? RefreshToken { get; set; }
    }
}
