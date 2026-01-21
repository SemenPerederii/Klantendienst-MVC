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
        public IActionResult ZoekenOpFilter(ArtikelViewModel avm)
        {
            return View();
        }
    }
}
