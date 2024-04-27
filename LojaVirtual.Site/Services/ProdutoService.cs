using AutoMapper;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Site.Services
{
    public class ProdutoService(HttpClient httpClient, IConfiguration configuration, IMapper mapper, Sessao sessao) : IProdutoService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly Sessao sessao = sessao;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        private readonly string baseAddress = configuration.GetValue<string>("Services:Catalogo.Produtos") ?? string.Empty;

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemProdutos ordem, bool desc, string pesquisa, int categoriaId, bool semEstoque)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/paginado?pagina={pagina}&qtdPorPagina={qtdPorPagina}&ordem={ordem}&desc={desc}&pesquisa={pesquisa}&categoriaId={categoriaId}&semEstoque={semEstoque}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<Paginacao<ProdutoViewModel>>(JsonSerializer.Deserialize<Paginacao<Produto>>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> ObterPaginadoSite(int pagina, int qtdPorPagina, string pesquisa, TipoOrdemProdutosSite ordem, int categoriaId)
        {
            var response = await httpClient.GetAsync($"{baseAddress}/?pagina={pagina}&qtdPorPagina={qtdPorPagina}&pesquisa={pesquisa}&ordem={ordem}&categoriaId={categoriaId}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<Paginacao<Produto>>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Obter(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ProdutoViewModel>(JsonSerializer.Deserialize<Produto>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> ObterSite(int id)
        {
            var response = await httpClient.GetAsync($"{baseAddress}/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<Produto>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Adicionar(ProdutoViewModel model)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PostAsJsonAsync($"{baseAddress}", mapper.Map<Produto>(model));

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ProdutoViewModel>(JsonSerializer.Deserialize<Produto>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Atualizar(int id, ProdutoViewModel model)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PutAsJsonAsync($"{baseAddress}/{id}", mapper.Map<Produto>(model));

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
