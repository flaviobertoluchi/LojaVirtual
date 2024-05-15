using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Site.Services
{
    public class ProdutoService(HttpClient httpClient, IConfiguration configuration) : IProdutoService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        private readonly string baseAddress = configuration.GetValue<string>("Services:Catalogo.Produtos") ?? string.Empty;

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa, TipoOrdemProdutosSite ordem, int categoriaId)
        {
            var response = await httpClient.GetAsync($"{baseAddress}?pagina={pagina}&qtdPorPagina={qtdPorPagina}&pesquisa={pesquisa}&ordem={ordem}&categoriaId={categoriaId}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<Paginacao<Produto>>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Obter(int id)
        {
            var response = await httpClient.GetAsync($"{baseAddress}/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<Produto>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
