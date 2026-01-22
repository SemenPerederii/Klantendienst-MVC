using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.ViewComponents
{
    public class Begroeting : ViewComponent
    {
        public IViewComponentResult Invoke(string plaats)
        {
            var sessionVariabeleVoornaam = HttpContext.Session.GetString("Voornaam");
            var sessionVariabeleFamilienaam = HttpContext.Session.GetString("Familienaam");
            if (string.IsNullOrEmpty(sessionVariabeleVoornaam) && string.IsNullOrEmpty(sessionVariabeleFamilienaam))
                return View((object)String.Empty);
            return View((object)$"Welkom {sessionVariabeleVoornaam} {sessionVariabeleFamilienaam}!");
        }
    }
}
