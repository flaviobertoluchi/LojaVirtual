using AutoMapper;
using LojaVirtual.Pedidos.Config;
using LojaVirtual.Pedidos.Data;
using LojaVirtual.Pedidos.Models;
using LojaVirtual.Pedidos.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LojaVirtual.Pedidos.Controllers
{
    [Authorize(Roles = "cliente")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PedidosController(IPedidoRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly IPedidoRepository repository = repository;
        private readonly IMapper mapper = mapper;

        [HttpGet("quantidadepedidoscliente")]
        public async Task<IActionResult> QuantidadePedidosCliente()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var clienteId)) return Forbid();

            return Ok(await repository.QuantidadePedidosCliente(clienteId));
        }

        [HttpGet]
        public async Task<IActionResult> ObterPaginado(int pagina, int qtdPorPagina)
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var clienteId)) return Forbid();

            var paginacao = await repository.ObterPaginado(pagina, qtdPorPagina, clienteId);

            return Ok(mapper.Map<Paginacao<PedidoDTO>>(paginacao));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            if (id <= 0) return BadRequest();

            var pedido = await repository.Obter(id);

            if (pedido is null) return NotFound();
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != pedido.Cliente.ClienteId.ToString()) return Forbid();

            return Ok(mapper.Map<PedidoDTO>(pedido));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(PedidoDTO dto)
        {
            if (dto.QuantidadeItens <= 0
                || dto.ValorTotal <= 0
                || dto.QuantidadeItens != dto.PedidoItens.Sum(x => x.Quantidade)
                || dto.ValorTotal != dto.PedidoItens.Sum(x => x.Quantidade * x.Preco)
                || dto.Cliente.ClienteId <= 0) return BadRequest();

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != dto.Cliente.ClienteId.ToString()) return Forbid();

            var pedido = mapper.Map<Pedido>(dto);

            pedido.SituacoesPedido =
                [
                    new()
                    {
                        TipoSituacaoPedido = Models.Tipos.TipoSituacaoPedido.Recebido,
                        Mensagem = SituacaoPedidoMensagens.Recebido,
                        Data = DateTime.Now,
                    }
                ];

            await repository.Adicionar(pedido);
            if (pedido.Id <= 0) return Problem();

            return Ok(mapper.Map<PedidoDTO>(pedido));
        }
    }
}
