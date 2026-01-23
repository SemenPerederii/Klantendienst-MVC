using KlantenDienstData.Models;

namespace KlantenDienstWeb.Models
{
    public class ArtikelToevoegViewModel
    {
        public Artikel Artikel { get; set; }
        public List<SimpeleCategorie> Categorieën { get; set; }
    }
}
