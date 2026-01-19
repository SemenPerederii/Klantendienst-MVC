using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class betaalwijze
{
    public int betaalwijzeId { get; set; }

    public string naam { get; set; } = null!;

    public virtual ICollection<bestellingen> bestellingens { get; set; } = new List<bestellingen>();
}
