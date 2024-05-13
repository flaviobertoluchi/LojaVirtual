using AutoMapper;
using LojaVirtual.Site.Areas.Administracao.Models.Tipos;
using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using System.Text.Json;

namespace LojaVirtual.Site.Areas.Administracao.Services
{
    public class ClienteAdministracaoService(HttpClient httpClient, IConfiguration configuration, IMapper mapper, Sessao sessao) : IClienteAdministracaoService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMapper mapper = mapper;
        private readonly Sessao sessao = sessao;
        private readonly string baseAddress = configuration.GetValue<string>("Services:Clientes") ?? string.Empty;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemClientes ordem, bool desc, string pesquisa, string pesquisaCpf)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/administracao/paginado?pagina={pagina}&qtdPorPagina={qtdPorPagina}&ordem={ordem}&desc={desc}&pesquisa={pesquisa}&pesquisaCpf={pesquisaCpf}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<Paginacao<ClienteViewModel>>(JsonSerializer.Deserialize<Paginacao<Cliente>>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, response.Content);
        }
    }
}
