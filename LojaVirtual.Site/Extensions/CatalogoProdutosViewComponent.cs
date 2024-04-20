using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Extensions
{
    public class CatalogoProdutosViewComponent(IProdutoService service) : ViewComponent
    {
        private readonly IProdutoService service = service;

        public async Task<IViewComponentResult> InvokeAsync(int pagina = 1, int qtdPorPagina = 12, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, long categoriaId = 0)
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, pesquisa, ordem, categoriaId);

            if (response.Ok()) return View(response.Content);
            if (response.NotFound()) return View();

            ViewBag.Mensagem = response.Content;
            return View();
        }
    }
}
