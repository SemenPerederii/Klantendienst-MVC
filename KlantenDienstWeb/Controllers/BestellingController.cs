using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class BestellingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            // Logica om de bestelling met de opgegeven id op te halen
            return View();
        }
    }
}
