using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Models
{
    public class ArtikelCategorie
    {
        public int ArtikelId { get; set; }
        public Artikel Artikel { get; set; } = null!;

        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; } = null!;
    }
}
