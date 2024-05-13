using LojaVirtual.Site.Areas.Administracao.Models.Tipos;
using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Areas.Administracao.Controllers
{
    [Area("Administracao")]
    [Route("administracao/clientes")]
    [ColaboradorAutorizacao]
    public class ClienteController(IClienteAdministracaoService service) : Controller
    {
        private readonly IClienteAdministracaoService service = service;

        public async Task<IActionResult> Index()
        {
            var response = await service.ObterPaginado(1, 10, TipoOrdemClientes.Id, false, "", "");
            if (response.Ok()) return View(response.Content);

            return View();
        }

        [Route("paginacao")]
        public async Task<IActionResult> ClientePaginacao(int pagina = 1, int qtdPorPagina = 10, TipoOrdemClientes ordem = TipoOrdemClientes.Id, bool desc = false, string pesquisa = "", string pesquisaCpf = "")
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa, pesquisaCpf);
            if (response.Ok()) return PartialView("_ClientePaginacaoPartial", response.Content);

            return PartialView("_ClientePaginacaoPartial");
        }
    }
}
