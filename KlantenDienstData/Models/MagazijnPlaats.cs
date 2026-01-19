using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class MagazijnPlaats
{
    public int MagazijnPlaatsId { get; set; }

    public int? ArtikelId { get; set; }

    public string Rij { get; set; } = null!;

    public int Rek { get; set; }

    public int aantal { get; set; }

    public virtual Artikel? artikel { get; set; }
}
