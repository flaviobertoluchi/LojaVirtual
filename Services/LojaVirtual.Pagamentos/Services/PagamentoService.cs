
using EasyNetQ;
using LojaVirtual.Pagamentos.Models;

namespace LojaVirtual.Pagamentos.Services
{
    public class PagamentoService(IConfiguration configuration, IBus bus) : IPagamentoService
    {
        private readonly IAdvancedBus advancedBus = bus.Advanced;
        private readonly IConfiguration configuration = configuration;

        public async Task ProcessarPagamento(Pagamento pagamento)
        {
            var exchange = await advancedBus.ExchangeDeclareAsync(configuration.GetValue<string>("RabbitMQPagamento:ExchangeName"),
                                                                  configuration.GetValue<string>("RabbitMQPagamento:ExchangeType"));

            await advancedBus.PublishAsync(exchange, configuration.GetValue<string>("RabbitMQPagamento:RoutingKey"), true, new Message<string>(System.Text.Json.JsonSerializer.Serialize(pagamento)));
        }
    }
}
