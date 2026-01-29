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
        [HttpGet]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactiveerAccountDoorvoeren(int klantId, int accountId)
        {
            await _serviceKlant.DisableAccountAsync(accountId);
            return RedirectToAction("Details", new { id = klantId });
        }
        [HttpGet]
        public async Task<IActionResult> HeractiveerAccount(int klantId,int accountId,string email)
        {
            ViewBag.klantEmail = email;
            ViewBag.klantId = klantId;
            ViewBag.accountId = accountId;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeactiveerAccount(int klantId, int accountId, string email)
        {
            ViewBag.klantEmail = email;
            ViewBag.klantId = klantId;
            ViewBag.accountId = accountId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HeractiveerAccountDoorvoeren(int klantId, int accountId)
        {
            await _serviceKlant.EnableAccountAsync(accountId);
            return RedirectToAction("Details", new { id = klantId });
        }
    }
}
