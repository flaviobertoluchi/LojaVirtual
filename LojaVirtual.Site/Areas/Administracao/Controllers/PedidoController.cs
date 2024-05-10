using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Config;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Tipos;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Areas.Administracao.Controllers
{
    [Area("Administracao")]
    [Route("administracao/pedidos")]
    [ColaboradorAutorizacao]
    public class PedidoController(IPedidoAdministracaoService service) : Controller
    {
        private readonly IPedidoAdministracaoService service = service;

        public async Task<IActionResult> Index()
        {
            var response = await service.ObterPaginado(1, 10, TipoOrdemPedidos.Id, false, "", "", null, null);
            if (response.Ok()) return View(response.Content);

            return View();
        }

        [Route("paginacao")]
        public async Task<IActionResult> PedidosPaginacao(int pagina = 1, int qtdPorPagina = 10, TipoOrdemPedidos ordem = TipoOrdemPedidos.Id, bool desc = false, string pesquisa = "", string pesquisaCpf = "", DateTime? dataCompraInicial = null, DateTime? dataCompraFinal = null)
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa, pesquisaCpf, dataCompraInicial, dataCompraFinal);
            if (response.Ok()) return PartialView("_PedidoPaginacaoPartial", response.Content);

            return PartialView("_PedidoPaginacaoPartial");
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var response = await service.Obter(id);

            if (response.Ok()) return View(response.Content);

            ViewBag.Mensagem = response.Content;
            return View();
        }

        [HttpPost("situacao")]
        public async Task<IActionResult> AdicionarSituacao([FromForm] SituacaoPedidoViewModel model)
        {
            if (model.TipoSituacaoPedido == TipoSituacaoPedido.Recebido)
            {
                TempData["Mensagem"] = "Informe uma situação válida.";
                return RedirectToAction(nameof(Editar), new { id = model.PedidoId });
            }

            if (string.IsNullOrEmpty(model.Mensagem))
            {
                TempData["Mensagem"] = "Informe uma mensagem.";
                return RedirectToAction(nameof(Editar), new { id = model.PedidoId });
            }

            var response = await service.AdicionarSituacao(model);

            if (response.Ok())
            {
                TempData["Sucesso"] = Mensagens.AtualizarSucesso;

                return RedirectToAction(nameof(Editar), new { id = model.PedidoId });
            }

            TempData["Mensagem"] = response.Content;
            return RedirectToAction(nameof(Editar), new { id = model.PedidoId });
        }
    }
}
