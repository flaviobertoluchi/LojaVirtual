using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Areas.Administracao.ViewComponents
{
    public class CategoriaPaginacaoViewComponent(ICategoriaService service) : ViewComponent
    {
        private readonly ICategoriaService service = service;

        public async Task<IViewComponentResult> InvokeAsync(int pagina = 1, int qtdPorPagina = 10, TipoOrdemCategorias ordem = TipoOrdemCategorias.Id, bool desc = false, string pesquisa = "")
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa);
            if (response.Ok()) return View(response.Content);

            return View();
        }
    }
}