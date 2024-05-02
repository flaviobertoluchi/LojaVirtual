using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LojaVirtual.Site.ViewComponents
{
    public class CarrinhoViewComponent(Cookie cookie) : ViewComponent
    {
        private readonly Cookie cookie = cookie;
        private readonly string cookieKey = "carrinho";

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carrinhoCookie = cookie.Obter(cookieKey);
            if (carrinhoCookie is null)
            {
                await Task.CompletedTask;
                return View(0);
            }

            await Task.CompletedTask;
            return View(JsonSerializer.Deserialize<Carrinho>(carrinhoCookie)?.QuantidadeItens ?? 0);
        }
    }
}
