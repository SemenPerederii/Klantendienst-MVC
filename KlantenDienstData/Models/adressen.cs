using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class adressen
{
    public int adresId { get; set; }

    public string straat { get; set; } = null!;

    public string huisNummer { get; set; } = null!;

    public string? bus { get; set; }

    public int plaatsId { get; set; }

    public bool? actief { get; set; }

    public virtual ICollection<bestellingen> bestellingenfacturatieAdres { get; set; } = new List<bestellingen>();

    public virtual ICollection<bestellingen> bestellingenleveringsAdres { get; set; } = new List<bestellingen>();

    public virtual ICollection<klanten> klantenfacturatieAdres { get; set; } = new List<klanten>();

    public virtual ICollection<klanten> klantenleveringsAdres { get; set; } = new List<klanten>();

    public virtual plaatsen plaats { get; set; } = null!;
}
