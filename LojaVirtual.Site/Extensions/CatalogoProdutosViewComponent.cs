using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LojaVirtual.Site.Extensions
{
    public class CatalogoProdutosViewComponent(IProdutoService service) : ViewComponent
    {
        private readonly IProdutoService service = service;

        public async Task<IViewComponentResult> InvokeAsync(int pagina = 1, int qtdPorPagina = 12, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao)
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, pesquisa, ordem);

            if (response.Ok()) return View(response.Content);
            if (response.Status == HttpStatusCode.NotFound) return View();

            ViewBag.Mensagem = response.Content;
            return View();
        }
    }
}
