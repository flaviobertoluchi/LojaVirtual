using LojaVirtual.Site.Models.Services;
using System.Text.Json;

namespace LojaVirtual.Site.Extensions
{
    public class Sessao(IHttpContextAccessor accessor)
    {
        private readonly IHttpContextAccessor accessor = accessor;
        private const string key = "clienteJwt";
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public ClienteToken? ObterClienteToken()
        {
            var sessao = accessor.HttpContext?.Session.GetString(key);
            if (sessao is null) return null;

            return JsonSerializer.Deserialize<ClienteToken>(sessao, options);
        }

        public void Adicionar(string clienteJwt)
        {
            accessor.HttpContext?.Session.SetString(key, clienteJwt);
        }
        public void Excluir()
        {
            accessor.HttpContext?.Session.Remove(key);
        }
    }
}
