using AutoMapper;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Site.Services
{
    public class ClienteService(HttpClient httpClient, IConfiguration configuration, IMapper mapper, Sessao sessao) : IClienteService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IMapper mapper = mapper;
        private readonly Sessao sessao = sessao;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        private readonly string baseAddress = configuration.GetValue<string>("Services:Clientes") ?? string.Empty;

        public async Task<ResponseApi> Entrar(string usuario, string senha)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseAddress}/token", new { usuario, senha }, options);

            if (response.IsSuccessStatusCode)
            {
                sessao.Adicionar(Sessao.clienteKey, await response.Content.ReadAsStringAsync());
                return new(response.StatusCode);
            }

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> EntrarPorRefreshToken(string refreshToken)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseAddress}/refreshtoken", refreshToken, options);

            if (response.IsSuccessStatusCode)
            {
                sessao.Adicionar(Sessao.clienteKey, await response.Content.ReadAsStringAsync());
                return new(response.StatusCode);
            }

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> Adicionar(ClienteAdicionarViewModel model)
        {
            var cliente = new Cliente()
            {
                Usuario = model.Usuario,
                Senha = model.Senha,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Cpf = model.Cpf,
                Emails = [new Email() { EmailEndereco = model.EmailEndereco }],
                Telefones = [new Telefone() { Numero = model.TelefoneNumero }],
                Enderecos = [new Endereco()
                {
                    EnderecoNome = model.EnderecoNome,
                    Cep = model.Cep,
                    Logradouro = model.Logradouro,
                    Numero = model.EnderecoNumero,
                    Complemento = model.Complemento,
                    Cidade = model.Cidade,
                    Bairro = model.Bairro,
                    Uf = model.Uf
                }]
            };

            var response = await httpClient.PostAsJsonAsync(baseAddress, cliente, options);

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ClienteViewModel>(JsonSerializer.Deserialize<Cliente>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task Sair()
        {
            sessao.Excluir(Sessao.clienteKey);
            await Task.CompletedTask;
        }

        public async Task<ResponseApi> ObterSite()
        {
            var clienteToken = sessao.ObterClienteToken();

            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", clienteToken?.BearerToken);

            var response = await httpClient.GetAsync($"{baseAddress}/site/{clienteToken?.ClienteId}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode, mapper.Map<ClienteViewModel>(JsonSerializer.Deserialize<Cliente>(await response.Content.ReadAsStringAsync(), options)));

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> AtualizarSite(int id, ClienteViewModel model)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterClienteToken()?.BearerToken);

            var response = await httpClient.PutAsJsonAsync($"{baseAddress}/site/{id}", mapper.Map<Cliente>(model));

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseApi> ExcluirSite(int id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", sessao.ObterClienteToken()?.BearerToken);

            var response = await httpClient.DeleteAsync($"{baseAddress}/site/{id}");

            if (response.IsSuccessStatusCode) return new(response.StatusCode);

            return new(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
