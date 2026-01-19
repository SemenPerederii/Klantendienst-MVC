using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class inkomendeleveringen
{
    public int inkomendeLeveringsId { get; set; }

    public int leveranciersId { get; set; }

    public string leveringsbonNummer { get; set; } = null!;

    public DateOnly leveringsbondatum { get; set; }

    public DateOnly leverDatum { get; set; }

    public int ontvangerPersoneelslidId { get; set; }

    public virtual ICollection<inkomendeleveringslijnen> inkomendeleveringslijnens { get; set; } = new List<inkomendeleveringslijnen>();

    public virtual leverancier leveranciers { get; set; } = null!;

    public virtual personeelsleden ontvangerPersoneelslid { get; set; } = null!;
}
