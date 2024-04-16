using AutoMapper;
using LojaVirtual.Identidade.API.Data.Interfaces;
using LojaVirtual.Identidade.API.Extensions;
using LojaVirtual.Identidade.API.Models;
using LojaVirtual.Identidade.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LojaVirtual.Identidade.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuariosController(IUsuarioRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly IUsuarioRepository repository = repository;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var usuarios = mapper.Map<ICollection<UsuarioDTO>>(await repository.ObterTodos());

            foreach (var usuario in usuarios) usuario.Senha = string.Empty;

            return usuarios.Count > 0 ? Ok(usuarios) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(long id)
        {
            if (id <= 0) return BadRequest();

            var usuario = await repository.ObterPorId(id);
            if (usuario is null) return NotFound();

            usuario.Senha = string.Empty;

            return Ok(mapper.Map<UsuarioDTO>(usuario));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Login)) return BadRequest();
            if (await repository.ObterPorLogin(dto.Login.Trim().ToLower()) is not null) return UnprocessableEntity("Login já existe.");

            var usuario = mapper.Map<Usuario>(dto);
            usuario.Senha = CriptografarSenha.Criptografar(dto.Senha);
            usuario.Tipo = Models.Tipos.TipoUsuario.Cliente;
            usuario.DataCadastro = DateTime.Now;

            await repository.Adicionar(usuario);

            var usuarioDto = mapper.Map<UsuarioDTO>(usuario);
            usuarioDto.Senha = string.Empty;

            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuarioDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, UsuarioDTO dto)
        {
            //Somente atualização de senha é permitido

            if (id <= 0 || id != dto.Id || string.IsNullOrWhiteSpace(dto.Senha)) return BadRequest();

            var jwtUserId = Request.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (jwtUserId is null) return Unauthorized();
            if (!int.TryParse(jwtUserId, out int userId)) return Unauthorized();
            if (userId != id) return Forbid();

            var usuario = await repository.ObterPorId(id);
            if (usuario is null) return NotFound();

            usuario.Senha = CriptografarSenha.Criptografar(dto.Senha);
            usuario.DataAtualizacao = DateTime.Now;

            await repository.Atualizar(usuario);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desativar(long id)
        {
            if (id <= 0) return BadRequest();

            var jwtUserId = Request.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (jwtUserId is null) return Unauthorized();
            if (!int.TryParse(jwtUserId, out int userId)) return Unauthorized();
            if (userId != id) return Forbid();

            var usuario = await repository.ObterPorId(id);
            if (usuario is null) return NotFound();

            usuario.DataAtualizacao = DateTime.Now;
            usuario.Ativo = false;
            await repository.Atualizar(usuario);

            return NoContent();
        }
    }
}
