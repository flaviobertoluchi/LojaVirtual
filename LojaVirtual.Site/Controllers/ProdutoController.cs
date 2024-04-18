using LojaVirtual.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LojaVirtual.WebApp.Controllers
{
    public class ProdutoController(IProdutoService service) : Controller
    {
        private readonly IProdutoService service = service;

        public async Task<IActionResult> Index()
        {
            var response = await service.ObterPaginado(1, 10);

            if (response.Ok()) return View(response.Content);
            if (response.Status == HttpStatusCode.NotFound) return View();

            ViewBag.Mensagem = response.Content;
            return View();
        }
    }
}
