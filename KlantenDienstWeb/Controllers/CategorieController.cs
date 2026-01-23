using KlantenDienstData.Models;
using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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
        public async Task<IActionResult> ToevoegenHoofdcategorie()
        {
            var categorieen = await _serviceCategorie.GetHoofdcategorieenAsync();
            var subModel = new SubcategorieenChecklistVM
            {
                SubCategorieen = categorieen.Select(c => new SelectListItem
                {
                    Value = c.CategorieId.ToString(),
                    Text = c.Naam
                }).ToList(),
                GeselecteerdeSubCategorieIds = new List<int>()
            };

            var vm = new NieuweHoofdcategorieVM
            {
                Categorie = new(),
                InversehoofdCategorie = subModel
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToevoegenHoofdcategorie(NieuweHoofdcategorieVM vm)
        {
            if (vm == null)
                return View("Error");

            var selectedSubCategorieIds = vm.InversehoofdCategorie?.GeselecteerdeSubCategorieIds;

            if (!ModelState.IsValid)
            {
                var categorieen = await _serviceCategorie.GetHoofdcategorieenAsync();
                if (vm.InversehoofdCategorie == null)
                    vm.InversehoofdCategorie = new SubcategorieenChecklistVM();

                vm.InversehoofdCategorie.SubCategorieen = categorieen.Select(c => new SelectListItem
                {
                    Value = c.CategorieId.ToString(),
                    Text = c.Naam
                }).ToList();

                return View(vm);
            }

            if (await _serviceCategorie.CategorieBestaatAlAsync(vm.Categorie.Naam))
            {
                ModelState.AddModelError("", "Categorie bestaat al.");

                var categorieen = await _serviceCategorie.GetHoofdcategorieenAsync();
                if (vm.InversehoofdCategorie == null)
                    vm.InversehoofdCategorie = new SubcategorieenChecklistVM();

                vm.InversehoofdCategorie.SubCategorieen = categorieen.Select(c => new SelectListItem
                {
                    Value = c.CategorieId.ToString(),
                    Text = c.Naam
                }).ToList();

                return View(vm);
            }

            await _serviceCategorie.MaakHoofdcategorieAsync(vm.Categorie, selectedSubCategorieIds);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> ToevoegenSubcategorie(int id)
        {
            var hoofdCategorie = await _serviceCategorie.GetCategorieByIdAsync(id);
            if (hoofdCategorie == null) 
            { 
                return RedirectToAction(nameof(Index)); 
            } else
            {
                var categorieen = await _serviceCategorie.GetSubcategorieenAsync(id);
                var subModel = new SubcategorieenChecklistVM
                {
                    SubCategorieen = categorieen.Select(c => new SelectListItem
                    {
                        Value = c.CategorieId.ToString(),
                        Text = c.Naam
                    }).ToList(),
                    GeselecteerdeSubCategorieIds = new List<int>()
                };
                var vm = new NieuweSubcategorieVM
                {
                    HoofdCategorieId = id,
                    Hoofdcategorie = hoofdCategorie,
                    Categorie = new(),
                    Subcategorieen = subModel
                };
                return View(vm);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToevoegenSubcategorie(NieuweSubcategorieVM vm)
        {
            if (vm == null)
                return View("Error");

            var selectedSubCategorieIds = vm.Subcategorieen?.GeselecteerdeSubCategorieIds;

            if (!ModelState.IsValid)
            {
                var categorieen = await _serviceCategorie.GetSubcategorieenAsync(vm.HoofdCategorieId);
                if (vm.Subcategorieen == null)
                    vm.Subcategorieen = new SubcategorieenChecklistVM();

                vm.Subcategorieen.SubCategorieen = categorieen.Select(c => new SelectListItem
                {
                    Value = c.CategorieId.ToString(),
                    Text = c.Naam
                }).ToList();

                return View(vm);
            }

            if (await _serviceCategorie.CategorieBestaatAlAsync(vm.Categorie.Naam))
            {
                ModelState.AddModelError("", "Categorie bestaat al.");

                var categorieen = await _serviceCategorie.GetSubcategorieenAsync(vm.HoofdCategorieId);
                if (vm.Subcategorieen == null)
                    vm.Subcategorieen = new SubcategorieenChecklistVM();

                vm.Subcategorieen.SubCategorieen = categorieen.Select(c => new SelectListItem
                {
                    Value = c.CategorieId.ToString(),
                    Text = c.Naam
                }).ToList();

                return View(vm);
            }

            await _serviceCategorie.MaakSubcategorieAsync(vm.HoofdCategorieId, vm.Categorie, selectedSubCategorieIds);

            return RedirectToAction(nameof(Index));
        }
    }
    
}