using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KlantenDienstWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PrulariaDbContext _context;
        private readonly PersoneelslidRepository _repository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var loginKlant = new LoginViewModel();
            return View(loginKlant);
        }

        [HttpPost]
        public async Task<ActionResult> Inloggen(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(Inloggen), loginViewModel);
            var personeelslid = await AccountService.Login(loginViewModel.Emailadres,loginViewModel.Paswoord);
            if (personeelslid is null ||
                (personeelslid.InDienst.HasValue && !personeelslid.InDienst.Value) || personeelslid.PersoneelslidAccount.Disabled)
            {
                loginViewModel.ErrorMessage = "Deze gebruiker/paswoord combinatie is fout.";
                return View(nameof(Inloggen), loginViewModel);
            }
            //await SecurityManager.SignIn(this.HttpContext, personeelslid);
            return LocalRedirect(loginViewModel.ReturnUrl);
        }

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
