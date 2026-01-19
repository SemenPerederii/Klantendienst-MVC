using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class PersoneelsLid
{
    public int PersoneelslidId { get; set; }

    public string Voornaam { get; set; } = null!;

    public string Familienaam { get; set; } = null!;

    public bool? InDienst { get; set; }

    public int PersoneelslidAccountId { get; set; }

    public virtual ICollection<InkomendeLevering> Inkomendeleveringens { get; set; } = new List<InkomendeLevering>();

    public virtual PersoneelsLidAccount PersoneelslidAccount { get; set; } = null!;

    public virtual ICollection<SecurityGroep> SecurityGroepen { get; set; } = new List<SecurityGroep>();
}
