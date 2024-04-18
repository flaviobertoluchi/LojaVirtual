namespace LojaVirtual.WebApp.Models.Services
{
    public class ClienteToken
    {
        public string BearerToken { get; set; } = string.Empty;
        public long ClienteId { get; set; }
        public string ClienteUsuario { get; set; } = string.Empty;
        public DateTime Validade { get; set; }
        public string? RefreshToken { get; set; }
    }
}
