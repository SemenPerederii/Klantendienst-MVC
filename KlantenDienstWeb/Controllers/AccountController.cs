using KlantenDienstServices;
using KlantenDienstWeb.Models;
using Microsoft.AspNetCore.Mvc;

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

    }
}
