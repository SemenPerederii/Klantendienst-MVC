using Microsoft.AspNetCore.Mvc.Rendering;

public class CategorieDropdownViewModel
{
    public string Naam { get; set; }
    public int? GeselecteerdeHoofdcategorieId { get; set; }
    public IEnumerable<SelectListItem> Categorieen { get; set; }

    public List<int> GeselecteerdeSubCategorieIds { get; set; } = new();
    public IEnumerable<SelectListItem> SubCategorieen { get; set; }
}