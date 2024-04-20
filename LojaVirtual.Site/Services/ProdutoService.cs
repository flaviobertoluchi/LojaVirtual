using LojaVirtual.Site.Models;
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

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, long categoriaId = 0)
        {
            var response = await httpClient.GetAsync($"{baseAddress}/?pagina={pagina}&qtdPorPagina={qtdPorPagina}&pesquisa={pesquisa}&ordem={ordem}&categoriaId={categoriaId}");

            if (response.IsSuccessStatusCode)
            {
                var produtos = new CatalogoProdutosViewModel
                {
                    Produtos = JsonSerializer.Deserialize<ICollection<Produto>>(await response.Content.ReadAsStringAsync(), options) ?? []
                };

                if (response.Headers.TryGetValues("totalItens", out var totalItensStr))
                    if (long.TryParse(totalItensStr.FirstOrDefault(), out var totalItensLong))
                        produtos.TotalItens = totalItensLong;

                if (response.Headers.TryGetValues("qtdPorPagina", out var qtdPorPaginaStr))
                    if (int.TryParse(qtdPorPaginaStr.FirstOrDefault(), out var qtdPorPaginaInt))
                        produtos.QtdPorPagina = qtdPorPaginaInt;

                if (response.Headers.TryGetValues("totalPaginas", out var totalPaginasStr))
                    if (int.TryParse(totalPaginasStr.FirstOrDefault(), out var totalPaginasInt))
                        produtos.TotalPaginas = totalPaginasInt;

                if (response.Headers.TryGetValues("paginaAtual", out var paginaAtualStr))
                    if (int.TryParse(paginaAtualStr.FirstOrDefault(), out var paginaAtualInt))
                        produtos.PaginaAtual = paginaAtualInt;

                if (response.Headers.TryGetValues("paginaAnterior", out var paginaAnteriorStr))
                    if (int.TryParse(paginaAnteriorStr.FirstOrDefault(), out var paginaAnteriorInt))
                        produtos.PaginaAnterior = paginaAnteriorInt;

                if (response.Headers.TryGetValues("paginaProxima", out var paginaProximaStr))
                    if (int.TryParse(paginaProximaStr.FirstOrDefault(), out var paginaProximaInt))
                        produtos.PaginaProxima = paginaProximaInt;

                return new(response.StatusCode, produtos);
            }

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
