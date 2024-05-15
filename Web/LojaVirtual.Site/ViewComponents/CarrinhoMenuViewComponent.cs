using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.ViewComponents
{
    public class CarrinhoMenuViewComponent(ICarrinhoService service) : ViewComponent
    {
        private readonly ICarrinhoService service = service;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View(service.ObterCookie().CarrinhoItens.Sum(x => x.Quantidade));
        }
    }
}
