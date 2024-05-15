using AutoMapper;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Site.Services
{
    public class CategoriaService(HttpClient httpClient, IConfiguration configuration, IMapper mapper) : ICategoriaService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMapper mapper = mapper;
        private readonly string baseAddress = configuration.GetValue<string>("Services:Catalogo.Categorias") ?? string.Empty;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public async Task<ResponseApi> ObterTodos()
        {
            var response = await httpClient.GetAsync(baseAddress);

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ICollection<CategoriaViewModel>>(JsonSerializer.Deserialize<ICollection<Categoria>>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, response.Content);
        }
    }
}
