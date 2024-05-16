﻿using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Services.Interfaces;

namespace LojaVirtual.Site.Services
{
    public class PagamentoService(HttpClient httpClient, IConfiguration configuration) : IPagamentoService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly string baseAddress = configuration.GetValue<string>("Services:Pagamentos") ?? string.Empty;

        public async Task ProcessarPagamento(Pagamento pagamento)
        {
            await httpClient.GetAsync($"{baseAddress}?pedidoid={pagamento.PedidoId}&tipo={pagamento.Tipo}");
        }
    }
}