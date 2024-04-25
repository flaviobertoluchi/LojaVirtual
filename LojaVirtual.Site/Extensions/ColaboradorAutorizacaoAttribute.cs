using LojaVirtual.Site.Areas.Administracao.Controllers;
using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LojaVirtual.Site.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessao = (Sessao)context.HttpContext.RequestServices.GetRequiredService(typeof(Sessao));

            var colaboradorToken = sessao.ObterColaboradorToken();
            if (colaboradorToken is null)
            {
                context.Result = new RedirectToActionResult(nameof(ColaboradorController.Index), "Colaborador", new { area = "Administracao" });
                return;
            }

            if (colaboradorToken.Validade < DateTime.UtcNow)
            {
                if (string.IsNullOrEmpty(colaboradorToken.RefreshToken))
                {
                    context.Result = new RedirectToActionResult(nameof(ColaboradorController.Index), "Colaborador", new { area = "Administracao" });
                    return;
                }

                var colaboradorService = (IColaboradorService)context.HttpContext.RequestServices.GetRequiredService(typeof(IColaboradorService));

                var response = colaboradorService.EntrarPorRefreshToken(colaboradorToken.RefreshToken).Result;

                if (!response.Ok())
                {
                    context.Result = new RedirectToActionResult(nameof(ColaboradorController.Index), "Colaborador", new { area = "Administracao" });
                    return;
                }
            }
        }
    }
}
