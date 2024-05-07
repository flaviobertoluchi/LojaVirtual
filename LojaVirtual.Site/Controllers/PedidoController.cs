using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Site.Controllers
{
    [Route("pedido")]
    [ClienteAutorizacao]
    public class PedidoController(IPedidoService service, IClienteService clienteService, ICarrinhoService carrinhoService) : Controller
    {
        private readonly IPedidoService service = service;
        private readonly IClienteService clienteService = clienteService;
        private readonly ICarrinhoService carrinhoService = carrinhoService;

        public async Task<IActionResult> Adicionar()
        {
            var responseCliente = await clienteService.Obter();

            if (responseCliente.Ok())
            {
                var cliente = (ClienteViewModel)responseCliente.Content!;
                var carrinho = await carrinhoService.Obter();
                if (carrinho is null || cliente is null) return View();

                var pedido = new Pedido()
                {
                    QuantidadeItens = carrinho.QuantidadeItens,
                    ValorTotal = carrinho.ValorTotal,
                    Cliente = new()
                    {
                        ClienteId = cliente.Id,
                        Nome = cliente.Nome,
                        Sobrenome = cliente.Sobrenome,
                        Cpf = cliente.Cpf,
                        Email = cliente.Emails?.OrderBy(x => x.Id).FirstOrDefault()?.EmailEndereco ?? string.Empty,
                        Telefone = cliente.Telefones?.OrderBy(x => x.Id).FirstOrDefault()?.Numero ?? string.Empty
                    }
                };

                foreach (var item in carrinho.CarrinhoItens)
                {
                    pedido.PedidoItens.Add(
                        new()
                        {
                            ProdutoId = item.ProdutoId,
                            Nome = item.Nome,
                            Quantidade = item.Quantidade,
                            Preco = item.Preco,
                            Total = item.Total,
                            Imagem = item.Imagem
                        });
                }

                ViewBag.Endereco = cliente.Enderecos?.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.EnderecoNome
                }).ToList();

            }

            ViewBag.Mensagem = responseCliente.Content;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(Pedido pedido)
        {
            var response = await service.Adicionar(pedido);

            if (response.Ok()) return RedirectToAction(nameof(HomeController.Index), "Home");

            ViewBag.Mensagem = response.Content;
            return View();
        }
    }
}
