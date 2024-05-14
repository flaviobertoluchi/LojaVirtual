using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Models.Services;
using LojaVirtual.Site.Models.Tipos;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Site.Controllers
{
    [Route("pedidos")]
    [ClienteAutorizacao]
    public class PedidoController(IPedidoService service, IClienteService clienteService, ICarrinhoService carrinhoService, IPagamentoService pagamentoService) : Controller
    {
        private readonly IPedidoService service = service;
        private readonly IClienteService clienteService = clienteService;
        private readonly ICarrinhoService carrinhoService = carrinhoService;
        private readonly IPagamentoService pagamentoService = pagamentoService;

        [Route("novo")]
        public async Task<IActionResult> Adicionar()
        {
            return View(await PreencherModel());
        }

        [HttpPost("novo")]
        public async Task<IActionResult> Adicionar(PedidoAdicionarViewModel pedidoAdicionarViewModel)
        {
            var model = await PreencherModel(pedidoAdicionarViewModel);
            if (model is null) return View(pedidoAdicionarViewModel);

            if (model.Pedido.TipoPagamento == TipoPagamento.NaoInformado)
            {
                ViewBag.Mensagem = "Selecione uma forma de pagamento.";
                return View(model);
            }

            var endereco = model.ClienteViewModel.Enderecos?.FirstOrDefault(x => x.Id == model.EnderecoId);
            if (endereco is null)
            {
                ViewBag.Mensagem = "Endereço não encontrado.";
                return View(model);
            }

            var telefone = model.ClienteViewModel.Telefones?.FirstOrDefault(x => x.Id == model.TelefoneId)?.Numero;
            if (telefone is null)
            {
                ViewBag.Mensagem = "Telefone não encontrado.";
                return View(model);
            }

            var email = model.ClienteViewModel.Emails?.FirstOrDefault(x => x.Id == model.EmailId)?.EmailEndereco;
            if (email is null)
            {
                ViewBag.Mensagem = "Endereço não encontrado.";
                return View(model);
            }

            model.Pedido.Cliente = new()
            {
                ClienteId = model.ClienteViewModel.Id,
                Nome = model.ClienteViewModel.Nome,
                Sobrenome = model.ClienteViewModel.Sobrenome,
                Cpf = model.ClienteViewModel.Cpf,
                Email = email,
                Telefone = telefone,
                EnderecoNome = endereco.EnderecoNome,
                Cep = endereco.Cep,
                Logradouro = endereco.Logradouro,
                EnderecoNumero = endereco.Numero,
                Complemento = endereco.Complemento,
                Cidade = endereco.Cidade,
                Bairro = endereco.Bairro,
                Uf = endereco.Uf
            };

            if (model.PedidoAlterado) return View(model);

            var response = await service.Adicionar(model.Pedido);

            if (response.Ok())
            {
                carrinhoService.LimparCarrinho();
                return RedirectToAction(nameof(Recebido), new { id = ((Pedido)response.Content!).Id });
            }

            ViewBag.Mensagem = response.Content;
            return View(model);

        }

        [Route("recebido/{id}")]
        public async Task<IActionResult> Recebido(int id)
        {
            var responseCliente = await clienteService.Obter();

            if (responseCliente.Ok())
            {
                var cliente = (ClienteViewModel)responseCliente.Content!;
                if (cliente is null) return View();

                var response = await service.Obter(id);
                if (response.Ok())
                {
                    var pedido = (Pedido)response.Content!;
                    if (pedido?.SituacoesPedido.OrderByDescending(x => x.Id).FirstOrDefault()?.TipoSituacaoPedido != TipoSituacaoPedido.Recebido) return View();

                    return View(pedido.Id);
                }

                ViewBag.Mensagem = response.Content;
                return View();
            }

            ViewBag.Mensagem = responseCliente.Content;
            return View();
        }

        [Route("enderecopartial/{id}")]
        public async Task<IActionResult> EnderecoPartial(int id)
        {
            var response = await clienteService.Obter();

            if (response.Ok()) return PartialView("_PedidoEnderecoPartial", ((ClienteViewModel)response.Content!)?.Enderecos?.FirstOrDefault(x => x.Id == id));

            return PartialView("_PedidoEnderecoPartial");
        }

        private async Task<PedidoAdicionarViewModel?> PreencherModel(PedidoAdicionarViewModel? model = null)
        {
            var responseCliente = await clienteService.Obter();

            if (responseCliente.Ok())
            {
                var cliente = (ClienteViewModel)responseCliente.Content!;
                var carrinho = await carrinhoService.Obter();
                if (carrinho is null || cliente is null) return null;

                model ??= new();
                model.ClienteViewModel = cliente;
                if (model.EnderecoId <= 0) model.EnderecoId = cliente.Enderecos?.FirstOrDefault()?.Id ?? 0;
                if (model.TelefoneId <= 0) model.TelefoneId = cliente.Telefones?.FirstOrDefault()?.Id ?? 0;
                if (model.EmailId <= 0) model.EmailId = cliente.Emails?.FirstOrDefault()?.Id ?? 0;

                model.Pedido.QuantidadeItens = carrinho.QuantidadeItens;
                model.Pedido.ValorTotal = carrinho.ValorTotal;
                model.Pedido.PedidoItens = [];

                foreach (var item in carrinho.CarrinhoItens)
                {
                    model.Pedido.PedidoItens.Add(
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

                model.Endereco = cliente.Enderecos?.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.EnderecoNome
                }).ToList() ?? [];

                model.Telefone = cliente.Telefones?.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Numero
                }).ToList() ?? [];

                model.Email = cliente.Emails?.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.EmailEndereco
                }).ToList() ?? [];

                model.PedidoAlterado = carrinho.CarrinhoAlterado;

                return model;
            }

            ViewBag.Mensagem = responseCliente.Content;
            return null;
        }

        [HttpPost("pagamento")]
        public async Task<IActionResult> Pagamento(Pagamento model)
        {
            await pagamentoService.ProcessarPagamento(model);
            return RedirectToAction(nameof(ClientePedidoController.Detalhes), "ClientePedido", new { id = model.PedidoId });
        }
    }
}
