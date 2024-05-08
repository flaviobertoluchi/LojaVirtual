using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Site.Services
{
    public class PedidoService(HttpClient httpClient, IConfiguration configuration, Sessao sessao) : IPedidoService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly Sessao sessao = sessao;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        private readonly string baseAddress = configuration.GetValue<string>("Services:Pedidos") ?? string.Empty;

        public async Task<ResponseApi> Adicionar(Pedido pedido)
        {
            //TODO - Retirar estoque dos produtos

            var clienteToken = sessao.ObterClienteToken();

            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", clienteToken?.BearerToken);

            var response = await httpClient.PostAsJsonAsync(baseAddress, pedido, options);

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<Pedido>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
