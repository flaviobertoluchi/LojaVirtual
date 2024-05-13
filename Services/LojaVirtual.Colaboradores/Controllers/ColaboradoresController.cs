using AutoMapper;
using LojaVirtual.Colaboradores.Data;
using LojaVirtual.Colaboradores.Extensions;
using LojaVirtual.Colaboradores.Models;
using LojaVirtual.Colaboradores.Models.DTOs;
using LojaVirtual.Colaboradores.Models.Tipos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LojaVirtual.Colaboradores.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ColaboradoresController(IColaboradorRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly IColaboradorRepository repository = repository;
        private readonly IMapper mapper = mapper;

        [HttpGet("paginado")]
        public async Task<IActionResult> ObterPaginado(int pagina = 1, int qtdPorPagina = 10, TipoOrdemColaboradores ordem = TipoOrdemColaboradores.Id, bool desc = false, string pesquisa = "")
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            var paginacao = await repository.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa);

            foreach (var colabroador in paginacao.Data)
            {
                colabroador.Senha = "*****";
            }

            return Ok(mapper.Map<Paginacao<ColaboradorDTO>>(paginacao));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            if (id <= 0) return BadRequest();

            //TODO - verficar se tem permissao VisualizarColaborador aqui, se nao tiver, verificar se é o proprio coloaborador no token
            //if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString()) return Forbid();

            var colaborador = await repository.Obter(id);

            if (colaborador is null) return NotFound();
            colaborador.Senha = "*****";

            return Ok(mapper.Map<ColaboradorDTO>(colaborador));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(ColaboradorDTO dto)
        {
            var colaborador = mapper.Map<Colaborador>(dto);

            if (await repository.UsuarioExiste(colaborador.Usuario.Trim())) return UnprocessableEntity("Nome de usuário indisponível.");

            colaborador.Usuario = colaborador.Usuario.Trim();
            colaborador.Senha = CriptografarSHA256.Criptografar(colaborador.Senha);
            colaborador.DataCadastro = DateTime.Now;
            colaborador.Ativo = true;

            await repository.Adicionar(colaborador);

            if (colaborador.Id <= 0) return Problem();

            var colaboradorDTO = mapper.Map<ColaboradorDTO>(colaborador);
            colaboradorDTO.Senha = "*****";

            return CreatedAtAction(nameof(Obter), new { id = colaborador.Id }, colaboradorDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ColaboradorDTO dto)
        {
            if (id <= 0) return BadRequest();

            //TODO - verficar se tem permissao EditarColaborador aqui, se nao tiver, verificar se é o proprio coloaborador no token
            //if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString()) return Forbid();

            var colaborador = await repository.Obter(id);
            if (colaborador is null) return NotFound();

            //TODO - verificar se é o proprio coloaborador no token
            if (!dto.Senha.IsNullOrEmpty() && dto.Senha != "*****") colaborador.Senha = CriptografarSHA256.Criptografar(dto.Senha);

            //TODO - verficar se tem permissao EditarColaborador aqui
            colaborador.Permissao = mapper.Map<Permissao>(dto.Permissao);

            colaborador.DataAtualizacao = DateTime.Now;

            await repository.Atualizar(colaborador);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            if (id <= 0) return BadRequest();

            var colaborador = await repository.Obter(id);
            if (colaborador is null) return NotFound();

            colaborador.Ativo = false;
            colaborador.DataAtualizacao = DateTime.Now;

            await repository.Atualizar(colaborador);

            return NoContent();
        }
    }
}
