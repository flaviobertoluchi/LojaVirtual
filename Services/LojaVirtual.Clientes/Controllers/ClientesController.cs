using AutoMapper;
using LojaVirtual.Clientes.Data;
using LojaVirtual.Clientes.Extensions;
using LojaVirtual.Clientes.Models;
using LojaVirtual.Clientes.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace LojaVirtual.Clientes.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientesController(IClienteRepository repository, IMapper mapper, IConfiguration configuration) : ControllerBase
    {
        private readonly IClienteRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IConfiguration configuration = configuration;

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> ObterToken([FromBody] UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Usuario) || string.IsNullOrWhiteSpace(dto.Senha)) return BadRequest();

            var cliente = await repository.ObterPorUsuarioESenha(dto.Usuario.Trim(), CriptografarSHA256.Criptografar(dto.Senha), true);
            if (cliente is null) return Unauthorized("Credenciais inválidas.");

            var tokenDTO = await GerarToken(cliente);

            return tokenDTO is null ? Problem("Não foi possível obter o token.") : Ok(tokenDTO);
        }

        [AllowAnonymous]
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

        [HttpGet("site/{id}")]
        public async Task<IActionResult> ObterSite(int id)
        {
            if (id <= 0) return BadRequest();
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString()) return Forbid();

            var cliente = await repository.Obter(id, true, true, true, false, false);
            if (cliente is null) return NotFound();

            var clienteDTO = mapper.Map<ClienteDTO>(cliente);
            clienteDTO.Senha = "*****";

            return Ok(clienteDTO);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Adicionar(ClienteDTO dto)
        {
            var cliente = mapper.Map<Cliente>(dto);

            if (!(cliente.Emails?.Count > 0 && cliente.Telefones?.Count > 0 && cliente.Enderecos?.Count > 0)) return BadRequest();

            if (await repository.CpfExiste(cliente.Cpf)) return UnprocessableEntity("CPF já cadastrado.");
            if (await repository.UsuarioExiste(cliente.Usuario.Trim())) return UnprocessableEntity("Usuário já existe.");

            cliente.Usuario = cliente.Usuario.Trim();
            cliente.Senha = CriptografarSHA256.Criptografar(cliente.Senha);
            cliente.DataCadastro = DateTime.Now;
            cliente.Ativo = true;

            await repository.Adicionar(cliente);

            if (cliente.Id <= 0) return Problem();

            var clienteDTO = mapper.Map<ClienteDTO>(cliente);
            clienteDTO.Senha = "*****";

            return CreatedAtAction(nameof(ObterSite), new { id = cliente.Id }, clienteDTO);
        }

        [HttpPut("site/{id}")]
        public async Task<IActionResult> AtualizarSite(int id, ClienteDTO dto)
        {
            if (id <= 0) return BadRequest();
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString()) return Forbid();

            if (dto.Emails is null || dto.Emails.Count < 1) return BadRequest("É preciso ter pelo menos um e-mail cadastrado.");
            if (dto.Emails.Count > 10) return BadRequest("Não é possível ter mais de 10 e-mails, por favor edite ou exclua um existente.");

            if (dto.Telefones is null || dto.Telefones.Count < 1) return BadRequest("É preciso ter pelo menos um telefone cadastrado.");
            if (dto.Telefones.Count > 10) return BadRequest("Não é possível ter mais de 10 telefones, por favor edite ou exclua um existente.");

            if (dto.Enderecos is null || dto.Enderecos.Count < 1) return BadRequest("É preciso ter pelo menos um endereço cadastrado.");
            if (dto.Enderecos.Count > 10) return BadRequest("Não é possível ter mais de 10 endereços, por favor edite ou exclua um existente.");

            if (dto.Emails.Any(x => x.ClienteId != id) || dto.Telefones.Any(x => x.ClienteId != id) || dto.Enderecos.Any(x => x.ClienteId != id)) return BadRequest();

            var cliente = await repository.Obter(id, true, true, true, false, true);
            if (cliente is null) return NotFound();

            if (!dto.Senha.IsNullOrEmpty() && dto.Senha != "*****") cliente.Senha = CriptografarSHA256.Criptografar(dto.Senha);

            cliente.Emails = mapper.Map<ICollection<Email>>(dto.Emails);
            cliente.Telefones = mapper.Map<ICollection<Telefone>>(dto.Telefones);
            cliente.Enderecos = mapper.Map<ICollection<Endereco>>(dto.Enderecos);

            if (cliente.Emails.GroupBy(x => x.EmailEndereco).Any(x => x.Count() > 1)) return UnprocessableEntity("Este endereço de e-mail já existe.");
            if (cliente.Telefones.GroupBy(x => x.Numero).Any(x => x.Count() > 1)) return UnprocessableEntity("Este número de telefone já existe.");
            if (cliente.Enderecos.GroupBy(x => x.EnderecoNome).Any(x => x.Count() > 1)) return UnprocessableEntity("Este nome de endereço já existe.");

            cliente.DataAtualizacao = DateTime.Now;

            await repository.Atualizar(cliente);

            return NoContent();
        }

        [HttpDelete("site/{id}")]
        public async Task<IActionResult> ExcluirSite(int id)
        {
            if (id <= 0) return BadRequest();
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString()) return Forbid();

            var cliente = await repository.Obter(id, true, true, true, false, true);
            if (cliente is null) return NotFound();

            cliente.Ativo = false;
            cliente.DataAtualizacao = DateTime.Now;

            await repository.Atualizar(cliente);

            return NoContent();
        }
    }
}
