﻿using LojaVirtual.Site.Config;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    [Route("cliente/endereco")]
    [ClienteAutorizacao]
    public class ClienteEnderecoController(IClienteService service) : Controller
    {
        private readonly IClienteService service = service;

        public async Task<IActionResult> Index()
        {
            var response = await service.Obter();

            if (response.Ok()) return View(((ClienteViewModel)response.Content!).Enderecos);

            return View();
        }

        [Route("adicionar")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromForm] EnderecoViewModel model)
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
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Mensagem = responseAtualziacao.Content;
                return View(model);
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var response = await service.Obter();

            if (response.Ok()) return View(((ClienteViewModel)response.Content!).Enderecos?.FirstOrDefault(x => x.Id == id));

            return View();
        }

        [HttpPost("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromForm] EnderecoViewModel model)
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
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Mensagem = responseAtualziacao.Content;
                return View(model);
            }

            ViewBag.Mensagem = response.Content;
            return View(model);
        }

        [Route("excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
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
                    return RedirectToAction(nameof(Index));
                }

                TempData["Mensagem"] = responseAtualziacao.Content;
                return RedirectToAction(nameof(Editar), "ClienteEndereco", new { id });
            }

            TempData["Mensagem"] = response.Content;
            return RedirectToAction(nameof(Editar), "ClienteEndereco", new { id });
        }
    }
}