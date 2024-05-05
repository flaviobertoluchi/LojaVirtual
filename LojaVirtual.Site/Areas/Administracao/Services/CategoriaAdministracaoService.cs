﻿using AutoMapper;
using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;
using System.Text.Json;

namespace LojaVirtual.Site.Areas.Administracao.Services
{
    public class CategoriaAdministracaoService(HttpClient httpClient, IConfiguration configuration, IMapper mapper, Sessao sessao) : ICategoriaAdministracaoService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMapper mapper = mapper;
        private readonly Sessao sessao = sessao;
        private readonly string baseAddress = configuration.GetValue<string>("Services:Catalogo.Categorias") ?? string.Empty;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public async Task<ResponseApi> ObterPaginado(int pagina, int qtdPorPagina, TipoOrdemCategorias ordem, bool desc, string pesquisa)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/administracao/paginado?pagina={pagina}&qtdPorPagina={qtdPorPagina}&ordem={ordem}&desc={desc}&pesquisa={pesquisa}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<Paginacao<CategoriaViewModel>>(JsonSerializer.Deserialize<Paginacao<Categoria>>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> Obter(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/administracao/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<CategoriaViewModel>(JsonSerializer.Deserialize<Categoria>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, response.Content);
        }

        public async Task<ResponseApi> Adicionar(CategoriaViewModel model)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PostAsJsonAsync($"{baseAddress}/administracao", mapper.Map<Categoria>(model));

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<CategoriaViewModel>(JsonSerializer.Deserialize<Categoria>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Atualizar(int id, CategoriaViewModel model)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.PutAsJsonAsync($"{baseAddress}/administracao/{id}", mapper.Map<Categoria>(model));

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Excluir(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterColaboradorToken()?.BearerToken);

            var response = await httpClient.DeleteAsync($"{baseAddress}/administracao/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
