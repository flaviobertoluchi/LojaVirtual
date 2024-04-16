using LojaVirtual.WebApp.Libraries;
using LojaVirtual.WebApp.Models.Services;
using LojaVirtual.WebApp.Services.Interfaces;

namespace LojaVirtual.WebApp.Services
{
    public class ClienteService(HttpClient httpClient, IConfiguration configuration, Sessao sessao) : IClienteService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly Sessao sessao = sessao;

        private readonly string baseAddress = configuration.GetValue<string>("Services:ClienteAPI") ?? string.Empty;

        public async Task<ResponseApi> Entrar(string usuario, string senha)
        {
            var response = await httpClient.GetAsync($"{baseAddress}/token?usuario={usuario}&senha={senha}");

            if (response.IsSuccessStatusCode)
            {
                sessao.Adicionar(await response.Content.ReadAsStringAsync());
                return new(response.StatusCode);
            }

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> EntrarPorRefreshToken(string refreshToken)
        {
            var response = await httpClient.GetAsync($"{baseAddress}/refresh-token?refreshToken={refreshToken}");

            if (response.IsSuccessStatusCode)
            {
                sessao.Adicionar(await response.Content.ReadAsStringAsync());
                return new(response.StatusCode);
            }

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task Sair()
        {
            sessao.Excluir();
            await Task.CompletedTask;
        }
    }
}
