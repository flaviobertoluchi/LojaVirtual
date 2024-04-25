using LojaVirtual.Site.Areas.Administracao.Models;
using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Areas.Administracao.Controllers
{
    [Area("Administracao")]
    [Route("administracao")]
    public class ColaboradorController(IColaboradorService service) : Controller
    {
        private readonly IColaboradorService service = service;

        [Route("entrar")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("entrar")]
        public async Task<IActionResult> Index([FromForm] ColaboradorViewModel model)
        {
            var response = await service.Entrar(model.Usuario, model.Senha);

            if (response.Ok()) return RedirectToAction(nameof(HomeController.Index), "Home");

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("sair")]
        public async Task<IActionResult> Sair()
        {
            await service.Sair();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
