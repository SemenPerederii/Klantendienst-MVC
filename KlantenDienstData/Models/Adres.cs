using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Adres
{
    public int AdresId { get; set; }

    public string Straat { get; set; } = null!;

    public string HuisNummer { get; set; } = null!;

    public string? Bus { get; set; }

    public int PlaatsId { get; set; }

    public bool? Actief { get; set; }

    public virtual ICollection<Bestelling> BestellingenfacturatieAdres { get; set; } = new List<Bestelling>();

    public virtual ICollection<Bestelling> BestellingenleveringsAdres { get; set; } = new List<Bestelling>();

    public virtual ICollection<Klant> KlantenfacturatieAdres { get; set; } = new List<Klant>();

    public virtual ICollection<Klant> KlantenleveringsAdres { get; set; } = new List<Klant>();

    public virtual Plaats Plaats { get; set; } = null!;
}
