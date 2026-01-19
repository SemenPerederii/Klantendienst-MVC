using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Plaats
{
    public int plaatsId { get; set; }

    public string postcode { get; set; } = null!;

    public string plaats { get; set; } = null!;

    public virtual ICollection<Adres> adressens { get; set; } = new List<Adres>();

    public virtual ICollection<leverancier> leveranciers { get; set; } = new List<leverancier>();
}
