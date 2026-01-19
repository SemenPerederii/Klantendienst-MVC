using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class veelgesteldevragenartikel
{
    public int veelgesteldeVragenArtikelId { get; set; }

    public int artikelId { get; set; }

    public string vraag { get; set; } = null!;

    public string antwoord { get; set; } = null!;

    public virtual artikelen artikel { get; set; } = null!;
}
