using KlantenDienstData.Models;

namespace KlantenDienstWeb.Models
{
    public class ArtikelViewModel
    {
        public int? Id { get; set; }
        public string? EAN { get; set; } = string.Empty;
        public string? Naam { get; set; } = string.Empty;
        public decimal? MinPrijs { get; set; }
        public decimal? MaxPrijs { get; set; }
        public int? MinGewichtInGram { get; set; }
        public int? MaxGewichtInGram { get; set; }
        public bool InVoorraad { get; set; }
        public List<int> GeselecteerdeCategorieIds { get; set; } = new List<int>();
        public List<Categorie> Categorieën { get; set; } = new List<Categorie>();
        public List<Artikel> Artikelen { get; set; } = new List<Artikel>();
    }
}
