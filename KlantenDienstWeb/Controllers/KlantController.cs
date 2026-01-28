using KlantenDienstData.Models;
using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KlantenDienstWeb.Controllers
{
    public class KlantController : Controller
    {
        private readonly IKlantService _serviceKlant;
        public KlantController(IKlantService serviceKlant)
        {
            _serviceKlant = serviceKlant;
        }
        public async Task<IActionResult> Index()
        {
            var klanten = await _serviceKlant.GetAllKlantenAsync();
            return View(klanten);
        }
        public async Task<IActionResult> Details(int id)
        {
            var klant = await _serviceKlant.GetKlantAsync(id);
            if (klant == null)
                return NotFound();
            ViewBag.KlantNaam = klant.Natuurlijkepersonen != null
                ? $"{klant.Natuurlijkepersonen.Voornaam} {klant.Natuurlijkepersonen.Familienaam}"
                : klant.Rechtspersonen!.Naam;
            return View(klant);
        }
    }
}
