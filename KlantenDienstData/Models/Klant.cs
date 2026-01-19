using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Klant
{
    public int KlantId { get; set; }

    public int FacturatieAdresId { get; set; }

    public int LeveringsAdresId { get; set; }

    public virtual ICollection<Bestelling> Bestellingen { get; set; } = new List<Bestelling>();

    public virtual Adres FacturatieAdres { get; set; } = null!;

    public virtual Adres LeveringsAdres { get; set; } = null!;

    public virtual NatuurlijkePersoon? Natuurlijkepersonen { get; set; }

    public virtual RechtsPersoon? Rechtspersonen { get; set; }

    public virtual ICollection<UitgaandeLevering> Uitgaandeleveringen { get; set; } = new List<UitgaandeLevering>();
}
