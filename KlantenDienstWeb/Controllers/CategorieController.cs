using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Toevoegen()
        {
            var categorieen = await _serviceCategorie.GetAllCategorieAsync();
            var model = new CategorieDropdownViewModel
            {
                Categorieen = categorieen.Select(c => new SelectListItem
                {
                    Value = c.CategorieId.ToString(),
                    Text = c.Naam
                }),
                SubCategorieen = categorieen.Select(c => new SelectListItem
                {
                    Value = c.CategorieId.ToString(),
                    Text = c.Naam
                })
            };
            return View(model);
        }
    }
}