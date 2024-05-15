using LojaVirtual.Site.Models;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    [Route("carrinho")]
    public class CarrinhoController(ICarrinhoService service, IProdutoService produtoService) : Controller
    {
        private readonly ICarrinhoService service = service;
        private readonly IProdutoService produtoService = produtoService;

        public async Task<IActionResult> Index()
        {
            return View(await service.Obter());
        }

        [Route("carrinhopartial")]
        public async Task<IActionResult> CarrinhoPartial()
        {
            return PartialView("_CarrinhoPartial", await service.Obter());
        }

        [Route("carrinhomenu")]
        public IActionResult CarrinhoMenu()
        {
            return ViewComponent("CarrinhoMenu");
        }

        [HttpPost("adicionarredirecionar")]
        public IActionResult AdicionarRedirecionar(CarrinhoItemViewModel model)
        {
            service.Adicionar(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] CarrinhoItemViewModel model)
        {
            service.Adicionar(model);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] CarrinhoItemViewModel model)
        {
            if (id <= 0 || model is null || id != model.ProdutoId) return BadRequest();
            service.Atualizar(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            service.Excluir(id);
            return NoContent();
        }
    }
}
