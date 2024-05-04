using LojaVirtual.Site.Models;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    public class ClienteController(IClienteService service) : Controller
    {
        private readonly IClienteService service = service;

        [Route("entrar")]
        public IActionResult Index(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("entrar")]
        public async Task<IActionResult> Index([FromForm] ClienteViewModel model, string? returnUrl)
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

        [Route("nova-conta")]
        public IActionResult Adicionar(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("nova-conta")]
        public async Task<IActionResult> Adicionar([FromForm] ClienteAdicionarViewModel model, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid) return View(model);

            var response = await service.Adicionar(model);

            if (response.Ok())
            {
                var responseEntrar = await service.Entrar(model.Usuario, model.Senha);

                if (responseEntrar.Ok())
                {
                    if (returnUrl is not null) return LocalRedirect(returnUrl);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                return RedirectToAction(nameof(Index));
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

        [Route("conta")]
        public async Task<IActionResult> Conta()
        {
            var response = await service.ObterSite();

            if (response.Ok()) return View(response.Content);

            return View();
        }
    }
}
