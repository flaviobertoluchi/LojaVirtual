using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models.Services;
using System.Text.Json;

namespace LojaVirtual.Site.Areas.Administracao.Services
{
    public class ColaboradorService(HttpClient httpClient, IConfiguration configuration, Sessao sessao) : IColaboradorService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly Sessao sessao = sessao;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        private readonly string baseAddress = configuration.GetValue<string>("Services:Colaboradores") ?? string.Empty;

        public async Task<ResponseApi> Entrar(string usuario, string senha)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseAddress}/tokens", new { usuario, senha }, options);

            if (response.IsSuccessStatusCode)
            {
                sessao.Adicionar(Sessao.colaboradorKey, await response.Content.ReadAsStringAsync());
                return new(response.StatusCode);
            }

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> EntrarPorRefreshToken(string refreshToken)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseAddress}/tokens/refreshtoken", refreshToken, options);

            if (response.IsSuccessStatusCode)
            {
                sessao.Adicionar(Sessao.colaboradorKey, await response.Content.ReadAsStringAsync());
                return new(response.StatusCode);
            }

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task Sair()
        {
            sessao.Excluir(Sessao.colaboradorKey);
            await Task.CompletedTask;
        }
    }
}
