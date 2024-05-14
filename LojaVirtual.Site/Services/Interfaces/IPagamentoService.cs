using LojaVirtual.Site.Models.Services;

namespace LojaVirtual.Site.Services.Interfaces
{
    public interface IPagamentoService
    {
        Task ProcessarPagamento(Pagamento pagamento);
    }
}
