using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Betaalwijze
{
    public int betaalwijzeId { get; set; }

    public string naam { get; set; } = null!;

    public virtual ICollection<Bestelling> bestellingens { get; set; } = new List<Bestelling>();
}
