using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    [Route("cliente/pedidos")]
    [ClienteAutorizacao]
    public class ClientePedidoController(IPedidoService service) : Controller
    {
        private readonly IPedidoService service = service;

        public async Task<IActionResult> Index()
        {
            var response = await service.ObterPaginado(1, 10);
            if (response.Ok()) return View(response.Content);

            ViewBag.Mensagem = response.Content;
            return View();
        }

        [Route("paginacao")]
        public async Task<IActionResult> PedidoPaginacao(int pagina = 1)
        {
            var response = await service.ObterPaginado(pagina, 10);
            if (response.Ok()) return PartialView("_PedidoPaginacaoPartial", response.Content);

            ViewBag.Mensagem = response.Content;
            return PartialView("_PedidoPaginacaoPartial");
        }

        [Route("{id}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var response = await service.Obter(id);
            if (response.Ok()) return View(response.Content);

            ViewBag.Mensagem = response.Content;
            return View();
        }
    }
}
