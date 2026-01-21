using KlantenDienstData.Models;
using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace KlantenDienstWeb.Controllers
{
    public class ArtikelController : Controller
    {
        private readonly ArtikelService _artikelService;
        public ArtikelController(ArtikelService artikelService)
        {
            _artikelService = artikelService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var artikelVM = new ArtikelViewModel()
            {
                Artikelen = await _artikelService.GetAllArtikelenAsync()
            };
            return View(artikelVM);
        }
        [HttpGet]
        public async Task<IActionResult> ToevoegFormulier()
        {
            return View(new Artikel());
        }
        [HttpPost]
        public async Task<IActionResult> Toevoegen(Artikel artikel)
        {
            if (artikel == null)
            {
                return RedirectToAction(nameof(ToevoegFormulier));
            }
            if (!this.ModelState.IsValid)
            {
                return View(nameof(ToevoegFormulier), artikel);
            }
            //toevoegen
            await _artikelService.VoegArtikelToeAsync(artikel);
            return RedirectToAction(nameof(Index));
        }
    }
}
