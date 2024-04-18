using LojaVirtual.WebApp.Models;
using LojaVirtual.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WebApp.Controllers
{
    public class ClienteController(IClienteService service) : Controller
    {
        private readonly IClienteService service = service;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] ClienteViewModel model)
        {
            var response = await service.Entrar(model.Usuario, model.Senha);

            if (response.Ok()) return RedirectToAction(nameof(HomeController.Index), "Home");

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromForm] ClienteViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.Adicionar(model);

            if (response.Ok())
            {
                var responseEntrar = await service.Entrar(model.Usuario, model.Senha);

                if (responseEntrar.Ok()) return RedirectToAction(nameof(HomeController.Index), "Home");

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        public async Task<IActionResult> Sair()
        {
            await service.Sair();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
