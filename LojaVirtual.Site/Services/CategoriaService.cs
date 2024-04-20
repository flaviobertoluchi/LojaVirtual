using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Site.Services
{
    public class CategoriaService(HttpClient httpClient, IConfiguration configuration) : ICategoriaService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly string baseAddress = configuration.GetValue<string>("Services:Catalogo.Categorias") ?? string.Empty;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public async Task<ResponseApi> ObterTodos()
        {
            var response = await httpClient.GetAsync(baseAddress);

            if (response.IsSuccessStatusCode) return new(response.StatusCode, JsonSerializer.Deserialize<ICollection<Categoria>>(await response.Content.ReadAsStringAsync(), options));

            return new(response.StatusCode, response.Content);
        }
    }
}
