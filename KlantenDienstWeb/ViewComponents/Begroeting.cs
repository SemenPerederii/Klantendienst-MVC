using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.ViewComponents
{
    public class Begroeting : ViewComponent
    {
        public IViewComponentResult Invoke(string plaats)
        {
            var sessionVariabeleNaam = HttpContext.Session.GetString("Voornaam");
            if (string.IsNullOrEmpty(sessionVariabeleNaam))
                return View((object)"Voornaam niet gevonden");
            return View((object)$"Welkom {sessionVariabeleNaam}!");
        }
    }
}
