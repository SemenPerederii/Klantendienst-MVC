using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
