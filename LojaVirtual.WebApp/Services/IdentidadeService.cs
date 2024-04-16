using LojaVirtual.WebApp.Services.Interfaces;

namespace LojaVirtual.WebApp.Services
{
    public class IdentidadeService(HttpClient httpClient, IConfiguration configuration) : IIdentidadeService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly string baseAddress = configuration.GetValue<string>("Services:IdentidadeAPI") ?? string.Empty;

        public async Task<ResponseApi> Entrar(string login, string senha)
        {
            var response = await httpClient.GetAsync($"{baseAddress}/token?login={login}&senha={senha}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
