﻿using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Site.ViewComponents
{
    public class CatalogoProdutosViewComponent(IProdutoService service) : ViewComponent
    {
        private readonly IProdutoService service = service;

        public async Task<IViewComponentResult> InvokeAsync(int pagina = 1, int qtdPorPagina = 12, string pesquisa = "", TipoOrdemProdutosSite ordem = TipoOrdemProdutosSite.Padrao, int categoriaId = 0)
        {
            var response = await service.ObterPaginado(pagina, qtdPorPagina, pesquisa, ordem, categoriaId);

            if (response.Ok()) return View(response.Content);
            if (response.NotFound()) return View();

            ViewBag.Mensagem = response.Content;
            return View(new List<SelectListItem>());
        }
    }
}
