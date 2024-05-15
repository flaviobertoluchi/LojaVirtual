using AutoMapper;
using LojaVirtual.Site.Areas.Administracao.Models;
using LojaVirtual.Site.Areas.Administracao.Models.Services;
using LojaVirtual.Site.Areas.Administracao.Models.Tipos;
using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models.Services;
using System.Text.Json;

namespace LojaVirtual.Site.Areas.Administracao.Services
{
    public class ColaboradorService(HttpClient httpClient, IConfiguration configuration, IMapper mapper, Sessao sessao) : IColaboradorService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMapper mapper = mapper;
        private readonly Sessao sessao = sessao;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        private readonly string baseAddress = configuration.GetValue<string>("Services:Colaboradores") ?? string.Empty;

        public async Task<ResponseApi> Entrar(string usuario, string senha)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseAddress}/tokens", new { usuario, senha }, options);

            if (response.IsSuccessStatusCode)
            {
                sessao.Adicionar(Sessao.colaboradorKey, await response.Content.ReadAsStringAsync());
                return new(response.StatusCode);
            }

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> EntrarPorRefreshToken(string refreshToken)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseAddress}/tokens/refreshtoken", refreshToken, options);

            if (response.IsSuccessStatusCode)
            {
                sessao.Adicionar(Sessao.colaboradorKey, await response.Content.ReadAsStringAsync());
                return new(response.StatusCode);
            }

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task Sair()
        {
            sessao.Excluir(Sessao.colaboradorKey);
            await Task.CompletedTask;
        }

        public async Task<ResponseApi> Obter()
        {
            var colaboradorToken = sessao.ObterColaboradorToken();

            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", colaboradorToken?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/{colaboradorToken?.ColaboradorId}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ColaboradorViewModel>(JsonSerializer.Deserialize<Colaborador>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemColaboradores ordem, bool desc, string pesquisa)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/paginado?pagina={pagina}&qtdPorPagina={qtdPorPagina}&ordem={ordem}&desc={desc}&pesquisa={pesquisa}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<Paginacao<ColaboradorViewModel>>(JsonSerializer.Deserialize<Paginacao<Colaborador>>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> Obter(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ColaboradorViewModel>(JsonSerializer.Deserialize<Colaborador>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Adicionar(ColaboradorViewModel model)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PostAsJsonAsync(baseAddress, mapper.Map<Colaborador>(model), options);

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ColaboradorViewModel>(JsonSerializer.Deserialize<Colaborador>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Atualizar(int id, ColaboradorViewModel model)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PutAsJsonAsync($"{baseAddress}/{id}", mapper.Map<Colaborador>(model));

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
