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
        public async Task<ActionResult> LogIn(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(LogIn), loginModel);
            var personeelslid = await _accountService.Login(loginModel.Emailadres, loginModel.Paswoord);
            if (personeelslid is null ||
                 (personeelslid.InDienst.HasValue && !personeelslid.InDienst.Value) ||
                 personeelslid.PersoneelslidAccount.Disabled)
            {
                loginModel.ErrorMessage = "Deze gebruiker/paswoord combinatie is fout.";
                return View(nameof(LogIn), loginModel);
            }
            //await SecurityManager.SignIn(this.HttpContext, personeelslid);
            return LocalRedirect(loginModel.ReturnUrl);
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
