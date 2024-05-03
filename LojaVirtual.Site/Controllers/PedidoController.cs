using LojaVirtual.Site.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Controllers
{
    [Route("pedido")]
    [ClienteAutorizacao]
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
