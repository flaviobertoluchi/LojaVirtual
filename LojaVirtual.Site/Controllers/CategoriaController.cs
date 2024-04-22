using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    [Route("categorias")]
    public class CategoriaController(ICategoriaService service) : Controller
    {
        private readonly ICategoriaService service = service;

        public async Task<IActionResult> Index()
        {
            var response = await service.ObterTodos();
            if (response.Ok()) return View(response.Content);

            return View();
        }
    }
}
