using LojaVirtual.Site.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Areas.Administracao.Controllers
{
    [Area("Administracao")]
    [Route("administracao/pedidos")]
    [ColaboradorAutorizacao]
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
