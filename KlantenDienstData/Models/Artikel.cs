using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KlantenDienstData.Models;

public partial class Artikel
{
    public int ArtikelId { get; set; }

    public string EAN { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public string Beschrijving { get; set; } = null!;
    [DisplayFormat(DataFormatString = "{0:€ #,##0.00}")]
    [Range(0, 1000, ErrorMessage = "Prijs buiten range")]
    public decimal Prijs { get; set; }
    [Range(0, 1000, ErrorMessage = "Gewicht buiten range")]
    public int GewichtInGram { get; set; }

    public int Bestelpeil { get; set; }

    public int Voorraad { get; set; }

    public int MinimumVoorraad { get; set; }

    public int MaximumVoorraad { get; set; }

    public int Levertijd { get; set; }

    public int AantalBesteldLeverancier { get; set; }

    public int MaxAantalInMagazijnPLaats { get; set; }

    public int LeveranciersId { get; set; }

    public virtual ICollection<ArtikelLeveranciersInfolijn> Artikelleveranciersinfolijnen { get; set; } = new List<ArtikelLeveranciersInfolijn>();

    public virtual ICollection<Bestellijn> Bestellijnen { get; set; } = new List<Bestellijn>();

    public virtual ICollection<InkomendeLeveringslijn> Inkomendeleveringslijnen { get; set; } = new List<InkomendeLeveringslijn>();

    public virtual Leverancier Leveranciers { get; set; } = null!;

    public virtual ICollection<MagazijnPlaats> Magazijnplaatsen { get; set; } = new List<MagazijnPlaats>();

    public virtual ICollection<VeelgesteldevragenArtikel> Veelgesteldevragenartikels { get; set; } = new List<VeelgesteldevragenArtikel>();

    public virtual ICollection<Wishlistitem> Wishlistitems { get; set; } = new List<Wishlistitem>();

    public virtual ICollection<Categorie> Categorieën { get; set; } = new List<Categorie>();
}
