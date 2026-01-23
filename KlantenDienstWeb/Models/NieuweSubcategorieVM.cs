using KlantenDienstData.Models;

namespace KlantenDienstWeb.Models
{
    public class NieuweSubcategorieVM
    {
        public int HoofdCategorieId { get; set; }
        public Categorie HoofdCategorie { get; set; }
        public Categorie Categorie { get; set; }
        public SubcategorieenChecklistVM? Subcategorieen { get; set; } = null!;
    }
}
