using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Site.ViewComponents
{
    public class CatalogoCategoriasViewComponent(ICategoriaService service) : ViewComponent
    {
        private readonly ICategoriaService service = service;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await service.ObterTodos();
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
    }
}
