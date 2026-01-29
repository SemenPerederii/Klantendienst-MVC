using KlantenDienstData.Models;
using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;
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
        public async Task<IActionResult> Index(string? naamFilter, string? typeFilter, string sortOrder = "naam_asc")
        {
            //get alle klanten
            var klanten = await _serviceKlant.GetAllKlantenAsync();
            //filters toepassen
            if (!string.IsNullOrWhiteSpace(naamFilter))
            {
                klanten = klanten.Where(k => k.Naam.Contains(naamFilter, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(typeFilter))
            {
                klanten = klanten.Where(k => k.Type.Equals(typeFilter, StringComparison.OrdinalIgnoreCase));
            }

            // sorteren
            klanten = sortOrder switch
            {
                "naam_desc" => klanten.OrderByDescending(k => k.Naam),
                "type_asc" => klanten.OrderBy(k => k.Type).ThenBy(k => k.Naam),
                "type_desc" => klanten.OrderByDescending(k => k.Type).ThenBy(k => k.Naam),
                _ => klanten.OrderBy(k => k.Naam)
            };

            var viewModel = new KlantIndexViewModel
            {
                Klanten = klanten.ToList(),
                NaamFilter = naamFilter,
                TypeFilter = typeFilter,
                SortOrder = sortOrder
            };

            return View(viewModel);
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
