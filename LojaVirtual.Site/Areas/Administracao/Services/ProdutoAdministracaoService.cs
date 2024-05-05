using AutoMapper;
using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;
using System.Text.Json;

namespace LojaVirtual.Site.Areas.Administracao.Services
{
    public class ProdutoAdministracaoService(HttpClient httpClient, IConfiguration configuration, IMapper mapper, Sessao sessao) : IProdutoAdministracaoService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMapper mapper = mapper;
        private readonly Sessao sessao = sessao;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        private readonly string baseAddress = configuration.GetValue<string>("Services:Catalogo.Produtos") ?? string.Empty;

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemProdutos ordem, bool desc, string pesquisa, int categoriaId, bool semEstoque)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/administracao/?pagina={pagina}&qtdPorPagina={qtdPorPagina}&ordem={ordem}&desc={desc}&pesquisa={pesquisa}&categoriaId={categoriaId}&semEstoque={semEstoque}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<Paginacao<ProdutoViewModel>>(JsonSerializer.Deserialize<Paginacao<Produto>>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> Obter(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/administracao/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ProdutoViewModel>(JsonSerializer.Deserialize<Produto>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> Adicionar(ProdutoViewModel model)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PostAsJsonAsync($"{baseAddress}/administracao", mapper.Map<Produto>(model));

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ProdutoViewModel>(JsonSerializer.Deserialize<Produto>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Atualizar(int id, ProdutoViewModel model)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PutAsJsonAsync($"{baseAddress}/administracao/{id}", mapper.Map<Produto>(model));

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Excluir(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.DeleteAsync($"{baseAddress}/administracao/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
