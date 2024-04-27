using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Areas.Administracao.ViewComponents
{
    public class ProdutoPaginacaoViewComponent(IProdutoService service) : ViewComponent
    {
        private readonly IProdutoService service = service;

        public async Task<IViewComponentResult> InvokeAsync(int pagina = 1, int qtdPorPagina = 10, TipoOrdemProdutos ordem = TipoOrdemProdutos.Id, bool desc = false, string pesquisa = "")
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa);
            if (response.Ok()) return View(response.Content);

            return View();
        }
    }
}