using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Config;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Tipos;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Areas.Administracao.Controllers
{
    [Area("Administracao")]
    [Route("administracao/categorias")]
    [ColaboradorAutorizacao]
    public class CategoriaController(ICategoriaAdministracaoService service) : Controller
    {
        private readonly ICategoriaAdministracaoService service = service;

        public async Task<IActionResult> Index()
        {
            var response = await service.ObterPaginado(1, 10, TipoOrdemCategorias.Id, false, "");
            if (response.Ok()) return View(response.Content);

            return View();
        }

        [Route("paginacao")]
        public async Task<IActionResult> CategoriaPaginacao(int pagina = 1, int qtdPorPagina = 10, TipoOrdemCategorias ordem = TipoOrdemCategorias.Id, bool desc = false, string pesquisa = "")
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa);
            if (response.Ok()) return PartialView("_CategoriaPaginacaoPartial", response.Content);

            return PartialView("_CategoriaPaginacaoPartial");
        }

        [Route("adicionar")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromForm] CategoriaViewModel model)
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

        [Route("editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var response = await service.Obter(id);

            if (response.Ok()) return View(response.Content);

            ViewBag.Mensagem = response.Content;
            return View();
        }

        [HttpPost("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromForm] CategoriaViewModel model)
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

        [Route("excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var response = await service.Excluir(id);

            if (response.Ok())
            {
                TempData["Sucesso"] = Mensagens.ExcluirSucesso;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensagem = response.Content;
            return RedirectToAction(nameof(Editar), "Categoria", new { id });
        }
    }
}
