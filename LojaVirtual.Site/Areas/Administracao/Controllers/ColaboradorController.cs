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
        public IActionResult Entrar(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("entrar")]
        public async Task<IActionResult> Entrar([FromForm] ColaboradorViewModel model, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var response = await service.Entrar(model.Usuario, model.Senha);

            if (response.Ok())
            {
                if (returnUrl is not null) return LocalRedirect(returnUrl);

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

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
