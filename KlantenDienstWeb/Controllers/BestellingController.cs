using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class BestellingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
