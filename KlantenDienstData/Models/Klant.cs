using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Klant
{
    public int klantId { get; set; }

    public int facturatieAdresId { get; set; }

    public int leveringsAdresId { get; set; }

    public virtual ICollection<Bestelling> bestellingens { get; set; } = new List<Bestelling>();

    public virtual Adres facturatieAdres { get; set; } = null!;

    public virtual Adres leveringsAdres { get; set; } = null!;

    public virtual NatuurlijkePersoon? natuurlijkepersonen { get; set; }

    public virtual RechtsPersoon? rechtspersonen { get; set; }

    public virtual ICollection<UitgaandeLevering> uitgaandeleveringens { get; set; } = new List<UitgaandeLevering>();
}
