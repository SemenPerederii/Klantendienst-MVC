using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Leverancier
{
    public int leveranciersId { get; set; }

    public string naam { get; set; } = null!;

    public string btwNummer { get; set; } = null!;

    public string straat { get; set; } = null!;

    public string huisNummer { get; set; } = null!;

    public string? bus { get; set; }

    public int plaatsId { get; set; }

    public string familienaamContactpersoon { get; set; } = null!;

    public string voornaamContactpersoon { get; set; } = null!;

    public virtual ICollection<Artikel> artikelens { get; set; } = new List<Artikel>();

    public virtual ICollection<InkomendeLevering> inkomendeleveringens { get; set; } = new List<InkomendeLevering>();

    public virtual Plaats plaats { get; set; } = null!;
}
