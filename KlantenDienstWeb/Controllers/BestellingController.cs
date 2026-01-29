using KlantenDienstData.Models;
using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

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
        public async Task< IActionResult> Details(int id)
        {
            // Logica om de bestelling met de opgegeven id op te halen
            Bestelling? bestelling = await _serviceBestelling.GetBestellingAsync(id);
            if (bestelling == null)
                return NotFound();
            ViewBag.KlantNaam = bestelling.Klant.Natuurlijkepersonen != null
                ? $"{bestelling.Klant.Natuurlijkepersonen.Voornaam} {bestelling.Klant.Natuurlijkepersonen.Familienaam}"
                : bestelling.Klant.Rechtspersonen!.Naam;
            return View(bestelling);
        }
    }
}
