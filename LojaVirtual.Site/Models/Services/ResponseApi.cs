using System.Net;

namespace LojaVirtual.Site.Models.Services
{
    public class ResponseApi(HttpStatusCode status, object? content = null)
    {
        public bool Ok() => (int)Status >= 200 && (int)Status < 300;
        public bool NotFound() => (int)Status == 404;
        public HttpStatusCode Status { get; } = status;
        public object? Content { get; } = content;
    }
}
