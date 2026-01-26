using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Categorie
{
    public int CategorieId { get; set; }

    public string Naam { get; set; } = null!;

    public int? HoofdCategorieId { get; set; }

    public virtual ICollection<Categorie>? InversehoofdCategorie { get; set; } = new List<Categorie>();

    public virtual Categorie? HoofdCategorie { get; set; }

    public virtual ICollection<Artikel>? Artikelen { get; set; } = new List<Artikel>();
    public virtual ICollection<Artikel> Artikelen { get; set; } = new List<Artikel>();

    public ICollection<ArtikelCategorie> ArtikelCategorieen { get; set; } = new List<ArtikelCategorie>();
}
