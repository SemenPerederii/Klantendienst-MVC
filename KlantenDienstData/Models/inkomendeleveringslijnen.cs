using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class inkomendeleveringslijnen
{
    public int inkomendeLeveringsId { get; set; }

    public int artikelId { get; set; }

    public int aantalBesteld { get; set; }

    public int aantalGoedgekeurd { get; set; }

    public int aantalTeruggestuurd { get; set; }

    public int magazijnPlaatsId { get; set; }

    public virtual artikelen artikel { get; set; } = null!;

    public virtual inkomendeleveringen inkomendeLeverings { get; set; } = null!;
}
