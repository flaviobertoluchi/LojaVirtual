using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WebApp.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
