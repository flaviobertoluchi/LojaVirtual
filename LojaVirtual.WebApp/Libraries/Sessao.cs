using LojaVirtual.WebApp.Models.Services;
using System.Text.Json;

namespace LojaVirtual.WebApp.Libraries
{
    public class Sessao(IHttpContextAccessor accessor)
    {
        private readonly IHttpContextAccessor accessor = accessor;
        private const string key = "clienteJwt";
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public string? ObterUsuario()
        {
            var sessao = accessor.HttpContext?.Session.GetString(key);
            if (sessao is null) return null;

            var clienteToken = JsonSerializer.Deserialize<ClienteToken>(sessao, options);
            if (clienteToken is null) return null;

            return clienteToken.ClienteUsuario;
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
