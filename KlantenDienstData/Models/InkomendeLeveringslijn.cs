using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class InkomendeLeveringslijn
{
    public int inkomendeLeveringsId { get; set; }

    public int artikelId { get; set; }

    public int aantalBesteld { get; set; }

    public int aantalGoedgekeurd { get; set; }

    public int aantalTeruggestuurd { get; set; }

    public int magazijnPlaatsId { get; set; }

    public virtual Artikel artikel { get; set; } = null!;

    public virtual InkomendeLevering inkomendeLeverings { get; set; } = null!;
}
