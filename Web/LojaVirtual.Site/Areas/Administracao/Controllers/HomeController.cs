using LojaVirtual.Site.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Areas.Administracao.Controllers
{
    [Area("Administracao")]
    [Route("administracao")]
    [ColaboradorAutorizacao]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
