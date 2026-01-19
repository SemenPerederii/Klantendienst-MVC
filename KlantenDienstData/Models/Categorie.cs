using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Categorie
{
    public int categorieId { get; set; }

    public string naam { get; set; } = null!;

    public int? hoofdCategorieId { get; set; }

    public virtual ICollection<Categorie> InversehoofdCategorie { get; set; } = new List<Categorie>();

    public virtual Categorie? hoofdCategorie { get; set; }

    public virtual ICollection<Artikel> artikels { get; set; } = new List<Artikel>();
}
