using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class VeelgesteldevragenArtikel
{
    public int VeelgesteldeVragenArtikelId { get; set; }

    public int artikelId { get; set; }

    public string vraag { get; set; } = null!;

    public string antwoord { get; set; } = null!;

    public virtual Artikel artikel { get; set; } = null!;
}
