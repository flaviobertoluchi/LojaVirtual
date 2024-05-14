using AutoMapper;
using LojaVirtual.Pedidos.Data;
using LojaVirtual.Pedidos.Models;
using LojaVirtual.Pedidos.Models.DTOs;
using LojaVirtual.Pedidos.Models.Services;
using LojaVirtual.Pedidos.Models.Tipos;
using LojaVirtual.Pedidos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Pedidos.Controllers
{
    [Authorize(Roles = "colaborador")]
    [Route("api/v1/pedidos/administracao")]
    [ApiController]
    public class PedidosAdministracaoController(IPedidoRepository repository, IEstoqueService estoqueService, IMapper mapper) : ControllerBase
    {
        private readonly IPedidoRepository repository = repository;
        private readonly IEstoqueService estoqueService = estoqueService;
        private readonly IMapper mapper = mapper;

        [Authorize(Policy = "VisualizarPedido")]
        [HttpGet("paginado")]
        public async Task<IActionResult> ObterPaginado(int pagina = 1, int qtdPorPagina = 10, TipoOrdemPedidos ordem = TipoOrdemPedidos.Id, bool desc = false, string pesquisa = "", string pesquisaCpf = "", DateTime? dataCompraInicial = null, DateTime? dataCompraFinal = null)
        {
            if (pagina <= 0 || qtdPorPagina <= 0) return BadRequest();
            if (qtdPorPagina > 100) qtdPorPagina = 100;

            var paginacao = await repository.ObterPaginadoAdministracao(pagina, qtdPorPagina, ordem, desc, pesquisa, pesquisaCpf, dataCompraInicial, dataCompraFinal);

            return Ok(mapper.Map<Paginacao<PedidoDTO>>(paginacao));
        }

        [Authorize(Policy = "VisualizarPedido")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            if (id <= 0) return BadRequest();

            var pedido = await repository.Obter(id, false);

            if (pedido is null) return NotFound();

            return Ok(mapper.Map<PedidoDTO>(pedido));
        }

        [Authorize(Policy = "AdicionarSituacaoPedido")]
        [HttpPost("situacao")]
        public async Task<IActionResult> AdicionarSituacao(SituacaoPedidoDTO dto)
        {
            if (dto.PedidoId <= 0 || dto.TipoSituacaoPedido == TipoSituacaoPedido.Recebido) return BadRequest();

            var pedido = await repository.Obter(dto.PedidoId, true);
            if (pedido is null) return NotFound();

            var situacaoAtual = pedido.SituacoesPedido.OrderByDescending(x => x.Id).FirstOrDefault()?.TipoSituacaoPedido;
            if (situacaoAtual is null) return NotFound();

            if (situacaoAtual == TipoSituacaoPedido.Finalizado || situacaoAtual == TipoSituacaoPedido.Cancelado) return BadRequest();
            if (dto.TipoSituacaoPedido != TipoSituacaoPedido.Cancelado)
            {
                if (situacaoAtual == TipoSituacaoPedido.Recebido && dto.TipoSituacaoPedido != TipoSituacaoPedido.Aprovado) return BadRequest();
                if (situacaoAtual == TipoSituacaoPedido.Aprovado && dto.TipoSituacaoPedido != TipoSituacaoPedido.Enviado) return BadRequest();
                if (situacaoAtual == TipoSituacaoPedido.Enviado && dto.TipoSituacaoPedido != TipoSituacaoPedido.Entregue) return BadRequest();
                if (situacaoAtual == TipoSituacaoPedido.Entregue && dto.TipoSituacaoPedido != TipoSituacaoPedido.Finalizado) return BadRequest();
            }

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

            if (dto.TipoSituacaoPedido == TipoSituacaoPedido.Cancelado)
            {
                ICollection<Estoque> estoques = [];
                foreach (var item in pedido.PedidoItens)
                {
                    estoques.Add(
                        new()
                        {
                            Remover = false,
                            ProdutoId = item.ProdutoId,
                            Quantidade = item.Quantidade
                        });
                }

                await estoqueService.AlterarEstoque(estoques);
            }

            return NoContent();
        }
    }
}
