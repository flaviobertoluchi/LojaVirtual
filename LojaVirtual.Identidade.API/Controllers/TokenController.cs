using LojaVirtual.Identidade.API.Data.Interfaces;
using LojaVirtual.Identidade.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LojaVirtual.Identidade.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController(IUsuarioRepository repository, IConfiguration configuration) : ControllerBase
    {
        private readonly IUsuarioRepository repository = repository;
        private readonly IConfiguration configuration = configuration;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ObterToken(string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha)) return BadRequest();

            var usuario = await repository.ObterPorLoginESenha(login.Trim().ToLower(), CriptografarSenha.Criptografar(senha));
            if (usuario is null) return Unauthorized("Credenciais inválidas");

            return Ok(GerarJwt(usuario.Id));
        }

        private object? GerarJwt(long sub)
        {
            var key = configuration.GetValue<string>("Token:Key");
            _ = double.TryParse(configuration.GetValue<string>("Token:ExpiracaoMinutos"), out var expiracaoMinutos);

            if (key == null || expiracaoMinutos <= 0) return null;

            var descriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object> { [JwtRegisteredClaimNames.Sub] = sub },
                Expires = DateTime.UtcNow.AddMinutes(expiracaoMinutos),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };

            var handler = new JsonWebTokenHandler { SetDefaultTimesOnTokenCreation = false };

            return new { token = handler.CreateToken(descriptor), sub, validade = descriptor.Expires };
        }
    }
}
