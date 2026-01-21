using KlantenDienstData.Models;
using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KlantenDienstWeb.Controllers
{
    public class CategorieController : Controller
    {
        private readonly CategorieService _serviceCategorie;

        public CategorieController(CategorieService serviceCategorie)
        {
            _serviceCategorie = serviceCategorie;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _serviceCategorie.GetAllCategorieAsync();
            var tree = _serviceCategorie.BuildTree(categories);

            return View(tree);
        }
        [HttpGet]
        public async Task<IActionResult> VoegCategorieToe()
        {
            var categories = await _serviceCategorie.GetAllCategorieAsync();
            ViewBag.Categorieen = categories;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VoegCategorieToe(Categorie categorie)
        {
            if (await _serviceCategorie.CategorieBestaatAlAsync(categorie.Naam))
            {
                ModelState.AddModelError(nameof(Categorie.Naam), "Deze categorie bestaat al.");
            }
            if (!ModelState.IsValid)
            {
                return View(categorie);
            }
            await _serviceCategorie.AddCategorieAsync(categorie);
            return RedirectToAction(nameof(Index));
        }
    }
}