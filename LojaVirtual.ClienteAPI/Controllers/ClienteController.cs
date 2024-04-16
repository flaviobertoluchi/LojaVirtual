using LojaVirtual.ClienteAPI.Data;
using LojaVirtual.ClienteAPI.Extensions;
using LojaVirtual.ClienteAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LojaVirtual.ClienteAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController(IClienteRepository repository, IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration configuration = configuration;

        [AllowAnonymous]
        [HttpGet("token")]
        public async Task<IActionResult> ObterToken(string usuario, string senha)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha)) return BadRequest();

            var cliente = await repository.ObterPorUsuarioESenha(usuario.Trim().ToLower(), CriptografarSHA256.Criptografar(senha));
            if (cliente is null) return Unauthorized("Credenciais inválidas");

            var clienteJwt = await GerarClienteJwt(cliente);

            return clienteJwt is null ? Problem("Não foi possível obter o token.") : Ok(clienteJwt);
        }

        [AllowAnonymous]
        [HttpGet("refreshtoken")]
        public async Task<IActionResult> ObterRefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken)) return BadRequest();

            var cliente = await repository.ObterPorRefreshToken(refreshToken);
            if (cliente is null) return Unauthorized("RefreshToken inválido");

            var clienteJwt = await GerarClienteJwt(cliente);

            return clienteJwt is null ? Problem("Não foi possível obter o token.") : Ok(clienteJwt);
        }

        private async Task<ClienteJwt?> GerarClienteJwt(Cliente cliente)
        {
            var key = configuration.GetValue<string>("Token:Key");
            _ = double.TryParse(configuration.GetValue<string>("Token:ExpiracaoMinutos"), out var expiracaoMinutos);

            if (key == null || expiracaoMinutos <= 0) return null;

            var validade = DateTime.UtcNow.AddMinutes(expiracaoMinutos);

            var descriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object> { [JwtRegisteredClaimNames.Sub] = cliente.Id },
                Expires = validade,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };

            var handler = new JsonWebTokenHandler { SetDefaultTimesOnTokenCreation = false };

            cliente.RefreshToken = CriptografarSHA256.Criptografar(Guid.NewGuid().ToString());
            await repository.Atualizar(cliente);

            return new()
            {
                Token = handler.CreateToken(descriptor),
                ClienteId = cliente.Id,
                ClienteNome = cliente.Nome,
                Validade = validade,
                RefreshToken = cliente.RefreshToken
            };
        }
    }
}
