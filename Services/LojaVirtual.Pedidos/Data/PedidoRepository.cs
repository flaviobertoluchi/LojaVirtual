using LojaVirtual.Pedidos.Models;

namespace LojaVirtual.Pedidos.Data
{
    public class PedidoRepository(SqlServerContext context) : IPedidoRepository
    {
        private readonly SqlServerContext context = context;

        public async Task Adicionar(Pedido pedido)
        {
            context.Add(pedido);
            await context.SaveChangesAsync();
        }
    }
}
