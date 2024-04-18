using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Site.Services
{
    public class ProdutoService(HttpClient httpClient, IConfiguration configuration) : IProdutoService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        private readonly string baseAddress = configuration.GetValue<string>("Services:Catalogo.Produtos") ?? string.Empty;

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina)
        {
            var response = await httpClient.GetAsync($"{baseAddress}/?pagina={pagina}&qtdPorPagina={qtdPorPagina}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<ICollection<Produto>>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
