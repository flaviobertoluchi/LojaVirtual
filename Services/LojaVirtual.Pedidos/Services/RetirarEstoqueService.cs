using EasyNetQ;
using LojaVirtual.Pedidos.Models.Services;

namespace LojaVirtual.Pedidos.Services
{
    public class RetirarEstoqueService(IBus bus) : IRetirarEstoqueService
    {
        private readonly IAdvancedBus advancedBus = bus.Advanced;

        public async Task RetirarEstoque(ICollection<RetirarEstoque> retirarEstoques)
        {
            var exchange = await advancedBus.ExchangeDeclareAsync("LojaVirtual", "topic");
            await advancedBus.PublishAsync(exchange, "RetirarEstoque", true, new Message<string>(System.Text.Json.JsonSerializer.Serialize(retirarEstoques)));

        }
    }
}
