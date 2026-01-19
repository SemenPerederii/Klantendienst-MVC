using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class MagazijnPlaats
{
    public int magazijnPlaatsId { get; set; }

    public int? artikelId { get; set; }

    public string rij { get; set; } = null!;

    public int rek { get; set; }

    public int aantal { get; set; }

    public virtual Artikel? artikel { get; set; }
}
