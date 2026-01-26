using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

public class SubcategorieenChecklistVM
{
    public List<int>? GeselecteerdeSubCategorieIds { get; set; } = new();
    [ValidateNever]
    public IEnumerable<SelectListItem> SubCategorieen { get; set; }
}