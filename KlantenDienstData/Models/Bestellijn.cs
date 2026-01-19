using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Bestellijn
{
    public int bestellijnId { get; set; }

    public int bestelId { get; set; }

    public int artikelId { get; set; }

    public int aantalBesteld { get; set; }

    public int aantalGeannuleerd { get; set; }

    public virtual Artikel artikel { get; set; } = null!;

    public virtual Bestelling bestel { get; set; } = null!;

    public virtual ICollection<KlantReview> klantenreviews { get; set; } = new List<KlantReview>();
}
