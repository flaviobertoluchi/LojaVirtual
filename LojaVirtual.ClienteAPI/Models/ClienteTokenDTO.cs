namespace LojaVirtual.ClienteAPI.Models
{
    public class ClienteTokenDTO
    {
        public string BearerToken { get; set; } = string.Empty;
        public long ClienteId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;
        public DateTime Validade { get; set; }
        public string? RefreshToken { get; set; }
    }
}
