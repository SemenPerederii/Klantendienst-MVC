using KlantenDienstServices;
using KlantenDienstServices.DTO_s;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace KlantenDienstWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
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


            var wijzigingsInfo = new PaswoordWijzigingsDto
            {
                Emailadres = model.Emailadres,
                HuidigPaswoord = model.HuidigPaswoord,
                NieuwPaswoord = model.NieuwPaswoord,
            };

            await _accountService.WijzigPaswoord(wijzigingsInfo);
                
            return View();

            // echte logica hier
        }

    }
}
