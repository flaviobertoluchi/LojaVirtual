using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;
using System.Text.Json;

namespace LojaVirtual.Site.Areas.Administracao.Services
{
    public class PedidoAdministracaoService(HttpClient httpClient, IConfiguration configuration, Sessao sessao) : IPedidoAdministracaoService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly Sessao sessao = sessao;
        private readonly string baseAddress = configuration.GetValue<string>("Services:Pedidos") ?? string.Empty;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemPedidos ordem, bool desc, string pesquisa, string pesquisaCpf, DateTime? dataCompraInicial, DateTime? dataCompraFinal)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/administracao/paginado?pagina={pagina}&qtdPorPagina={qtdPorPagina}&ordem={ordem}&desc={desc}&pesquisa={pesquisa}&pesquisaCpf={pesquisaCpf}&dataCompraInicial={dataCompraInicial?.ToString("MM-dd-yyyy HH:mm:ss")}&dataCompraFinal={dataCompraFinal?.ToString("MM-dd-yyyy HH:mm:ss")}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<Paginacao<Pedido>>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> Obter(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/administracao/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<Pedido>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> AdicionarSituacao(SituacaoPedidoViewModel situacao)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PostAsJsonAsync($"{baseAddress}/administracao/situacao", situacao);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
