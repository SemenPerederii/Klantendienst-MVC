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
        private readonly LeverancierService _leverancierService;
        private readonly ICategorieService _categorieService;
        public ArtikelController(ArtikelService artikelService, LeverancierService leverancierService, ICategorieService categorieService)
        {
            _artikelService = artikelService;
            _leverancierService = leverancierService;
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
        [HttpGet]
        public async Task<IActionResult> ToevoegFormulier()
        {
            ArtikelToevoegViewModel artikelToevoegViewModel = new ArtikelToevoegViewModel();
            var categorieën = await _categorieService.GetAllCategorieAsync();
            artikelToevoegViewModel.Categorieën = new List<SimpeleCategorie>();
            foreach (var categorie in categorieën)
            {
                artikelToevoegViewModel.Categorieën.Add(new SimpeleCategorie
                {
                    Id = categorie.CategorieId,
                    Naam = categorie.Naam,
                    Gekozen = false
                });
            }
            //EAN ZETTEN
            List<Artikel> artikelen = await _artikelService.GetAllArtikelenAsync();

            var EANstring = artikelen.Last().EAN;
            long EANNummer;
            if (long.TryParse(EANstring, out EANNummer))
            {
                EANNummer++;
            }
            Artikel leegArtikel = new Artikel
            {
                EAN = EANNummer.ToString()
            };
            artikelToevoegViewModel.Artikel = leegArtikel;
            return View(artikelToevoegViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toevoegen(ArtikelToevoegViewModel artikelToevoegViewModel)
        {
            if (artikelToevoegViewModel == null)
            {
                return RedirectToAction(nameof(ToevoegFormulier));
            }
            //leverancier toekennen
            Leverancier? leverancier = await _leverancierService.GetLeverancierAsync(artikelToevoegViewModel.Artikel.LeveranciersId);
            if (leverancier != null)
                artikelToevoegViewModel.Artikel.Leverancier = leverancier;

            if (!this.ModelState.IsValid)
            {
                return View(nameof(ToevoegFormulier), artikelToevoegViewModel);
            }
            foreach (var categorie in artikelToevoegViewModel.Categorieën)
            {
                if (categorie.Gekozen == true)
                {
                    Categorie? volledgeCategorie = await _categorieService.GetCategorieAsync(categorie.Id);
                    if (volledgeCategorie != null)
                        artikelToevoegViewModel.Artikel.Categorieën.Add(volledgeCategorie);
                }
            }
            //toevoegen
            await _artikelService.VoegArtikelToeAsync(artikelToevoegViewModel.Artikel);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> WijzigFormulier(int id)
        {
            ArtikelToevoegViewModel viewModel = new ArtikelToevoegViewModel();
            Artikel? artikel = await _artikelService.GetArtikelAsync(id);
            if (artikel == null)
                return RedirectToAction(nameof(Index));
            viewModel.Artikel = artikel;
            var mogelijkeCategorieën = await _categorieService.GetAllCategorieAsync();
            viewModel.Categorieën = new List<SimpeleCategorie>();
            foreach (var categorie in mogelijkeCategorieën)
            {
                SimpeleCategorie simpeleCategorie = new SimpeleCategorie
                {
                    Id = categorie.CategorieId,
                    Naam = categorie.Naam,
                    Gekozen=false
                };
                if (viewModel.Artikel.Categorieën.Contains(categorie))
                    simpeleCategorie.Gekozen = true;
                viewModel.Categorieën.Add(simpeleCategorie);
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Wijzig(ArtikelToevoegViewModel artikelToevoegViewModel)
        {
            if(artikelToevoegViewModel == null)
            {
                return RedirectToAction(nameof(WijzigFormulier));
            }
            //leverancier toekennen
            Leverancier? leverancier = await _leverancierService.GetLeverancierAsync(artikelToevoegViewModel.Artikel.LeveranciersId);
            if (leverancier != null)
                artikelToevoegViewModel.Artikel.Leverancier = leverancier;
            //als niet valid, keer terug naar formulier
            if (!this.ModelState.IsValid)
            {
                return View(nameof(WijzigFormulier), artikelToevoegViewModel);
            }
            //pas de gekozen categorieën aan
            foreach (var categorie in artikelToevoegViewModel.Categorieën)
            {
                if (categorie.Gekozen == true)
                {
                    Categorie? volledgeCategorie = await _categorieService.GetCategorieAsync(categorie.Id);
                    if (volledgeCategorie != null)
                        artikelToevoegViewModel.Artikel.Categorieën.Add(volledgeCategorie);
                }
            }

            //save changes
            await _artikelService.WijzigArtikelAsync(artikelToevoegViewModel.Artikel.ArtikelId,artikelToevoegViewModel.Artikel);
            return RedirectToAction(nameof(Index));
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
    }
}
