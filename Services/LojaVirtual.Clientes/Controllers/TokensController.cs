using AutoMapper;
using LojaVirtual.Clientes.Data;
using LojaVirtual.Clientes.Extensions;
using LojaVirtual.Clientes.Models;
using LojaVirtual.Clientes.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


namespace LojaVirtual.Clientes.Controllers
{
    [Route("api/v1/clientes/[controller]")]
    [ApiController]
    public class TokensController(IClienteRepository repository, IMapper mapper, IConfiguration configuration) : ControllerBase
    {
        private readonly IClienteRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IConfiguration configuration = configuration;

        [HttpPost]
        public async Task<IActionResult> ObterToken([FromBody] UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Usuario) || string.IsNullOrWhiteSpace(dto.Senha)) return BadRequest();

            var cliente = await repository.ObterPorUsuarioESenha(dto.Usuario.Trim(), CriptografarSHA256.Criptografar(dto.Senha), true);
            if (cliente is null) return Unauthorized("Credenciais inválidas.");

            var tokenDTO = await GerarToken(cliente);

            return tokenDTO is null ? Problem("Não foi possível obter o token.") : Ok(tokenDTO);
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> ObterRefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken)) return BadRequest();

            var cliente = await repository.ObterPorRefreshToken(refreshToken);
            if (cliente is null) return Unauthorized("RefreshToken inválido");

            var tokenDTO = await GerarToken(cliente);

            return tokenDTO is null ? Problem("Não foi possível obter o token.") : Ok(tokenDTO);
        }

        private async Task<TokenDTO?> GerarToken(Cliente cliente)
        {
            var key = configuration.GetValue<string>("Token:Key");
            _ = double.TryParse(configuration.GetValue<string>("Token:ExpiracaoMinutos"), out var expiracaoMinutos);

            if (key is null || expiracaoMinutos <= 0) return null;

            var claims = new Dictionary<string, object>
            {
                [JwtRegisteredClaimNames.Sub] = cliente.Id,
                [ClaimTypes.Role] = "cliente"
            };

            var validade = DateTime.UtcNow.AddMinutes(expiracaoMinutos);

            var descriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Expires = validade,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };

            var handler = new JsonWebTokenHandler { SetDefaultTimesOnTokenCreation = false };

            cliente.Token ??= new();
            cliente.Token.ClienteId = cliente.Id;
            cliente.Token.BearerToken = handler.CreateToken(descriptor);
            cliente.Token.RefreshToken = CriptografarSHA256.Criptografar(Guid.NewGuid().ToString());
            cliente.Token.Validade = validade;

            await repository.Atualizar(cliente);

            var tokenDTO = mapper.Map<TokenDTO>(cliente.Token);
            tokenDTO.ClienteUsuario = cliente.Usuario;

            return tokenDTO;
        }
    }
}
