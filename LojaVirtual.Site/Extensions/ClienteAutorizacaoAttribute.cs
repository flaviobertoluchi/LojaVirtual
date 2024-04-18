using LojaVirtual.WebApp.Controllers;
using LojaVirtual.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaVirtual.WebApp.Extensions
{
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessao = (Sessao)context.HttpContext.RequestServices.GetRequiredService(typeof(Sessao));

            var clienteToken = sessao.ObterClienteToken();
            if (clienteToken == null)
            {
                context.Result = new RedirectToActionResult(nameof(ClienteController.Index), "Cliente", new { area = string.Empty });
                return;
            }

            if (clienteToken.Validade < DateTime.UtcNow)
            {
                if (string.IsNullOrEmpty(clienteToken.RefreshToken))
                {
                    context.Result = new RedirectToActionResult(nameof(ClienteController.Index), "Cliente", new { area = string.Empty });
                    return;
                }

                var clienteService = (IClienteService)context.HttpContext.RequestServices.GetRequiredService(typeof(IClienteService));

                var response = clienteService.EntrarPorRefreshToken(clienteToken.RefreshToken).Result;

                if (!response.Ok())
                {
                    context.Result = new RedirectToActionResult(nameof(ClienteController.Index), "Cliente", new { area = string.Empty });
                    return;
                }
            }
        }
    }
}
