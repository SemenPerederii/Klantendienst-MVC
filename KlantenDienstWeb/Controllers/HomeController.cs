using KlantenDienstData.Models;
using KlantenDienstServices;
using KlantenDienstServices.DTO_s;
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
            try
            {
                var loginInfo = new LogInInfoDTO
                {
                    Paswoord = loginViewModel.Paswoord,
                    Emailadres = loginViewModel.Emailadres
                };
                loginInfo.GevondenAccount = await _accountService.Login(loginInfo);

                if (!IsGeldigAccount(loginInfo.GevondenAccount))
                {
                    if (loginInfo.ErrorMessage != null)
                    {
                        loginViewModel.ErrorMessage = loginInfo.ErrorMessage;
                    }
                    else
                    {
                       loginViewModel.ErrorMessage = "Onbekende gebruiker, probeer opnieuw";
                    }
                    return LoginMislukt(loginViewModel);
                }
                var personeelslid = await _accountService.GetPersoneelslidById(loginInfo.GevondenAccount!.PersoneelslidAccountId);
                HttpContext.Session.SetInt32("PersoneelslidId", personeelslid.PersoneelslidAccountId);
                HttpContext.Session.SetString("Voornaam", personeelslid?.Voornaam ?? string.Empty);
                HttpContext.Session.SetString("Familienaam", personeelslid?.Familienaam ?? string.Empty);
                return RedirectToAction(nameof(Landingspagina));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het aanmelden van klant.");
                ModelState.AddModelError(string.Empty, "Er is een fout opgetreden. Probeer het later opnieuw.");
                return View(nameof(Index), loginViewModel);
            }
            // await SecurityManager.SignIn(HttpContext, account);
        }

        private bool IsGeldigAccount(PersoneelslidAccount? account)
        {
            if (account == null || account.Disabled)
            {
                return false;
            }
            return account.Personeelsleden.Any(p => p.InDienst == true);
        }

        private IActionResult LoginMislukt(LoginViewModel model)
        {
            ModelState.AddModelError(string.Empty, model.ErrorMessage);
            return View(nameof(Index), model);
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
