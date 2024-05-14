using EasyNetQ;
using LojaVirtual.Pedidos.Models.Services;

namespace LojaVirtual.Pedidos.Services
{
    public class EstoqueService(IConfiguration configuration, IBus bus) : IEstoqueService
    {
        private readonly IAdvancedBus advancedBus = bus.Advanced;
        private readonly IConfiguration configuration = configuration;

        public async Task AlterarEstoque(ICollection<Estoque> estoques)
        {
            var exchange = await advancedBus.ExchangeDeclareAsync(configuration.GetValue<string>("RabbitMQEstoque:ExchangeName"),
                                                                  configuration.GetValue<string>("RabbitMQEstoque:ExchangeType"));

            await advancedBus.PublishAsync(exchange, configuration.GetValue<string>("RabbitMQEstoque:RoutingKey"), true, new Message<string>(System.Text.Json.JsonSerializer.Serialize(estoques)));

        }
    }
}
