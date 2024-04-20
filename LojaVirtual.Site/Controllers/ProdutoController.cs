using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Site.Controllers
{
    public class ProdutoController(ICategoriaService categoriaService) : Controller
    {
        private readonly ICategoriaService categoriaService = categoriaService;

        public async Task<IActionResult> Index()
        {
            var response = await categoriaService.ObterTodos();
            if (response.Ok())
            {
                var categorias = (ICollection<Categoria>)response.Content!;

                var selectListItem = categorias.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                }).ToList();

                return View(selectListItem);
            }

            return View();
        }

        public IActionResult CatalogoProdutos(int pagina = 1, int qtdPorPagina = 12, string pesquisa = "", TipoOrdemProdutos ordem = TipoOrdemProdutos.Padrao, long categoriaId = 0)
        {
            return ViewComponent("CatalogoProdutos", new { pagina, qtdPorPagina, pesquisa, ordem, categoriaId });
        }
    }
}
