using LojaVirtual.Site.Controllers;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaVirtual.Site.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessao = (Sessao)context.HttpContext.RequestServices.GetRequiredService(typeof(Sessao));

            var clienteToken = sessao.ObterClienteToken();
            if (clienteToken is null)
            {
                context.Result = new RedirectToActionResult(nameof(ClienteController.Entrar), "Cliente", new { area = string.Empty, returnUrl = context.HttpContext.Request.Path });
                return;
            }

            if (clienteToken.Validade < DateTime.UtcNow)
            {
                if (string.IsNullOrEmpty(clienteToken.RefreshToken))
                {
                    context.Result = new RedirectToActionResult(nameof(ClienteController.Entrar), "Cliente", new { area = string.Empty, returnUrl = context.HttpContext.Request.Path });
                    return;
                }

                var clienteService = (IClienteService)context.HttpContext.RequestServices.GetRequiredService(typeof(IClienteService));

                var response = clienteService.EntrarPorRefreshToken(clienteToken.RefreshToken).Result;

                if (!response.Ok())
                {
                    context.Result = new RedirectToActionResult(nameof(ClienteController.Entrar), "Cliente", new { area = string.Empty, returnUrl = context.HttpContext.Request.Path });
                    return;
                }
            }
        }
    }
}
