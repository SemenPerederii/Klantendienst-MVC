using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class ArtikelController : Controller
    {
        private readonly ArtikelService _artikelService;
        private readonly ICategorieService _categorieService;
        public ArtikelController(ArtikelService artikelService, ICategorieService categorieService)
        {
            _artikelService = artikelService;
            _categorieService = categorieService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var artikelVM = new ArtikelViewModel()
            {
                Artikelen = await _artikelService.GetAllArtikelenAsync(),
                Categorieën = await _categorieService.GetAllCategorieAsync()
            };
            return View(artikelVM);
        }
        [HttpPost]
        public async Task<IActionResult> ZoekenOpFilterAsync(ArtikelViewModel vm)
        {
            var filter = new ArtikelFilterDto
            {
                Id = vm.Id,
                Ean = vm.EAN, // of vm.Ean, afhankelijk van jouw VM
                Naam = vm.Naam,
                MinPrijs = vm.MinPrijs,
                MaxPrijs = vm.MaxPrijs,
                MinGewichtInGram = vm.MinGewichtInGram,
                MaxGewichtInGram = vm.MaxGewichtInGram,
                EnkelInVoorraad = vm.InVoorraad,
                CategorieIds = vm.GeselecteerdeCategorieIds ?? new List<int>()
            };

            vm.Artikelen = await _artikelService.ZoekArtikelenOpFilterAsync(filter);

            // Cruciaal: categorieën terug vullen, anders zijn je checkboxen weg na post
            vm.Categorieën = await _categorieService.GetAllCategorieAsync();

            return View("Index", vm);
        }
    }
}
