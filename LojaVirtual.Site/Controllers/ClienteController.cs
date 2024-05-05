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
            var response = await service.Obter();

            if (response.Ok()) return View(response.Content);

            return View();
        }

        [HttpPost("conta/excluir")]
        public async Task<IActionResult> ContaExcluir(ClienteViewModel model)
        {
            var responseEntrar = await service.Entrar(model.Usuario, model.Senha);

            if (responseEntrar.Ok())
            {
                var response = await service.Excluir(model.Id);

                if (response.Ok()) return RedirectToAction(nameof(Sair));

                TempData["Mensagem"] = response.Content;
                return RedirectToAction(nameof(Conta));
            }

            TempData["Mensagem"] = responseEntrar.Content;
            return RedirectToAction(nameof(Conta));
        }

        [Route("conta/senha")]
        public async Task<IActionResult> ContaSenha()
        {
            var response = await service.Obter();

            if (response.Ok()) return View(response.Content);

            return View();
        }

        [HttpPost("conta/senha")]
        public async Task<IActionResult> ContaSenha(ClienteViewModel model, string senhaAtual)
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

        [Route("conta/email")]
        public async Task<IActionResult> ContaEmail()
        {
            var response = await service.Obter();

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

            var response = await service.Obter();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                model.ClienteId = cliente.Id;
                cliente.Emails?.Add(model);

                var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

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
            var response = await service.Obter();

            if (response.Ok()) return View(((ClienteViewModel)response.Content!).Emails?.FirstOrDefault(x => x.Id == id));

            return View();
        }

        [HttpPost("conta/email/editar/{id}")]
        public async Task<IActionResult> ContaEmailEditar(int id, [FromForm] EmailViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.Obter();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                cliente.Emails = [.. cliente.Emails?.Where(x => x.Id != id)];
                cliente.Emails.Add(model);

                var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

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
            var response = await service.Obter();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                cliente.Emails = [.. cliente.Emails?.Where(x => x.Id != id)];

                var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

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

        [Route("conta/telefone")]
        public async Task<IActionResult> ContaTelefone()
        {
            var response = await service.Obter();

            if (response.Ok()) return View(((ClienteViewModel)response.Content!).Telefones);

            return View();
        }

        [Route("conta/telefone/adicionar")]
        public IActionResult ContaTelefoneAdicionar()
        {
            return View();
        }

        [HttpPost("conta/telefone/adicionar")]
        public async Task<IActionResult> ContaTelefoneAdicionar([FromForm] TelefoneViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.Obter();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                model.ClienteId = cliente.Id;
                cliente.Telefones?.Add(model);

                var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

                if (responseAtualziacao.Ok())
                {
                    TempData["Sucesso"] = Mensagens.AdicionarSucesso;
                    return RedirectToAction(nameof(ContaTelefone));
                }

                ViewBag.Mensagem = responseAtualziacao.Content;
                return View(model);
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("conta/telefone/editar/{id}")]
        public async Task<IActionResult> ContaTelefoneEditar(int id)
        {
            var response = await service.Obter();

            if (response.Ok()) return View(((ClienteViewModel)response.Content!).Telefones?.FirstOrDefault(x => x.Id == id));

            return View();
        }

        [HttpPost("conta/telefone/editar/{id}")]
        public async Task<IActionResult> ContaTelefoneEditar(int id, [FromForm] TelefoneViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.Obter();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                cliente.Telefones = [.. cliente.Telefones?.Where(x => x.Id != id)];
                cliente.Telefones.Add(model);

                var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

                if (responseAtualziacao.Ok())
                {
                    TempData["Sucesso"] = Mensagens.AtualizarSucesso;
                    return RedirectToAction(nameof(ContaTelefone));
                }

                ViewBag.Mensagem = responseAtualziacao.Content;
                return View(model);
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("conta/telefone/excluir/{id}")]
        public async Task<IActionResult> ContaTelefoneExcluir(int id)
        {
            var response = await service.Obter();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                cliente.Telefones = [.. cliente.Telefones?.Where(x => x.Id != id)];

                var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

                if (responseAtualziacao.Ok())
                {
                    TempData["Sucesso"] = Mensagens.ExcluirSucesso;
                    return RedirectToAction(nameof(ContaTelefone));
                }

                TempData["Mensagem"] = responseAtualziacao.Content;
                return RedirectToAction(nameof(ContaTelefoneEditar), "Cliente", new { id });
            }

            TempData["Mensagem"] = response.Content;
            return RedirectToAction(nameof(ContaTelefoneEditar), "Cliente", new { id });
        }

        [Route("conta/endereco")]
        public async Task<IActionResult> ContaEndereco()
        {
            var response = await service.Obter();

            if (response.Ok()) return View(((ClienteViewModel)response.Content!).Enderecos);

            return View();
        }

        [Route("conta/endereco/adicionar")]
        public IActionResult ContaEnderecoAdicionar()
        {
            return View();
        }

        [HttpPost("conta/endereco/adicionar")]
        public async Task<IActionResult> ContaEnderecoAdicionar([FromForm] EnderecoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.Obter();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                model.ClienteId = cliente.Id;
                cliente.Enderecos?.Add(model);

                var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

                if (responseAtualziacao.Ok())
                {
                    TempData["Sucesso"] = Mensagens.AdicionarSucesso;
                    return RedirectToAction(nameof(ContaEndereco));
                }

                ViewBag.Mensagem = responseAtualziacao.Content;
                return View(model);
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("conta/endereco/editar/{id}")]
        public async Task<IActionResult> ContaEnderecoEditar(int id)
        {
            var response = await service.Obter();

            if (response.Ok()) return View(((ClienteViewModel)response.Content!).Enderecos?.FirstOrDefault(x => x.Id == id));

            return View();
        }

        [HttpPost("conta/endereco/editar/{id}")]
        public async Task<IActionResult> ContaEnderecoEditar(int id, [FromForm] EnderecoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await service.Obter();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                cliente.Enderecos = [.. cliente.Enderecos?.Where(x => x.Id != id)];
                cliente.Enderecos.Add(model);

                var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

                if (responseAtualziacao.Ok())
                {
                    TempData["Sucesso"] = Mensagens.AtualizarSucesso;
                    return RedirectToAction(nameof(ContaEndereco));
                }

                ViewBag.Mensagem = responseAtualziacao.Content;
                return View(model);
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("conta/endereco/excluir/{id}")]
        public async Task<IActionResult> ContaEnderecoExcluir(int id)
        {
            var response = await service.Obter();

            if (response.Ok())
            {
                var cliente = (ClienteViewModel)response.Content!;
                cliente.Enderecos = [.. cliente.Enderecos?.Where(x => x.Id != id)];

                var responseAtualziacao = await service.Atualizar(cliente.Id, cliente);

                if (responseAtualziacao.Ok())
                {
                    TempData["Sucesso"] = Mensagens.ExcluirSucesso;
                    return RedirectToAction(nameof(ContaEndereco));
                }

                TempData["Mensagem"] = responseAtualziacao.Content;
                return RedirectToAction(nameof(ContaEnderecoEditar), "Cliente", new { id });
            }

            TempData["Mensagem"] = response.Content;
            return RedirectToAction(nameof(ContaEnderecoEditar), "Cliente", new { id });
        }
    }
}
