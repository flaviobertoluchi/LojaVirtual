using EasyNetQ;
using LojaVirtual.Pedidos.Models.Services;

namespace LojaVirtual.Pedidos.Services
{
    public class RetirarEstoqueService(IConfiguration configuration, IBus bus) : IRetirarEstoqueService
    {
        private readonly IAdvancedBus advancedBus = bus.Advanced;
        private readonly IConfiguration configuration = configuration;

        public async Task RetirarEstoque(ICollection<RetirarEstoque> retirarEstoques)
        {
            var exchange = await advancedBus.ExchangeDeclareAsync(configuration.GetValue<string>("RabbitMQRetirarEstoque:ExchangeName"),
                                                                  configuration.GetValue<string>("RabbitMQRetirarEstoque:ExchangeType"));

            await advancedBus.PublishAsync(exchange, configuration.GetValue<string>("RabbitMQRetirarEstoque:RoutingKey"), true, new Message<string>(System.Text.Json.JsonSerializer.Serialize(retirarEstoques)));

        }
    }
}
