using KlantenDienstServices;
using KlantenDienstWeb.Models;
using KlantenDienstWeb.Security;
using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.Controllers
{
    public sealed class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly SecurityManager _securityManager;

        //private AccountService AccountService => _accountService;
        //private SecurityManager SecurityManager => _securityManager;

        public AccountController(AccountService accountService, SecurityManager securityManager)
        {
            _accountService = accountService;
            _securityManager = securityManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> WijzigWachtwoord()
        {
            var sessionVariabel = HttpContext.Session.GetInt32("PersoneelslidId");

            var account = await _accountService.GetPersoneelslidById(sessionVariabel!.Value);

            var accountVM = new PaswoordWijzigenVM
            {
                Account = account!,
                Emailadres = account!.PersoneelslidAccount.Emailadres
            };
            return View(accountVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WachtwoordWijzigenDoorvoeren(PaswoordWijzigenVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("WijzigWachtwoord", model);
            }

            return View();

            // echte logica hier
        }
        public async Task<IActionResult> Logout()
        {
            await _securityManager.SignOut(this.HttpContext);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View(nameof(AccessDenied));
        }
    }
}