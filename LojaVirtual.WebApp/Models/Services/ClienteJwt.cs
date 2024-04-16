namespace LojaVirtual.WebApp.Models.Services
{
    public class ClienteJwt
    {
        public string Token { get; set; } = string.Empty;
        public long ClienteId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;
        public DateTime Validade { get; set; }
        public string? RefreshToken { get; set; }
    }
}
