using KlantenDienstData.Models;
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
            var artikelVM = new ArtikelViewModel();
            var alleCategorieën = await _categorieService.GetAllCategorieAsync();
            var artikelen = await _artikelService.GetAllArtikelenAsync();
            var alleActieveArtikelen = new List<Artikel>();

            foreach (var artikel in artikelen)
            {
                if (_artikelService.CheckStatusActief(artikel))
                {
                    alleActieveArtikelen.Add(artikel);
                }
            }

            artikelVM.Artikelen = artikelen;
            artikelVM.Categorieën = alleCategorieën.Where(c => c.HoofdCategorieId == null).ToList();
            artikelVM.GeselecteerdeCategorieIds = new List<int>();
            artikelVM.ActieveArtikelen = alleActieveArtikelen;


            return View(artikelVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            var artikel = await _artikelService.GetArtikelByIdAsync(id);
            if (!_artikelService.CheckStatusActief(artikel))
            {
                return RedirectToAction("Index");
            }
            var vm = new ArtikelViewModel
            {
                ArtikelVoorDeactivatie = await _artikelService.GetArtikelByIdAsync(id)
            };
            return View(vm);
        }

    }
}
