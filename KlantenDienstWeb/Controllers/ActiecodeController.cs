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
    }
}
