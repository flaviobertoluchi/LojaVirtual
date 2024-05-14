using LojaVirtual.Pagamentos.Models;
using LojaVirtual.Pagamentos.Models.Tipos;
using LojaVirtual.Pagamentos.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Pagamentos.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PagamentosController(IPagamentoService service) : ControllerBase
    {
        private readonly IPagamentoService service = service;

        [HttpGet]
        public async Task<IActionResult> ProcessarPagamento(int pedidoId, TipoPagamento tipo)
        {
            var pagamento = new Pagamento
            {
                PedidoId = pedidoId,
                Aprovar = tipo switch
                {
                    TipoPagamento.Aprovar => true,
                    TipoPagamento.Recusar => false,
                    _ => new Random().Next(2) == 0,
                }
            };

            await service.ProcessarPagamento(pagamento);

            return NoContent();
        }
    }
}
