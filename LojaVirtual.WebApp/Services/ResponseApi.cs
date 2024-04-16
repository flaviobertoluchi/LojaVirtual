using System.Net;

namespace LojaVirtual.WebApp.Services
{
    public class ResponseApi(HttpStatusCode status, string? mensagem = null, object? content = null)
    {
        public bool Ok() => ((int)Status) >= 200 && ((int)Status) < 300;
        public HttpStatusCode Status { get; } = status;
        public string? Mensagem { get; } = mensagem;
        public object? Content { get; } = content;
    }
}
