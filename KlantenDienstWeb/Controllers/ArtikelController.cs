using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class ArtikelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
