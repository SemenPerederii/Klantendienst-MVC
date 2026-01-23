using KlantenDienstData.Models;

namespace KlantenDienstWeb.Models
{
    public class NieuweSubcategorieVM
    {
        public int HoofdCategorieId { get; set; }
        public Categorie Hoofdcategorie { get; set; }
        public Categorie Categorie { get; set; }
        public virtual SubcategorieenChecklistVM? Subcategorieen { get; set; } = null!;
    }
}
