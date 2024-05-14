using EasyNetQ;
using LojaVirtual.Catalogo.Models.Services;
using LojaVirtual.Produtos.Data;

namespace LojaVirtual.Catalogo.Services
{
    public class RetirarEstoqueService(IServiceProvider services, IConfiguration configuration, IBus bus) : BackgroundService
    {
        private readonly IAdvancedBus advancedBus = bus.Advanced;
        private readonly IServiceProvider services = services;
        private readonly IConfiguration configuration = configuration;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var queue = await advancedBus.QueueDeclareAsync(configuration.GetValue<string>("RabbitMQRetirarEstoque:Queue"), cancellationToken);
            var exchange = await advancedBus.ExchangeDeclareAsync(configuration.GetValue<string>("RabbitMQRetirarEstoque:ExchangeName"),
                                                                  configuration.GetValue<string>("RabbitMQRetirarEstoque:ExchangeType"),
                                                                  cancellationToken: cancellationToken);

            await advancedBus.BindAsync(exchange, queue, configuration.GetValue<string>("RabbitMQRetirarEstoque:RoutingKey"), cancellationToken);

            advancedBus.Consume<string>(queue, async (msg, info) =>
            {
                var retirarEstoques = System.Text.Json.JsonSerializer.Deserialize<ICollection<RetirarEstoque>>(msg.Body);
                if (retirarEstoques is not null)
                {
                    using var scope = services.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IProdutoRepository>();

                    foreach (var item in retirarEstoques)
                    {
                        var produto = await repository.Obter(item.ProdutoId);
                        if (produto is not null)
                        {
                            produto.Estoque -= item.Quantidade;
                            await repository.Atualizar(produto);
                        }
                    }
                }
            });
        }
    }
}
