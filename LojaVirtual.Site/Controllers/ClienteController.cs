using LojaVirtual.Site.Config;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    public class ClienteController(IClienteService service) : Controller
    {
        private readonly IClienteService service = service;

        [Route("entrar")]
        public IActionResult Index(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("entrar")]
        public async Task<IActionResult> Index([FromForm] ClienteViewModel model, string? returnUrl)
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

        [Route("conta")]
        public async Task<IActionResult> Conta()
        {
            var response = await service.ObterSite();

            if (response.Ok()) return View(response.Content);

            return View();
        }

        [Route("conta/senha")]
        public IActionResult ContaSenha()
        {
            return View();
        }

        [HttpPost("conta/senha")]
        public async Task<IActionResult> ContaSenha(string senha, string senhaAtual)
        {
            //TODO
            await Task.CompletedTask;
            return View();
        }

        [Route("conta/email")]
        public async Task<IActionResult> ContaEmail()
        {
            var response = await service.ObterSite();

            if (response.Ok()) return View(((ClienteViewModel)response.Content!).Emails);

            return View();
        }

        [Route("conta/email/adicionar")]
        public IActionResult ContaEmailAdicionar()
        {
            return View();
        }

        [HttpPost("conta/email/adicionar")]
        public async Task<IActionResult> ContaEmailAdicionar([FromForm] EmailViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.ObterSite();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                model.ClienteId = cliente.Id;
                cliente.Emails?.Add(model);

                var responseAtualziacao = await service.AtualizarSite(cliente.Id, cliente);

                if (responseAtualziacao.Ok())
                {
                    TempData["Sucesso"] = Mensagens.AdicionarSucesso;
                    return RedirectToAction(nameof(ContaEmail));
                }

                ViewBag.Mensagem = responseAtualziacao.Content;
                return View(model);
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("conta/email/editar/{id}")]
        public async Task<IActionResult> ContaEmailEditar(int id)
        {
            var response = await service.ObterSite();

            if (response.Ok()) return View(((ClienteViewModel)response.Content!).Emails?.FirstOrDefault(x => x.Id == id));

            return View();
        }

        [HttpPost("conta/email/editar/{id}")]
        public async Task<IActionResult> ContaEmailEditar(int id, [FromForm] EmailViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.ObterSite();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                cliente.Emails = [.. cliente.Emails?.Where(x => x.Id != id)];
                cliente.Emails.Add(model);

                var responseAtualziacao = await service.AtualizarSite(cliente.Id, cliente);

                if (responseAtualziacao.Ok())
                {
                    TempData["Sucesso"] = Mensagens.AtualizarSucesso;
                    return RedirectToAction(nameof(ContaEmail));
                }

                ViewBag.Mensagem = responseAtualziacao.Content;
                return View(model);
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("conta/email/excluir/{id}")]
        public async Task<IActionResult> ContaEmailExcluir(int id)
        {
            var response = await service.ObterSite();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                cliente.Emails = [.. cliente.Emails?.Where(x => x.Id != id)];

                var responseAtualziacao = await service.AtualizarSite(cliente.Id, cliente);

                if (responseAtualziacao.Ok())
                {
                    TempData["Sucesso"] = Mensagens.ExcluirSucesso;
                    return RedirectToAction(nameof(ContaEmail));
                }

                TempData["Mensagem"] = responseAtualziacao.Content;
                return RedirectToAction(nameof(ContaEmailEditar), "Cliente", new { id });
            }

            TempData["Mensagem"] = response.Content;
            return RedirectToAction(nameof(ContaEmailEditar), "Cliente", new { id });
        }
    }
}
