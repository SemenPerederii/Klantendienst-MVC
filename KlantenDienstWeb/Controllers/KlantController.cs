using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class KlantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
