using EasyNetQ;
using LojaVirtual.Pedidos.Config;
using LojaVirtual.Pedidos.Data;
using LojaVirtual.Pedidos.Models.Services;
using LojaVirtual.Pedidos.Models.Tipos;

namespace LojaVirtual.Pedidos.Services
{
    public class PagamentoService(IServiceProvider services, IConfiguration configuration, IBus bus) : BackgroundService
    {
        private readonly IAdvancedBus advancedBus = bus.Advanced;
        private readonly IServiceProvider services = services;
        private readonly IConfiguration configuration = configuration;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var queue = await advancedBus.QueueDeclareAsync(configuration.GetValue<string>("RabbitMQPagamento:Queue"), cancellationToken);
            var exchange = await advancedBus.ExchangeDeclareAsync(configuration.GetValue<string>("RabbitMQPagamento:ExchangeName"),
                                                                  configuration.GetValue<string>("RabbitMQPagamento:ExchangeType"),
                                                                  cancellationToken: cancellationToken);

            await advancedBus.BindAsync(exchange, queue, configuration.GetValue<string>("RabbitMQPagamento:RoutingKey"), cancellationToken);

            advancedBus.Consume<string>(queue, async (msg, info) =>
            {
                var pagamento = System.Text.Json.JsonSerializer.Deserialize<Pagamento>(msg.Body);
                if (pagamento is not null)
                {
                    using var scope = services.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IPedidoRepository>();
                    var estoqueService = scope.ServiceProvider.GetRequiredService<IEstoqueService>();

                    var pedido = await repository.Obter(pagamento.PedidoId, true);
                    if (pedido is null) return;

                    var situacaoAtual = pedido.SituacoesPedido.OrderByDescending(x => x.Id).FirstOrDefault()?.TipoSituacaoPedido;
                    if (situacaoAtual is null) return;
                    if (situacaoAtual != TipoSituacaoPedido.Recebido) return;

                    pedido.SituacoesPedido.Add(
                            new()
                            {
                                PedidoId = pagamento.PedidoId,
                                TipoSituacaoPedido = pagamento.Aprovar ? TipoSituacaoPedido.Aprovado : TipoSituacaoPedido.Cancelado,
                                Mensagem = pagamento.Aprovar ? SituacaoPedidoMensagens.Aprovado : SituacaoPedidoMensagens.Cancelado,
                                Data = DateTime.Now,
                            }
                        );

                    await repository.Atualizar(pedido);

                    if (!pagamento.Aprovar)
                    {
                        ICollection<Estoque> estoques = [];
                        foreach (var item in pedido.PedidoItens)
                        {
                            estoques.Add(
                                new()
                                {
                                    Remover = false,
                                    ProdutoId = item.ProdutoId,
                                    Quantidade = item.Quantidade
                                });
                        }

                        await estoqueService.AlterarEstoque(estoques);
                    }
                }
            });
        }
    }
}
