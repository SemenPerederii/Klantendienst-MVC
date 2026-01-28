using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KlantenDienstWeb.Controllers
{
    public class KlantController : Controller
    {
        private readonly IKlantService _serviceKlant;
        public KlantController(IKlantService serviceKlant)
        {
            _serviceKlant = serviceKlant;
        }
        public async Task<IActionResult> Index()
        {
            var klanten = await _serviceKlant.GetAllKlantenAsync();
            return View(klanten);
        }
    }
}
