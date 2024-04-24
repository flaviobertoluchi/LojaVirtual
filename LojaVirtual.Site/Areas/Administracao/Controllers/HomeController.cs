using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Site.Areas.Administracao.Controllers
{
    [Area("Administracao")]
    [Route("administracao")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
