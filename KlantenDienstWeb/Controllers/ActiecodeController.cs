using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;
using KlantenDienstData.Models;
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
        public async Task<IActionResult> Index()
        {
            var actiecodes = await _serviceActiecode.GetAllActiecodesAsync();
            return View(actiecodes);
        }
        public async Task<IActionResult> ToevoegFormulier()
        {
            return View();
        }
        public async Task<IActionResult> Toevoegen(Actiecode actiecode)
        {
            if (!this.ModelState.IsValid)
                return View(nameof(ToevoegFormulier), actiecode);
            await _serviceActiecode.Toevoegen(actiecode);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> WijzigFormulier(int id)
        {
            Actiecode? code = await _serviceActiecode.GetActiecodeByIdAsync(id);
            if (code == null)
                return RedirectToAction(nameof(Index));
            return View(code);
        }
        public async Task<IActionResult> Wijzigen(Actiecode actiecode)
        {
            if (!this.ModelState.IsValid)
                return View(nameof(WijzigFormulier), actiecode);
            await _serviceActiecode.WijzigActieCode(actiecode.ActiecodeId,actiecode);
            return RedirectToAction(nameof(Index));
        }
    }
}
