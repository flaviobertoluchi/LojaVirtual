using AutoMapper;
using LojaVirtual.Produtos.Data;
using LojaVirtual.Produtos.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Produtos.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriasController(ICategoriaRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly ICategoriaRepository repository = repository;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var categorias = await repository.ObterTodos();
            if (categorias.Count <= 0) return NotFound();

            return Ok(mapper.Map<ICollection<CategoriaDTO>>(categorias.ToList()));
        }
    }
}
