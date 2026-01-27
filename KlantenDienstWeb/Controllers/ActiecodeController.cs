using KlantenDienstServices;
using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class ActiecodeController : Controller
    {
        private readonly ActiecodeService _serviceActiecode;

        public ActiecodeController(ActiecodeService serviceActiecode)
        {
            _serviceActiecode = serviceActiecode;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
