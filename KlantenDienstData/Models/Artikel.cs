using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Artikel
{
    public int ArtikelId { get; set; }

    public string EAN { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public string Beschrijving { get; set; } = null!;

    public decimal Prijs { get; set; }

    public int GewichtInGram { get; set; }

    public int Bestelpeil { get; set; }

    public int Voorraad { get; set; }

    public int MinimumVoorraad { get; set; }

    public int MaximumVoorraad { get; set; }

    public int Levertijd { get; set; }

    public int AantalBesteldLeverancier { get; set; }

    public int MaxAantalInMagazijnPLaats { get; set; }

    public int LeveranciersId { get; set; }

    public virtual ICollection<ArtikelLeveranciersInfolijn> Artikelleveranciersinfolijnens { get; set; } = new List<ArtikelLeveranciersInfolijn>();

    public virtual ICollection<Bestellijn> Bestellijnens { get; set; } = new List<Bestellijn>();

    public virtual ICollection<InkomendeLeveringslijn> Inkomendeleveringslijnens { get; set; } = new List<InkomendeLeveringslijn>();

    public virtual Leverancier Leveranciers { get; set; } = null!;

    public virtual ICollection<MagazijnPlaats> Magazijnplaatsens { get; set; } = new List<MagazijnPlaats>();

    public virtual ICollection<VeelgesteldevragenArtikel> Veelgesteldevragenartikels { get; set; } = new List<VeelgesteldevragenArtikel>();

    public virtual ICollection<Wishlistitem> wishlistitems { get; set; } = new List<Wishlistitem>();

    public virtual ICollection<Categorie> categories { get; set; } = new List<Categorie>();
}
