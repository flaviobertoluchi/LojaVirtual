using LojaVirtual.WebApp.Models;
using LojaVirtual.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WebApp.Controllers
{
    public class UsuarioController(IIdentidadeService identidadeService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] UsuarioViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await identidadeService.Entrar(model.Login, model.Senha);

            if (response.Ok()) return RedirectToAction(nameof(HomeController.Index), "Home");

            ViewBag.Mensagem = response.Mensagem;
            return View(model);
        }
    }
}
