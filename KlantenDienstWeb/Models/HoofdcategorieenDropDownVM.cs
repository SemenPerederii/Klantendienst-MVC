using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlantenDienstWeb.Models
{
    public class HoofdcategorieenDropDownVM
    {
        public int? GeselecteerdeHoofdcategorieId { get; set; }
        public string Naam { get; set; }
        public IEnumerable<SelectListItem> Subcategorieen { get; set; }

    }
}
