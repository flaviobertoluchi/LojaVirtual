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

        [Route("atualizar_quantidade")]
        public IActionResult AtualizarQuantidade()
        {
            return ViewComponent("CarrinhoMenu");
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
            if (id <= 0 || id != model.ProdutoId) return BadRequest();
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
