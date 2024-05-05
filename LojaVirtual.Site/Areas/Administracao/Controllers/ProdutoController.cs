﻿using LojaVirtual.Site.Config;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Site.Areas.Administracao.Controllers
{
    [Area("Administracao")]
    [Route("administracao/produtos")]
    [ColaboradorAutorizacao]
    public class ProdutoController(IProdutoService service, ICategoriaService categoriaService) : Controller
    {
        private readonly IProdutoService service = service;
        private readonly ICategoriaService categoriaService = categoriaService;

        public async Task<IActionResult> Index()
        {
            var response = await service.ObterPaginado(1, 10, TipoOrdemProdutos.Id, false, "", 0, false);
            if (response.Ok()) return View(response.Content);

            return View();
        }

        [Route("paginacao")]
        public async Task<IActionResult> ProdutoPaginacao(int pagina = 1, int qtdPorPagina = 10, TipoOrdemProdutos ordem = TipoOrdemProdutos.Id, bool desc = false, string pesquisa = "", int categoriaId = 0, bool semEstoque = false)
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa, categoriaId, semEstoque);
            if (response.Ok()) return PartialView("_ProdutoPaginacaoPartial", response.Content);

            return PartialView("_ProdutoPaginacaoPartial");
        }

        [Route("adicionar")]
        public async Task<IActionResult> Adicionar()
        {
            await PreencherCategorias();
            return View();
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromForm] ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PreencherCategorias();
                return View(model);
            }

            var response = await service.Adicionar(model);

            if (response.Ok())
            {
                await UploadImagens(((ProdutoViewModel)response.Content!).Id, model.Imagens ?? []);

                TempData["Sucesso"] = Mensagens.AdicionarSucesso;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensagem = response.Content;
            await PreencherCategorias();
            return View(model);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var response = await service.Obter(id);

            if (response.Ok())
            {
                await PreencherCategorias();
                return View(response.Content);
            }

            ViewBag.Mensagem = response.Content;
            return View();
        }

        [HttpPost("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromForm] ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PreencherCategorias();
                return View(model);
            }

            var response = await service.Atualizar(id, model);

            if (response.Ok())
            {
                if (model.Imagens?.Count > 0)
                {
                    await UploadImagens(id, model.Imagens);
                }

                TempData["Sucesso"] = Mensagens.AtualizarSucesso;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensagem = response.Content;
            await PreencherCategorias();
            return View(model);
        }

        [Route("excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var response = await service.Excluir(id);

            if (response.Ok())
            {
                ExcluirPasta(id);
                TempData["Sucesso"] = Mensagens.ExcluirSucesso;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensagem = response.Content;
            return RedirectToAction(nameof(Editar), "Produto", new { id });
        }

        private async Task PreencherCategorias()
        {
            var response = await categoriaService.ObterTodos();
            if (response.Ok())
            {
                var categorias = (ICollection<CategoriaViewModel>)response.Content!;

                var selectListItem = categorias.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                }).ToList();

                ViewBag.Categorias = selectListItem;
            }
        }

        private static async Task UploadImagens(int produtoId, ICollection<IFormFile> imagens)
        {
            var pasta = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/img/produtos/{produtoId}");
            ExcluirPasta(produtoId);
            Directory.CreateDirectory(pasta);

            foreach (var imagem in imagens)
            {
                var caminhoCompleto = Path.Combine(pasta, imagem.FileName);
                using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                await imagem.CopyToAsync(stream);
            }
        }

        private static void ExcluirPasta(int produtoId)
        {
            var pasta = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/img/produtos/{produtoId}");
            if (Directory.Exists(pasta)) Directory.Delete(pasta, true);
        }
    }
}
