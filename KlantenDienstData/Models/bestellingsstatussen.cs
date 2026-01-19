using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class bestellingsstatussen
{
    public int bestellingsStatusId { get; set; }

    public string naam { get; set; } = null!;

    public virtual ICollection<bestellingen> bestellingens { get; set; } = new List<bestellingen>();
}
