using LojaVirtual.Site.Areas.Administracao.Models;
using LojaVirtual.Site.Areas.Administracao.Models.Tipos;
using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Config;
using LojaVirtual.Site.Extensions;
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

        [ColaboradorAutorizacao]
        [Route("senha")]
        public async Task<IActionResult> Senha()
        {
            var response = await service.Obter();

            if (response.Ok()) return View(response.Content);

            return View();
        }

        [ColaboradorAutorizacao]
        [HttpPost("senha")]
        public async Task<IActionResult> Senha(ColaboradorViewModel model, string senhaAtual)
        {
            var responseEntrar = await service.Entrar(model.Usuario, senhaAtual);

            if (responseEntrar.Ok())
            {
                var response = await service.Obter();

                if (response.Ok())
                {
                    var colaborador = (ColaboradorViewModel)response.Content!;
                    colaborador.Senha = model.Senha;

                    var responseAtualziacao = await service.Atualizar(colaborador.Id, colaborador);

                    if (responseAtualziacao.Ok())
                    {
                        TempData["Sucesso"] = Mensagens.AtualizarSucesso;
                        return View();
                    }

                    ViewBag.Mensagem = responseAtualziacao.Content;
                    return View();
                }

                ViewBag.Mensagem = response.Content;
                return View();
            }

            ViewBag.Mensagem = responseEntrar.Content;
            return View();
        }

        [ColaboradorAutorizacao]
        [Route("colaboradores")]
        public async Task<IActionResult> Index()
        {
            var response = await service.ObterPaginado(1, 10, TipoOrdemColaboradores.Id, false, "");
            if (response.Ok()) return View(response.Content);

            return View();
        }

        [ColaboradorAutorizacao]
        [Route("colaboradores/paginacao")]
        public async Task<IActionResult> ColaboradorPaginacao(int pagina = 1, int qtdPorPagina = 10, TipoOrdemColaboradores ordem = TipoOrdemColaboradores.Id, bool desc = false, string pesquisa = "")
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa);
            if (response.Ok()) return PartialView("_ColaboradorPaginacaoPartial", response.Content);

            return PartialView("_ColaboradorPaginacaoPartial");
        }

        [ColaboradorAutorizacao]
        [Route("colaboradores/adicionar")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [ColaboradorAutorizacao]
        [HttpPost("colaboradores/adicionar")]
        public async Task<IActionResult> Adicionar([FromForm] ColaboradorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.Adicionar(model);

            if (response.Ok())
            {
                TempData["Sucesso"] = Mensagens.AdicionarSucesso;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [ColaboradorAutorizacao]
        [Route("colaboradores/editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var response = await service.Obter(id);

            if (response.Ok()) return View(response.Content);

            ViewBag.Mensagem = response.Content;
            return View();
        }

        [ColaboradorAutorizacao]
        [HttpPost("colaboradores/editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromForm] ColaboradorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.Atualizar(id, model);

            if (response.Ok())
            {
                TempData["Sucesso"] = Mensagens.AtualizarSucesso;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [ColaboradorAutorizacao]
        [Route("colaboradores/excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var response = await service.Excluir(id);

            if (response.Ok())
            {
                TempData["Sucesso"] = Mensagens.ExcluirSucesso;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensagem = response.Content;
            return RedirectToAction(nameof(Editar), "Colaborador", new { id, area = "Administracao" });
        }
    }
}
