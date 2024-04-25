using AutoMapper;
using LojaVirtual.Colaboradores.Data;
using LojaVirtual.Colaboradores.Extensions;
using LojaVirtual.Colaboradores.Models;
using LojaVirtual.Colaboradores.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LojaVirtual.Colaboradores.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ColaboradoresController(IColaboradorRepository repository, IMapper mapper, IConfiguration configuration) : ControllerBase
    {
        private readonly IColaboradorRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IConfiguration configuration = configuration;

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> ObterToken([FromBody] UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Usuario) || string.IsNullOrWhiteSpace(dto.Senha)) return BadRequest();

            var colaborador = await repository.ObterPorUsuarioESenha(dto.Usuario.Trim(), CriptografarSHA256.Criptografar(dto.Senha), true);
            if (colaborador is null) return Unauthorized("Credenciais inválidas.");

            var tokenDTO = await GerarToken(colaborador);

            return tokenDTO is null ? Problem("Não foi possível obter o token.") : Ok(tokenDTO);
        }

        [AllowAnonymous]
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> ObterRefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken)) return BadRequest();

            var colaborador = await repository.ObterPorRefreshToken(refreshToken);
            if (colaborador is null) return Unauthorized("RefreshToken inválido");

            var tokenDTO = await GerarToken(colaborador);

            return tokenDTO is null ? Problem("Não foi possível obter o token.") : Ok(tokenDTO);
        }

        private async Task<TokenDTO?> GerarToken(Colaborador colaborador)
        {
            var key = configuration.GetValue<string>("Token:Key");
            _ = double.TryParse(configuration.GetValue<string>("Token:ExpiracaoMinutos"), out var expiracaoMinutos);

            if (key is null || expiracaoMinutos <= 0) return null;

            var validade = DateTime.UtcNow.AddMinutes(expiracaoMinutos);

            var descriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object> { [JwtRegisteredClaimNames.Sub] = colaborador.Id },
                Expires = validade,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };

            var handler = new JsonWebTokenHandler { SetDefaultTimesOnTokenCreation = false };

            colaborador.Token ??= new();
            colaborador.Token.ColaboradorId = colaborador.Id;
            colaborador.Token.BearerToken = handler.CreateToken(descriptor);
            colaborador.Token.RefreshToken = CriptografarSHA256.Criptografar(Guid.NewGuid().ToString());
            colaborador.Token.Validade = validade;

            await repository.Atualizar(colaborador);

            var tokenDTO = mapper.Map<TokenDTO>(colaborador.Token);
            tokenDTO.ColaboradorUsuario = colaborador.Usuario;

            return tokenDTO;
        }
    }
}
