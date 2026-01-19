using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Bestellingsstatus
{
    public int BestellingsStatusId { get; set; }

    public string Naam { get; set; } = null!;

    public virtual ICollection<Bestelling> Bestellingen { get; set; } = new List<Bestelling>();
}
