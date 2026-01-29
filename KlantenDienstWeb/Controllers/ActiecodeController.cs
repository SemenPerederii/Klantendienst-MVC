using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;
using KlantenDienstData.Models;
using KlantenDienstData.Enums;
using KlantenDienstWeb.Models;

namespace KlantenDienstWeb.Controllers
{
    public class ActiecodeController : Controller
    {
        private readonly IActiecodeService _serviceActiecode;

        public ActiecodeController(IActiecodeService serviceActiecode)
        {
            _serviceActiecode = serviceActiecode;
        }
        public async Task<IActionResult> Index(ActiecodeStatus filter = ActiecodeStatus.Geen,
                        string? datum = null,
                        ActiecodeSorteerOpties sorteerOpties = ActiecodeSorteerOpties.Naam,
                        SorteerRichting sorteerRichting = SorteerRichting.Asc)
        {
            DateOnly? parsedDatum = null;
            if (!string.IsNullOrWhiteSpace(datum) && DateOnly.TryParse(datum, out var d))
            {
                parsedDatum = d;
            }
            var actiecodes = await _serviceActiecode.GetAllActiecodesAsync(
                                filter,
                                parsedDatum,
                                sorteerOpties,
                                sorteerRichting);

            var vm = new ActiecodeVM
            {
                Actiecodes = actiecodes,
                SorteerOpties = sorteerOpties,
                SorteerRichting = sorteerRichting,
                HuidigeFilter = filter,
                HuidigeDatum = parsedDatum
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> ToevoegFormulier()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toevoegen(Actiecode actiecode)
        {
            if (!this.ModelState.IsValid)
                return View(nameof(ToevoegFormulier), actiecode);
            await _serviceActiecode.Toevoegen(actiecode);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> WijzigFormulier(int id)
        {
            Actiecode? code = await _serviceActiecode.GetActiecodeByIdAsync(id);
            if (code == null)
                return RedirectToAction(nameof(Index));
            return View(code);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Wijzigen(Actiecode actiecode)
        {
            if (!this.ModelState.IsValid)
                return View(nameof(WijzigFormulier), actiecode);
            await _serviceActiecode.WijzigActieCode(actiecode.ActiecodeId,actiecode);
            return RedirectToAction(nameof(Index));
        }
    }
}
