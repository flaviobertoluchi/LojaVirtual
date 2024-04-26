using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Site.Services
{
    public class CategoriaService(HttpClient httpClient, IConfiguration configuration, Sessao sessao) : ICategoriaService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly Sessao sessao = sessao;
        private readonly string baseAddress = configuration.GetValue<string>("Services:Catalogo.Categorias") ?? string.Empty;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public async Task<ResponseApi> ObterTodos()
        {
            var response = await httpClient.GetAsync(baseAddress);

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<ICollection<Categoria>>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemCategorias ordem, bool desc, string pesquisa)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/paginado?pagina={pagina}&qtdPorPagina={qtdPorPagina}&ordem={ordem}&desc={desc}&pesquisa={pesquisa}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<Paginacao<Categoria>>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> Obter(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<Categoria>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> Adicionar(Categoria categoria)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PostAsJsonAsync($"{baseAddress}", categoria);

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Atualizar(int id, Categoria categoria)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PutAsJsonAsync($"{baseAddress}/{id}", categoria);

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Excluir(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.DeleteAsync($"{baseAddress}/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
