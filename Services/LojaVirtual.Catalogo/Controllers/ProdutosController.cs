using AutoMapper;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ObterPaginado(int pagina = 1, int qtdPorPagina = 10)
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            var totalItens = await repository.TotalItens();
            var produtos = await repository.ObterPaginado(pagina, qtdPorPagina, true);
            var totalPaginas = (totalItens + qtdPorPagina - 1) / qtdPorPagina;

            Response.Headers.Append("totalItens", totalItens.ToString());
            Response.Headers.Append("qtdPorPagina", qtdPorPagina.ToString());
            Response.Headers.Append("totalPaginas", totalPaginas.ToString());
            Response.Headers.Append("paginaAtual", pagina.ToString());
            if (totalPaginas > 1 && pagina > 1) Response.Headers.Append("paginaAnterior", (pagina - 1).ToString());
            if (pagina < totalPaginas) Response.Headers.Append("paginaProxima", (pagina + 1).ToString());

            if (produtos.Count <= 0) return NotFound();

            return Ok(mapper.Map<ICollection<ProdutoDTO>>(produtos.ToList()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(long id)
        {
            if (id <= 0) return BadRequest();

            var produto = await repository.Obter(id);

            if (produto is null) return NotFound();

            return Ok(mapper.Map<ProdutoDTO>(produto));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(ProdutoDTO dto)
        {
            if (dto.Estoque < 0 || dto.Preco < 0 || dto.CategoriaId <= 0) return BadRequest();

            if (categoriaRepository.Obter(dto.CategoriaId) is null) return BadRequest("Categoria não existe.");

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
        public async Task<IActionResult> Atualizar(long id, ProdutoDTO dto)
        {
            if (id <= 0 || id != dto.Id) return BadRequest();

            var produtoBanco = await repository.Obter(id);
            if (produtoBanco is null) return NotFound();

            var produto = mapper.Map<Produto>(dto);

            produtoBanco.CategoriaId = produto.CategoriaId;
            produtoBanco.Nome = produto.Nome;
            produtoBanco.Descricao = produto.Descricao;
            produtoBanco.Estoque = produto.Estoque;
            produtoBanco.Preco = produto.Preco;

            // Remove imagens do banco que não existem no json
            foreach (var item in produtoBanco.Imagens)
            {
                if (!dto.Imagens.Any(x => x == item.Local))
                    produtoBanco.Imagens.Remove(item);
            }

            // Adiciona imagens do json que não existem no banco
            foreach (var item in dto.Imagens)
            {
                if (!produtoBanco.Imagens.Any(x => x.Local == item))
                    produtoBanco.Imagens.Add(new() { Local = item });
            }

            produtoBanco.DataAtualizacao = DateTime.Now;

            await repository.Atualizar(produtoBanco);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            if (id <= 0) return BadRequest();

            var produto = await repository.Obter(id);
            if (produto is null) return NotFound();

            await repository.Excluir(produto);

            return NoContent();
        }
    }
}
