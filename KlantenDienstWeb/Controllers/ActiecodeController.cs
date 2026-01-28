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

        [HttpGet]
        public async Task<IActionResult> Filter(string filter, DateOnly? datum)
        {
            var vandaag = DateOnly.FromDateTime(DateTime.Today);
            IEnumerable<Actiecode> actiecodes;

            switch (filter)
            {
                case "ActiefNu":
                    actiecodes = await _serviceActiecode.GetActiefOpDatumAsync(vandaag);
                    break;

                case "NietActiefNu":
                    actiecodes = await _serviceActiecode.GetNietActieveVandaagAsync(vandaag);
                    break;

                case "ExacteDatum":
                    if (datum == null)
                        return RedirectToAction(nameof(Index));

                    actiecodes = await _serviceActiecode.GetActiefOpDatumAsync(datum.Value);
                    break;

                default:
                    return RedirectToAction(nameof(Index));
            }

            ViewBag.Filter = filter;
            ViewBag.Datum = datum;

            return View("Index", actiecodes);
        }
    }
}
