using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KlantenDienstData.Models;

public partial class Artikel
{
    public int ArtikelId { get; set; }

    public string EAN { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public string Beschrijving { get; set; } = null!;
    [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode =true)]
    [Range(0, 1000000, ErrorMessage = "buiten range")]
    public decimal Prijs { get; set; }
    [Range(0, 10000000, ErrorMessage = "buiten range")]
    [Display(Name = "Gewicht in gram")]
    public int GewichtInGram { get; set; }
    [Range(0, 1000000, ErrorMessage = "buiten range")]
    public int Bestelpeil { get; set; }

    public int Voorraad { get; set; }
    [Range(0, 1000000, ErrorMessage = "buiten range")]
    [Display(Name = "Minimum vorraad")]
    public int MinimumVoorraad { get; set; }
    [Range(0, 1000000, ErrorMessage = "buiten range")]
    [Display(Name = "Maximum vorraad")]
    public int MaximumVoorraad { get; set; }
    [Range(0, 1000000, ErrorMessage = "buiten range")]
    public int Levertijd { get; set; }

    [Display(Name ="Aantal Besteld Leverancier")]
    public int AantalBesteldLeverancier { get; set; }
    [Range(0, 1000000, ErrorMessage = "*")]
    [Display(Name = "Max in magazijnplaats")]
    public int MaxAantalInMagazijnPLaats { get; set; }

    public int LeveranciersId { get; set; }

    public virtual ICollection<ArtikelLeveranciersInfolijn> Artikelleveranciersinfolijnen { get; set; } = new List<ArtikelLeveranciersInfolijn>();

    public virtual ICollection<Bestellijn> Bestellijnen { get; set; } = new List<Bestellijn>();

    public virtual ICollection<InkomendeLeveringslijn> Inkomendeleveringslijnen { get; set; } = new List<InkomendeLeveringslijn>();
    [ForeignKey("LeveranciersId")]
    public virtual Leverancier? Leverancier { get; set; } = null!;

    public virtual ICollection<MagazijnPlaats> Magazijnplaatsen { get; set; } = new List<MagazijnPlaats>();

    public virtual ICollection<VeelgesteldevragenArtikel> Veelgesteldevragenartikels { get; set; } = new List<VeelgesteldevragenArtikel>();

    public virtual ICollection<Wishlistitem> Wishlistitems { get; set; } = new List<Wishlistitem>();

    public virtual ICollection<Categorie> Categorieën { get; set; } = new List<Categorie>();

    public ICollection<ArtikelCategorie> ArtikelCategorieen { get; set; } = new List<ArtikelCategorie>();

}
