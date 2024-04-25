using LojaVirtual.Site.Areas.Administracao.Models.Services;
using LojaVirtual.Site.Models.Services;
using System.Text.Json;

namespace LojaVirtual.Site.Extensions
{
    public class Sessao(IHttpContextAccessor accessor)
    {
        private readonly IHttpContextAccessor accessor = accessor;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        public const string clienteKey = "cliente";
        public const string colaboradorKey = "colaborador";

        public ClienteToken? ObterClienteToken()
        {
            var sessao = accessor.HttpContext?.Session.GetString(clienteKey);
            if (sessao is null) return null;

            return JsonSerializer.Deserialize<ClienteToken>(sessao, options);
        }

        public ColaboradorToken? ObterColaboradorToken()
        {
            var sessao = accessor.HttpContext?.Session.GetString(colaboradorKey);
            if (sessao is null) return null;

            return JsonSerializer.Deserialize<ColaboradorToken>(sessao, options);
        }

        public void Adicionar(string key, string value)
        {
            accessor.HttpContext?.Session.SetString(key, value);
        }

        public void Excluir(string key)
        {
            accessor.HttpContext?.Session.Remove(key);
        }
    }
}
