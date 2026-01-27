using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public sealed class ArtikelFilterDto
    {
        public int? Id { get; set; }
        public string? Ean { get; set; }
        public string? Naam { get; set; }
        public string? Beschrijving { get; set; }

        public decimal? MinPrijs { get; set; }
        public decimal? MaxPrijs { get; set; }

        public bool EnkelInVoorraad { get; set; }

        public List<int> CategorieIds { get; set; } = new();
    }

}
