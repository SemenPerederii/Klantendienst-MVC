using KlantenDienstData.Models;

namespace KlantenDienstWeb.Models
{
    public class NieuweHoofdcategorieVM
    {
        public Categorie Categorie { get; set; }

        public virtual SubcategorieenChecklistVM? InversehoofdCategorie { get; set; } = null!;
    }
}
