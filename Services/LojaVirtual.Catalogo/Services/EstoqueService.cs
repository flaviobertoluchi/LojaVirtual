using EasyNetQ;
using LojaVirtual.Catalogo.Models.Services;
using LojaVirtual.Produtos.Data;

namespace LojaVirtual.Catalogo.Services
{
    public class EstoqueService(IServiceProvider services, IConfiguration configuration, IBus bus) : BackgroundService
    {
        private readonly IAdvancedBus advancedBus = bus.Advanced;
        private readonly IServiceProvider services = services;
        private readonly IConfiguration configuration = configuration;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var queue = await advancedBus.QueueDeclareAsync(configuration.GetValue<string>("RabbitMQEstoque:Queue"), cancellationToken);
            var exchange = await advancedBus.ExchangeDeclareAsync(configuration.GetValue<string>("RabbitMQEstoque:ExchangeName"),
                                                                  configuration.GetValue<string>("RabbitMQEstoque:ExchangeType"),
                                                                  cancellationToken: cancellationToken);

            await advancedBus.BindAsync(exchange, queue, configuration.GetValue<string>("RabbitMQEstoque:RoutingKey"), cancellationToken);

            advancedBus.Consume<string>(queue, async (msg, info) =>
            {
                var estoques = System.Text.Json.JsonSerializer.Deserialize<ICollection<Estoque>>(msg.Body);
                if (estoques is not null)
                {
                    using var scope = services.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IProdutoRepository>();

                    foreach (var item in estoques)
                    {
                        var produto = await repository.Obter(item.ProdutoId, true);
                        if (produto is not null)
                        {
                            if (item.Remover)
                                produto.Estoque -= item.Quantidade;
                            else
                                produto.Estoque += item.Quantidade;

                            await repository.Atualizar(produto);
                        }
                    }
                }
            });
        }
    }
}
