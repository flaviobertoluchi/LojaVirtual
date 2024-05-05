using AutoMapper;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Site.Services
{
    public class CarrinhoService(Cookie cookie, IMapper mapper, IProdutoService produtoService) : ICarrinhoService
    {
        private readonly string cookieKey = "carrinho";
        private readonly Cookie cookie = cookie;
        private readonly IMapper mapper = mapper;
        private readonly IProdutoService produtoService = produtoService;

        public async Task<CarrinhoViewModel?> Obter()
        {
            var model = mapper.Map<CarrinhoViewModel>(ObterCookie());

            foreach (var item in model.CarrinhoItens)
            {
                var response = await produtoService.Obter(item.ProdutoId);
                if (response.Ok())
                {
                    var produto = (Produto)response.Content!;

                    if (item.Quantidade > produto.Estoque)
                    {
                        item.Quantidade = produto.Estoque;
                        model.CarrinhoAlterado = true;
                    }

                    item.Nome = produto.Nome;
                    item.Estoque = produto.Estoque;
                    item.Preco = produto.Preco;
                    item.Imagem = produto.Imagens.Order().FirstOrDefault() ?? string.Empty;
                    item.Total = item.Preco * item.Quantidade;
                }
                else if (response.NotFound())
                {
                    item.Quantidade = 0;
                    model.CarrinhoAlterado = true;
                }
                else
                {
                    return null;
                }
            }

            model.CarrinhoItens = [.. model.CarrinhoItens.Where(x => x.Quantidade > 0)];
            model.QuantidadeItens = model.CarrinhoItens.Sum(x => x.Quantidade);
            model.ValorTotal = model.CarrinhoItens.Sum(x => x.Total);

            if (model.CarrinhoAlterado) cookie.Adicionar(cookieKey, JsonSerializer.Serialize(mapper.Map<Carrinho>(model)));

            return model;
        }

        public Carrinho ObterCookie()
        {
            var cookie = this.cookie.Obter(cookieKey);
            if (cookie is null) return new();

            return JsonSerializer.Deserialize<Carrinho>(cookie) ?? new();
        }

        public void Adicionar(CarrinhoItemViewModel model)
        {
            AlterarItemCarrinho(mapper.Map<CarrinhoItem>(model));
        }

        public void Atualizar(CarrinhoItemViewModel model)
        {
            AlterarItemCarrinho(mapper.Map<CarrinhoItem>(model), true);
        }

        public void Excluir(int id)
        {
            var carrinhoCookie = ObterCookie();

            var carrinhoItem = carrinhoCookie.CarrinhoItens.FirstOrDefault(x => x.ProdutoId == id);

            if (carrinhoItem is not null)
            {
                carrinhoCookie.CarrinhoItens.Remove(carrinhoItem);
                cookie.Adicionar(cookieKey, JsonSerializer.Serialize(carrinhoCookie));
            }
        }

        private void AlterarItemCarrinho(CarrinhoItem carrinhoItem, bool atualizar = false)
        {
            var carrinhoCookie = ObterCookie();

            var carrinhoItemCookie = carrinhoCookie.CarrinhoItens.FirstOrDefault(x => x.ProdutoId == carrinhoItem.ProdutoId);

            if (carrinhoItemCookie is null)
                carrinhoCookie.CarrinhoItens.Add(carrinhoItem);
            else
            {
                if (atualizar)
                    carrinhoItemCookie.Quantidade = carrinhoItem.Quantidade;
                else
                    carrinhoItemCookie.Quantidade += carrinhoItem.Quantidade;
            }

            cookie.Adicionar(cookieKey, JsonSerializer.Serialize(carrinhoCookie));
        }
    }
}
