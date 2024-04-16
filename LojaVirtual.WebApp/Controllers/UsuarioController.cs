using LojaVirtual.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] UsuarioViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}
