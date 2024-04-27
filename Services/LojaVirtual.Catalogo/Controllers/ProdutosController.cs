using AutoMapper;
using LojaVirtual.Catalogo.Models;
using LojaVirtual.Catalogo.Models.Tipos;
using LojaVirtual.Produtos.Data;
using LojaVirtual.Produtos.Models;
using LojaVirtual.Produtos.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Produtos.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutosController(IProdutoRepository repository, ICategoriaRepository categoriaRepository, IMapper mapper) : ControllerBase
    {
        private readonly IProdutoRepository repository = repository;
        private readonly ICategoriaRepository categoriaRepository = categoriaRepository;
        private readonly IMapper mapper = mapper;

        [HttpGet("paginado")]
        public async Task<IActionResult> ObterPaginado(int pagina = 1, int qtdPorPagina = 10, TipoOrdemProdutos ordem = TipoOrdemProdutos.Id, bool desc = false, string pesquisa = "")
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            var paginacao = await repository.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa);

            return Ok(mapper.Map<Paginacao<ProdutoDTO>>(paginacao));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ObterPaginadoSite(int pagina, int qtdPorPagina, string pesquisa = "", TipoOrdemProdutosSite ordem = TipoOrdemProdutosSite.Padrao, int categoriaId = 0)
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            var paginacao = await repository.ObterPaginadoSite(pagina, qtdPorPagina, pesquisa, ordem, categoriaId, true, false);

            return Ok(mapper.Map<Paginacao<ProdutoDTO>>(paginacao));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            if (id <= 0) return BadRequest();

            var produto = await repository.Obter(id, false);

            if (produto is null) return NotFound();

            return Ok(mapper.Map<ProdutoDTO>(produto));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(ProdutoDTO dto)
        {
            if (dto.Estoque < 0 || dto.Preco < 0 || dto.CategoriaId <= 0) return BadRequest();

            if (await categoriaRepository.Obter(dto.CategoriaId) is null) return BadRequest("Categoria não existe.");

            var produto = mapper.Map<Produto>(dto);

            foreach (var item in dto.Imagens)
            {
                produto.Imagens.Add(new() { Local = item });
            }
            produto.DataCadastro = DateTime.Now;

            await repository.Adicionar(produto);

            if (produto.Id <= 0) return Problem();

            var produtoDTO = mapper.Map<ProdutoDTO>(produto);

            return CreatedAtAction(nameof(Obter), new { id = produto.Id }, produtoDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ProdutoDTO dto)
        {
            if (id <= 0 || id != dto.Id) return BadRequest();

            var produtoBanco = await repository.Obter(id, true);
            if (produtoBanco is null) return NotFound();

            var produto = mapper.Map<Produto>(dto);

            produtoBanco.CategoriaId = produto.CategoriaId;
            produtoBanco.Nome = produto.Nome;
            produtoBanco.Descricao = produto.Descricao;
            produtoBanco.Estoque = produto.Estoque;
            produtoBanco.Preco = produto.Preco;

            if (dto.Imagens?.Count > 0)
            {
                // Remove imagens do banco que não existem no json
                produtoBanco.Imagens = produtoBanco.Imagens.Where(x => dto.Imagens.Contains(x.Local)).ToList();

                // Adiciona imagens do json que não existem no banco
                foreach (var item in dto.Imagens)
                {
                    if (!produtoBanco.Imagens.Any(x => x.Local == item))
                        produtoBanco.Imagens.Add(new() { Local = item });
                }
            }

            produtoBanco.DataAtualizacao = DateTime.Now;

            await repository.Atualizar(produtoBanco);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            if (id <= 0) return BadRequest();

            var produto = await repository.Obter(id, false);
            if (produto is null) return NotFound();

            await repository.Excluir(produto);

            return NoContent();
        }
    }
}
