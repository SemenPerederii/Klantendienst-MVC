using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class leverancier
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

    public virtual ICollection<artikelen> artikelens { get; set; } = new List<artikelen>();

    public virtual ICollection<inkomendeleveringen> inkomendeleveringens { get; set; } = new List<inkomendeleveringen>();

    public virtual plaatsen plaats { get; set; } = null!;
}
