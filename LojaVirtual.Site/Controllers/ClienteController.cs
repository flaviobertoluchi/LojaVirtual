using LojaVirtual.Site.Config;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    [Route("cliente")]
    public class ClienteController(IClienteService service, IPedidoService pedidoService) : Controller
    {
        private readonly IClienteService service = service;
        private readonly IPedidoService pedidoService = pedidoService;

        public async Task<IActionResult> Index()
        {
            var response = await service.Obter();

            if (response.Ok())
            {
                var responsePedido = await pedidoService.QuantidadePedidosCliente();
                if (responsePedido.Ok()) ViewBag.QuantidadePedidosCliente = responsePedido.Content;

                return View(response.Content);
            }

            return View();
        }

        [Route("entrar")]
        public IActionResult Entrar(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("entrar")]
        public async Task<IActionResult> Entrar([FromForm] ClienteViewModel model, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var response = await service.Entrar(model.Usuario, model.Senha);

            if (response.Ok())
            {
                if (returnUrl is not null) return LocalRedirect(returnUrl);

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("nova-conta")]
        public IActionResult Adicionar(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("nova-conta")]
        public async Task<IActionResult> Adicionar([FromForm] ClienteAdicionarViewModel model, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid) return View(model);

            var response = await service.Adicionar(model);

            if (response.Ok())
            {
                var responseEntrar = await service.Entrar(model.Usuario, model.Senha);

                if (responseEntrar.Ok())
                {
                    if (returnUrl is not null) return LocalRedirect(returnUrl);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("sair")]
        public async Task<IActionResult> Sair()
        {
            await service.Sair();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost("excluir")]
        public async Task<IActionResult> Excluir(ClienteViewModel model)
        {
            var responseEntrar = await service.Entrar(model.Usuario, model.Senha);

            if (responseEntrar.Ok())
            {
                var response = await service.Excluir(model.Id);

                if (response.Ok()) return RedirectToAction(nameof(Sair));

                TempData["Mensagem"] = response.Content;
                return RedirectToAction(nameof(Index));
            }

            TempData["Mensagem"] = responseEntrar.Content;
            return RedirectToAction(nameof(Index));
        }

        [Route("senha")]
        public async Task<IActionResult> Senha()
        {
            var response = await service.Obter();

            if (response.Ok()) return View(response.Content);

            return View();
        }

        [HttpPost("senha")]
        public async Task<IActionResult> Senha(ClienteViewModel model, string senhaAtual)
        {
            var responseEntrar = await service.Entrar(model.Usuario, senhaAtual);

            if (responseEntrar.Ok())
            {
                var response = await service.Obter();

                if (response.Ok())
                {
                    var cliente = (ClienteViewModel)response.Content!;
                    cliente.Senha = model.Senha;

                    var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

                    if (responseAtualziacao.Ok())
                    {
                        TempData["Sucesso"] = Mensagens.AtualizarSucesso;
                        return View();
                    }

                    ViewBag.Mensagem = responseAtualziacao.Content;
                    return View();
                }

                ViewBag.Mensagem = response.Content;
                return View();
            }

            ViewBag.Mensagem = responseEntrar.Content;
            return View();
        }
    }
}
