using KlantenDienstServices;
using KlantenDienstServices.DTO_s;
using KlantenDienstWeb.Models;
using KlantenDienstWeb.Security;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace KlantenDienstWeb.Controllers
{
    public sealed class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly SecurityManager _securityManager;

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
            var id = HttpContext.Session.GetInt32("PersoneelslidId");

            var account = await _accountService.GetPersoneelslidById(id!.Value);

            var accountVM = new PaswoordWijzigenVM
            {
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
            var id = HttpContext.Session.GetInt32("PersoneelslidId");
            var account = await _accountService.GetPersoneelslidById(id!.Value);

            if (!BCrypt.Net.BCrypt.Verify(model.HuidigPaswoord, account!.PersoneelslidAccount.Paswoord))
            {
                ModelState.AddModelError(nameof(model.HuidigPaswoord), "Huidig paswoord is onjuist.");

                return View("WijzigWachtwoord", model);
            }
            if(BCrypt.Net.BCrypt.Verify(model.NieuwPaswoord, account!.PersoneelslidAccount.Paswoord))
            {
                ModelState.AddModelError(nameof(model.NieuwPaswoord), "Nieuw paswoord mag niet hetzelfde zijn als het huidige paswoord.");
                return View("WijzigWachtwoord", model);
            }

            return View("BevestigWachtwoordWijziging", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WachtwoordWijzigenBevestigen(PaswoordWijzigenVM model)
        {
            var id = HttpContext.Session.GetInt32("PersoneelslidId");
            await _accountService.WijzigPaswoord(id!.Value, model.NieuwPaswoord);
            return RedirectToAction("Landingspagina", "Home");
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