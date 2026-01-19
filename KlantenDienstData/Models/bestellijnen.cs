using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class bestellijnen
{
    public int bestellijnId { get; set; }

    public int bestelId { get; set; }

    public int artikelId { get; set; }

    public int aantalBesteld { get; set; }

    public int aantalGeannuleerd { get; set; }

    public virtual artikelen artikel { get; set; } = null!;

    public virtual bestellingen bestel { get; set; } = null!;

    public virtual ICollection<klantenreview> klantenreviews { get; set; } = new List<klantenreview>();
}
