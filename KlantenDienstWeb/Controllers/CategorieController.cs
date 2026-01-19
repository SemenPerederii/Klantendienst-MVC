using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class CategorieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
