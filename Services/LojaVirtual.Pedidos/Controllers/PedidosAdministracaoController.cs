using AutoMapper;
using LojaVirtual.Pedidos.Data;
using LojaVirtual.Pedidos.Models;
using LojaVirtual.Pedidos.Models.DTOs;
using LojaVirtual.Pedidos.Models.Tipos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Pedidos.Controllers
{
    [Authorize(Roles = "colaborador")]
    [Route("api/v1/pedidos/administracao")]
    [ApiController]
    public class PedidosAdministracaoController(IPedidoRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly IPedidoRepository repository = repository;
        private readonly IMapper mapper = mapper;

        [HttpGet("paginado")]
        public async Task<IActionResult> ObterPaginado(int pagina = 1, int qtdPorPagina = 10, TipoOrdemPedidos ordem = TipoOrdemPedidos.Id, bool desc = false, string pesquisa = "", string pesquisaCpf = "", DateTime? dataCompraInicial = null, DateTime? dataCompraFinal = null)
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            var paginacao = await repository.ObterPaginadoAdministracao(pagina, qtdPorPagina, ordem, desc, pesquisa, pesquisaCpf, dataCompraInicial, dataCompraFinal);

            return Ok(mapper.Map<Paginacao<PedidoDTO>>(paginacao));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            if (id <= 0) return BadRequest();

            var pedido = await repository.Obter(id, false);

            if (pedido is null) return NotFound();

            return Ok(mapper.Map<PedidoDTO>(pedido));
        }

        [HttpPost("situacao")]
        public async Task<IActionResult> AdicionarSituacao(SituacaoPedidoDTO dto)
        {
            if (dto.PedidoId <= 0) return BadRequest();

            var pedido = await repository.Obter(dto.PedidoId, true);
            if (pedido is null) return NotFound();

            pedido.SituacoesPedido.Add(
                    new()
                    {
                        PedidoId = dto.PedidoId,
                        TipoSituacaoPedido = dto.TipoSituacaoPedido,
                        Mensagem = dto.Mensagem,
                        Data = DateTime.Now,
                    }
                );

            await repository.Atualizar(pedido);

            return NoContent();
        }
    }
}
