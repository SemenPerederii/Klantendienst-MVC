using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ICategorieService _serviceCategorie;

        public CategorieController(ICategorieService serviceCategorie)
        {
            _serviceCategorie = serviceCategorie;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _serviceCategorie.GetAllCategorieAsync();
            var tree = _serviceCategorie.BuildTree(categories);

            return View(tree);
        }

        [HttpPost]
        public async Task<IActionResult> Verwijderen(int id)
        {
            await _serviceCategorie.DeleteCategorieAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
