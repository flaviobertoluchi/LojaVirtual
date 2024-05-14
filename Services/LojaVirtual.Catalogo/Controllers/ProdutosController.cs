using AutoMapper;
using LojaVirtual.Catalogo.Models;
using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Data;
using LojaVirtual.Produtos.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Produtos.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutosController(IProdutoRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly IProdutoRepository repository = repository;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> ObterPaginado(int pagina, int qtdPorPagina, string pesquisa = "", TipoOrdemProdutosSite ordem = TipoOrdemProdutosSite.Padrao, int categoriaId = 0)
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            var paginacao = await repository.ObterPaginado(pagina, qtdPorPagina, pesquisa, ordem, categoriaId, true, false);

            return Ok(mapper.Map<Paginacao<ProdutoDTO>>(paginacao));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            if (id <= 0) return BadRequest();

            var produto = await repository.Obter(id);

            if (produto is null) return NotFound();

            return Ok(mapper.Map<ProdutoDTO>(produto));
        }
    }
}
