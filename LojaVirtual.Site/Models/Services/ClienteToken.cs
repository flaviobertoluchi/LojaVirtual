namespace LojaVirtual.Site.Models.Services
{
    public class ClienteToken
    {
        public string BearerToken { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public string ClienteUsuario { get; set; } = string.Empty;
        public DateTime Validade { get; set; }
        public string? RefreshToken { get; set; }
    }
}
