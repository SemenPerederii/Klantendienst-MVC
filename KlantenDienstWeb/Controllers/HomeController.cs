using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KlantenDienstWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccountService _accountService;

        public HomeController(ILogger<HomeController> logger, AccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inloggen(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), loginViewModel);
            }
            var account = await _accountService.Login(loginViewModel.Emailadres, loginViewModel.Paswoord);
            if (account == null)
            {
                loginViewModel.ErrorMessage = "Deze gebruiker/paswoord combinatie is fout.";
                return View(nameof(Index), loginViewModel);
            }
            if (account.Disabled)
            {
                loginViewModel.ErrorMessage = "Deze gebruiker/paswoord combinatie is fout.";
                return View(nameof(Index), loginViewModel);
            }
            var heeftActiefPersoneelslid = account.Personeelsleden.Any(p => p.InDienst == true);
            if (!heeftActiefPersoneelslid)
            {
                loginViewModel.ErrorMessage = "Deze gebruiker/paswoord combinatie is fout.";
                return View(nameof(Index), loginViewModel);
            }
            // TODO: cookies
            // await SecurityManager.SignIn(HttpContext, account);
            return RedirectToAction(nameof(Landingspagina)); //naar landingspagina leiden
        }

        public IActionResult Landingspagina()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
