using AutoMapper;
using LojaVirtual.Clientes.Data;
using LojaVirtual.Clientes.Models;
using LojaVirtual.Clientes.Models.DTOs;
using LojaVirtual.Clientes.Models.Tipos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Clientes.Controllers
{
    [Authorize(Roles = "colaborador")]
    [Route("api/v1/clientes/administracao")]
    [ApiController]
    public class ClientesAdministracaoController(IClienteRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly IClienteRepository repository = repository;
        private readonly IMapper mapper = mapper;

        [Authorize(Policy = "VisualizarCliente")]
        [HttpGet("paginado")]
        public async Task<IActionResult> ObterPaginado(int pagina = 1, int qtdPorPagina = 10, TipoOrdemClientes ordem = TipoOrdemClientes.Id, bool desc = false, string pesquisa = "", string pesquisaCpf = "")
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            var paginacao = await repository.ObterPaginado(pagina, qtdPorPagina, ordem, desc, pesquisa, pesquisaCpf);

            foreach (var cliente in paginacao.Data)
            {
                cliente.Senha = "*****";
            }

            return Ok(mapper.Map<Paginacao<ClienteDTO>>(paginacao));
        }
    }
}
