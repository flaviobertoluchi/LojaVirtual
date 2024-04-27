using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    [Route("produtos")]
    public class ProdutoController(IProdutoService service) : Controller
    {
        private readonly IProdutoService service = service;

        public IActionResult Index()
        {
            return View();
        }

        [Route("catalogo")]
        public IActionResult CatalogoProdutos(int pagina = 1, int qtdPorPagina = 12, string pesquisa = "", TipoOrdemProdutosSite ordem = TipoOrdemProdutosSite.Padrao, int categoriaId = 0)
        {
            return ViewComponent("CatalogoProdutos", new { pagina, qtdPorPagina, pesquisa, ordem, categoriaId });
        }

        [Route("{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var response = await service.ObterSite(id);
            if (response.Ok()) return View(response.Content);

            return View();
        }
    }
}
