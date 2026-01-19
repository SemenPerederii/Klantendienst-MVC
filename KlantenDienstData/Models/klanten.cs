using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class klanten
{
    public int klantId { get; set; }

    public int facturatieAdresId { get; set; }

    public int leveringsAdresId { get; set; }

    public virtual ICollection<bestellingen> bestellingens { get; set; } = new List<bestellingen>();

    public virtual adressen facturatieAdres { get; set; } = null!;

    public virtual adressen leveringsAdres { get; set; } = null!;

    public virtual natuurlijkepersonen? natuurlijkepersonen { get; set; }

    public virtual rechtspersonen? rechtspersonen { get; set; }

    public virtual ICollection<uitgaandeleveringen> uitgaandeleveringens { get; set; } = new List<uitgaandeleveringen>();
}
