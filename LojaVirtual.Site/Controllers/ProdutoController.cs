using LojaVirtual.Site.Models.Tipos;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    [Route("produtos")]
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("catalogo")]
        public IActionResult CatalogoProdutos(int pagina = 1, int qtdPorPagina = 12, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, long categoriaId = 0)
        {
            return ViewComponent("CatalogoProdutos", new { pagina, qtdPorPagina, pesquisa, ordem, categoriaId });
        }

        [Route("detalhes")]
        public IActionResult Detalhes()
        {
            return View();
        }
    }
}
