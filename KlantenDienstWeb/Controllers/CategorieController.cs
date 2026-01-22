using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Wijzigen(int id)
        {
            var categorie = await _serviceCategorie.GetByIdAsync(id);

            if (categorie == null)
                return NotFound();

            var mogelijkeCategorieen = await _serviceCategorie.GetMogelijkeCategorieenAsync(id);

            var vm = new CategorieEditViewModel
            {
                CategorieId = categorie.CategorieId,
                Naam = categorie.Naam,
                SelectedHoofdCategorieId = categorie.HoofdCategorieId,
                MogelijkeCategorieen = mogelijkeCategorieen.Select(c => new SelectListItem
                {
                    Value = c.CategorieId.ToString(),
                    Text = c.Naam
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Wijzigen(CategorieEditViewModel model)
        {
            if (!ModelState.IsValid)
            {

                var categorieen = await _serviceCategorie
                    .GetMogelijkeCategorieenAsync(model.CategorieId);

                model.MogelijkeCategorieen = categorieen
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategorieId.ToString(),
                        Text = c.Naam
                    })
                    .ToList();

                return View(model);
            }

            try
            {
                await _serviceCategorie.AddAsSubcategorieAsync(
                    model.CategorieId,
                    model.Naam,
                    model.SelectedHoofdCategorieId
                );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(model.SelectedHoofdCategorieId), ex.Message);


                var categorieen = await _serviceCategorie
                    .GetMogelijkeCategorieenAsync(model.CategorieId);

                model.MogelijkeCategorieen = categorieen
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategorieId.ToString(),
                        Text = c.Naam
                    })
                    .ToList();

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
