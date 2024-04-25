using AutoMapper;
using LojaVirtual.Catalogo.Models;
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
    public class CategoriasController(ICategoriaRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly ICategoriaRepository repository = repository;
        private readonly IMapper mapper = mapper;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var categorias = await repository.ObterTodos();
            if (categorias.Count <= 0) return NotFound();

            return Ok(mapper.Map<ICollection<CategoriaDTO>>(categorias.ToList()));
        }

        [HttpGet("paginado")]
        public async Task<IActionResult> ObterPaginado(int pagina = 1, int qtdPorPagina = 10)
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            var paginacao = await repository.ObterPaginado(pagina, qtdPorPagina);

            return Ok(mapper.Map<Paginacao<CategoriaDTO>>(paginacao));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(long id)
        {
            if (id <= 0) return BadRequest();

            var categoria = await repository.Obter(id);

            if (categoria is null) return NotFound();

            return Ok(mapper.Map<CategoriaDTO>(categoria));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CategoriaDTO dto)
        {
            if (await repository.ObterPorNome(dto.Nome.Trim()) is not null) return UnprocessableEntity("Já existe este nome de categoria.");

            var categoria = mapper.Map<Categoria>(dto);
            categoria.Nome = categoria.Nome.Trim();

            await repository.Adicionar(categoria);

            if (categoria.Id <= 0) return Problem();

            var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);

            return CreatedAtAction(nameof(Obter), new { id = categoria.Id }, categoriaDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, CategoriaDTO dto)
        {
            if (id <= 0 || id != dto.Id) return BadRequest();

            var categoriaBanco = await repository.Obter(id);
            if (categoriaBanco is null) return NotFound();

            var categoriaBancoPorNome = await repository.ObterPorNome(dto.Nome.Trim());
            if (categoriaBancoPorNome?.Id != id) return UnprocessableEntity("Já existe este nome de categoria.");

            var categoria = mapper.Map<Categoria>(dto);
            categoria.Nome = categoria.Nome.Trim();

            await repository.Atualizar(categoria);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            if (id <= 0) return BadRequest();

            var categoria = await repository.Obter(id);
            if (categoria is null) return NotFound();

            await repository.Excluir(categoria);

            return NoContent();
        }
    }
}
