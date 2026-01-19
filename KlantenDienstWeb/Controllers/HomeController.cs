using System.Diagnostics;
using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PrulariaDbContext _context;
        private readonly SQLLoginRepository _repository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var loginKlant = new LoginViewModel();
            return View(loginKlant);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Index(LoginViewModel klant)
        //{
        //    var personeelsLid = _context.PersoneelslidAccount.ToList();
        //    var ingeloggedKlant = klanten
        //        .FirstOrDefault(k => k.Naam.Equals(klant.Naam, StringComparison.CurrentCultureIgnoreCase)
        //        && k.Postcode == klant.Postcode);
        //    if (ingeloggedKlant != null)
        //    {
        //        HttpContext.Session.SetString("KlantNaam", ingeloggedKlant.Naam);
        //        HttpContext.Session.SetString("KlantVoornaam", ingeloggedKlant.Voornaam);
        //        return RedirectToAction("GenreKiezen");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Onbekend klant");
        //        return View(klant);
        //    }
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
