using LojaVirtual.WebApp.Models.Services;
using System.Text.Json;

namespace LojaVirtual.WebApp.Libraries
{
    public class Sessao(IHttpContextAccessor accessor)
    {
        private readonly IHttpContextAccessor accessor = accessor;
        private const string key = "clienteJwt";
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public string? ObterNomeCliente()
        {
            var sessao = accessor.HttpContext?.Session.GetString(key);
            if (sessao is null) return null;

            var clienteJWT = JsonSerializer.Deserialize<ClienteJwt>(sessao, options);
            if (clienteJWT is null) return null;

            //TODO 
            return "Admin";
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
