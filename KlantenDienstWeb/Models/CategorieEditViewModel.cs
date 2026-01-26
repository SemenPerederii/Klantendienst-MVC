using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KlantenDienstWeb.Models
{
    public class CategorieEditViewModel
    {
        public int CategorieId { get; set; }
        [Required(ErrorMessage = "Naam is verplicht")]
        [StringLength(100, ErrorMessage = "Naam mag maximaal 100 tekens bevatten")]
        public string? Naam { get; set; }
        public int? SelectedHoofdCategorieId { get; set; }
        public List<SelectListItem> MogelijkeCategorieen { get; set; } = new();
        public int? HoofdCategorieId { get; set; }

    }
}
