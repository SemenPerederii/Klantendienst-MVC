using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class ArtikelController : Controller
    {
        private readonly IArtikelService _artikelService;
        private readonly ICategorieService _categorieService;
        public ArtikelController(IArtikelService artikelService, ICategorieService categorieService)
        {
            _artikelService = artikelService;
            _categorieService = categorieService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var alleCategorieën = await _categorieService.GetAllCategorieAsync();

            var artikelVM = new ArtikelViewModel()
            {
                Artikelen = await _artikelService.GetAllArtikelenAsync(),
                Categorieën = alleCategorieën.Where(c => c.HoofdCategorieId == null).ToList(),
                GeselecteerdeCategorieIds = new List<int>()
            };
            return View(artikelVM);
        }
        [HttpPost]
        public async Task<IActionResult> ZoekenOpFilterAsync(ArtikelViewModel vm)
        {
            vm.GeselecteerdeCategorieIds ??= new List<int>();

            var filter = new ArtikelFilterDto
            {
                Id = vm.Id,
                Ean = vm.EAN,
                Naam = vm.Naam,
                MinPrijs = vm.MinPrijs,
                MaxPrijs = vm.MaxPrijs,
                EnkelInVoorraad = vm.InVoorraad,
                CategorieIds = vm.GeselecteerdeCategorieIds
            };

            vm.Artikelen = await _artikelService.ZoekArtikelenOpFilterAsync(filter);

          
            var alleCats = await _categorieService.GetAllCategorieAsync();
            vm.Categorieën = alleCats.Where(c => c.HoofdCategorieId == null).ToList();

            return View("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> ZetArtikelInactief(int id)
        {
            var vm = new ArtikelViewModel
            {
                ArtikelVoorDeactivatie = await _artikelService.GetArtikelByIdAsync(id)
            };
            return View(vm);
        }
    }
}
