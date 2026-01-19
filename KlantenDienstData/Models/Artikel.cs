using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Artikel
{
    public int artikelId { get; set; }

    public string ean { get; set; } = null!;

    public string naam { get; set; } = null!;

    public string beschrijving { get; set; } = null!;

    public decimal prijs { get; set; }

    public int gewichtInGram { get; set; }

    public int bestelpeil { get; set; }

    public int voorraad { get; set; }

    public int minimumVoorraad { get; set; }

    public int maximumVoorraad { get; set; }

    public int levertijd { get; set; }

    public int aantalBesteldLeverancier { get; set; }

    public int maxAantalInMagazijnPLaats { get; set; }

    public int leveranciersId { get; set; }

    public virtual ICollection<ArtikelLeveranciersInfolijn> artikelleveranciersinfolijnens { get; set; } = new List<ArtikelLeveranciersInfolijn>();

    public virtual ICollection<Bestellijn> bestellijnens { get; set; } = new List<Bestellijn>();

    public virtual ICollection<InkomendeLeveringslijn> inkomendeleveringslijnens { get; set; } = new List<InkomendeLeveringslijn>();

    public virtual leverancier leveranciers { get; set; } = null!;

    public virtual ICollection<MagazijnPlaats> magazijnplaatsens { get; set; } = new List<MagazijnPlaats>();

    public virtual ICollection<veelgesteldevragenartikel> veelgesteldevragenartikels { get; set; } = new List<veelgesteldevragenartikel>();

    public virtual ICollection<wishlistitem> wishlistitems { get; set; } = new List<wishlistitem>();

    public virtual ICollection<Categorie> categories { get; set; } = new List<Categorie>();
}
