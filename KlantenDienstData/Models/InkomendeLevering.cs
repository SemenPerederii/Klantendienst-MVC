using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class InkomendeLevering
{
    public int inkomendeLeveringsId { get; set; }

    public int leveranciersId { get; set; }

    public string leveringsbonNummer { get; set; } = null!;

    public DateOnly leveringsbondatum { get; set; }

    public DateOnly leverDatum { get; set; }

    public int ontvangerPersoneelslidId { get; set; }

    public virtual ICollection<InkomendeLeveringslijn> inkomendeleveringslijnens { get; set; } = new List<InkomendeLeveringslijn>();

    public virtual leverancier leveranciers { get; set; } = null!;

    public virtual PersoneelsLid ontvangerPersoneelslid { get; set; } = null!;
}
