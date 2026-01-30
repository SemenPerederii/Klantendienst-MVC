using Microsoft.AspNetCore.Mvc;

namespace KlantenDienstWeb.ViewComponents
{
    public class NavbarKnoppen : ViewComponent
    {
        public IViewComponentResult Invoke(string plaats)
        {
            var id = HttpContext.Session.GetInt32("PersoneelslidId");
            var vn = HttpContext.Session.GetString("Voornaam");
            var fn = HttpContext.Session.GetString("Familienaam");
            if (string.IsNullOrEmpty(vn) || string.IsNullOrEmpty(fn) || id == null || id == 0)
                return View("InlogNav");
            // Get the current controller name
            ViewBag.controller = ViewContext.RouteData.Values["controller"]?.ToString();
            return View();
        }
    }
}
