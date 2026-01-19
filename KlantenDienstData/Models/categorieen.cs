using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class categorieen
{
    public int categorieId { get; set; }

    public string naam { get; set; } = null!;

    public int? hoofdCategorieId { get; set; }

    public virtual ICollection<categorieen> InversehoofdCategorie { get; set; } = new List<categorieen>();

    public virtual categorieen? hoofdCategorie { get; set; }

    public virtual ICollection<artikelen> artikels { get; set; } = new List<artikelen>();
}
