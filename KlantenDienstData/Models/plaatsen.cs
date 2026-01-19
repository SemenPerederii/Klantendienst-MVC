using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class plaatsen
{
    public int plaatsId { get; set; }

    public string postcode { get; set; } = null!;

    public string plaats { get; set; } = null!;

    public virtual ICollection<adressen> adressens { get; set; } = new List<adressen>();

    public virtual ICollection<leverancier> leveranciers { get; set; } = new List<leverancier>();
}
