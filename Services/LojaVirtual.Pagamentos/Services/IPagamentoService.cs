using LojaVirtual.Pagamentos.Models;

namespace LojaVirtual.Pagamentos.Services
{
    public interface IPagamentoService
    {
        Task ProcessarPagamento(Pagamento pagamento);
    }
}
