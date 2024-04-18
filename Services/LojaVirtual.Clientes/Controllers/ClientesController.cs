using AutoMapper;
using LojaVirtual.Clientes.Data;
using LojaVirtual.Clientes.Extensions;
using LojaVirtual.Clientes.Models;
using LojaVirtual.Clientes.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LojaVirtual.Clientes.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientesController(IClienteRepository repository, IMapper mapper, IConfiguration configuration) : ControllerBase
    {
        private readonly IMapper mapper = mapper;
        private readonly IConfiguration configuration = configuration;

        [AllowAnonymous]
        [HttpGet("token")]
        public async Task<IActionResult> ObterToken(string usuario, string senha)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha)) return BadRequest();

            var cliente = await repository.ObterPorUsuarioESenha(usuario.Trim(), CriptografarSHA256.Criptografar(senha), true);
            if (cliente is null) return Unauthorized("Credenciais inválidas.");

            var tokenDTO = await GerarToken(cliente);

            return tokenDTO is null ? Problem("Não foi possível obter o token.") : Ok(tokenDTO);
        }

        [AllowAnonymous]
        [HttpGet("refreshtoken")]
        public async Task<IActionResult> ObterRefreshToken(string refreshToken)
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

            if (key == null || expiracaoMinutos <= 0) return null;

            var validade = DateTime.UtcNow.AddMinutes(expiracaoMinutos);

            var descriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object> { [JwtRegisteredClaimNames.Sub] = cliente.Id },
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(long id)
        {
            if (id <= 0) return BadRequest();

            //TODO - verificar no token se cliente é o mesmo que solicitou.

            var cliente = await repository.Obter(id, true, true, true);

            if (cliente is null) return NotFound();
            cliente.Senha = string.Empty;

            return Ok(cliente);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Adicionar(ClienteDTO dto)
        {
            var cliente = mapper.Map<Cliente>(dto);

            if (!(cliente.Emails?.Count > 0 && cliente.Telefones?.Count > 0 && cliente.Enderecos?.Count > 0)) return BadRequest();

            if (await repository.CpfExiste(cliente.Cpf)) return UnprocessableEntity("CPF já cadastrado.");
            if (await repository.UsuarioExiste(cliente.Usuario.Trim())) return UnprocessableEntity("Usuário já existe.");

            var dataAtual = DateTime.Now;

            cliente.Usuario = cliente.Usuario.Trim();
            cliente.Senha = CriptografarSHA256.Criptografar(cliente.Senha);
            cliente.DataCadastro = dataAtual;
            cliente.Ativo = true;

            foreach (var item in cliente.Emails)
            {
                item.DataCadastro = dataAtual;
                item.Ativo = true;
            }

            foreach (var item in cliente.Telefones)
            {
                item.DataCadastro = dataAtual;
                item.Ativo = true;
            }

            foreach (var item in cliente.Enderecos)
            {
                item.DataCadastro = dataAtual;
                item.Ativo = true;
            }

            await repository.Adicionar(cliente);

            if (cliente.Id <= 0) return Problem();

            var clienteDTO = mapper.Map<ClienteDTO>(cliente);
            clienteDTO.Senha = string.Empty;

            return CreatedAtAction(nameof(Obter), new { id = cliente.Id }, clienteDTO);
        }
    }
}
