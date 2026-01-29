using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class BestellingController : Controller
    {
        private readonly IBestellingService _serviceBestelling;
        public BestellingController(IBestellingService serviceBestelling)
        {
            _serviceBestelling = serviceBestelling;
        }
        public async Task<IActionResult> Index()
        {
            var bestellingen = await _serviceBestelling.GetAllBestellingenAsync();

            return View(bestellingen);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            // Logica om de bestelling met de opgegeven id op te halen
            return View();
        }
    }
}
