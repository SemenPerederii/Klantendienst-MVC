using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Adres
{
    public int adresId { get; set; }

    public string straat { get; set; } = null!;

    public string huisNummer { get; set; } = null!;

    public string? bus { get; set; }

    public int plaatsId { get; set; }

    public bool? actief { get; set; }

    public virtual ICollection<Bestelling> bestellingenfacturatieAdres { get; set; } = new List<Bestelling>();

    public virtual ICollection<Bestelling> bestellingenleveringsAdres { get; set; } = new List<Bestelling>();

    public virtual ICollection<Klant> klantenfacturatieAdres { get; set; } = new List<Klant>();

    public virtual ICollection<Klant> klantenleveringsAdres { get; set; } = new List<Klant>();

    public virtual Plaats plaats { get; set; } = null!;
}
