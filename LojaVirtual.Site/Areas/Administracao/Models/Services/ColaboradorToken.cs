﻿namespace LojaVirtual.Site.Areas.Administracao.Models.Services
{
    public class ColaboradorToken
    {
        public string BearerToken { get; set; } = string.Empty;
        public long ColaboradorId { get; set; }
        public string ColaboradorUsuario { get; set; } = string.Empty;
        public DateTime Validade { get; set; }
        public string? RefreshToken { get; set; }
    }
}