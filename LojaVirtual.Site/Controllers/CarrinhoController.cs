using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LojaVirtual.Site.Controllers
{
    [Route("carrinho")]
    public class CarrinhoController(Cookie cookie) : Controller
    {
        private readonly string cookieKey = "carrinho";
        private readonly Cookie cookie = cookie;

        public IActionResult Index()
        {
            var carrinhoCookie = cookie.Obter(cookieKey);
            if (carrinhoCookie is null) return View();

            return View(JsonSerializer.Deserialize<Carrinho>(carrinhoCookie));
        }

        [HttpPost("adicionar")]
        public IActionResult Adicionar([FromBody] CarrinhoItem carrinhoItem)
        {
            AlterarItemCarrinho(carrinhoItem);
            return NoContent();
        }

        [HttpPost("editar")]
        public IActionResult Editar([FromBody] CarrinhoItem carrinhoItem)
        {
            AlterarItemCarrinho(carrinhoItem, true);
            return NoContent();
        }

        [HttpPost("excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            var carrinhoCookie = cookie.Obter(cookieKey);
            if (carrinhoCookie is null) return NoContent();

            var carrinho = JsonSerializer.Deserialize<Carrinho>(carrinhoCookie) ?? new();

            var carrinhoItemCookie = carrinho.CarrinhoItens.FirstOrDefault(x => x.ProdutoId == id);

            if (carrinhoItemCookie is not null)
            {
                carrinho.CarrinhoItens.Remove(carrinhoItemCookie);
                cookie.Adicionar(cookieKey, JsonSerializer.Serialize(carrinho));
            }

            return NoContent();
        }

        private void AlterarItemCarrinho(CarrinhoItem carrinhoItem, bool editar = false)
        {
            var carrinho = new Carrinho();
            var carrinhoCookie = cookie.Obter(cookieKey);
            if (carrinhoCookie is not null) carrinho = JsonSerializer.Deserialize<Carrinho>(carrinhoCookie) ?? new();

            var carrinhoItemCookie = carrinho.CarrinhoItens.FirstOrDefault(x => x.ProdutoId == carrinhoItem.ProdutoId);

            if (carrinhoItemCookie is null)
                carrinho.CarrinhoItens.Add(carrinhoItem);
            else
            {
                if (editar)
                    carrinhoItemCookie.Quantidade = carrinhoItem.Quantidade;
                else
                    carrinhoItemCookie.Quantidade += carrinhoItem.Quantidade;
            }

            cookie.Adicionar(cookieKey, JsonSerializer.Serialize(carrinho));
        }

    }
}
