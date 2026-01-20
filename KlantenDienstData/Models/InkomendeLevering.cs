using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class InkomendeLevering
{
    public int InkomendeLeveringsId { get; set; }

    public int LeveranciersId { get; set; }

    public string LeveringsbonNummer { get; set; } = null!;

    public DateOnly Leveringsbondatum { get; set; }

    public DateOnly LeverDatum { get; set; }

    public int OntvangerPersoneelslidId { get; set; }

    public virtual ICollection<InkomendeLeveringslijn> Inkomendeleveringslijnen { get; set; } = new List<InkomendeLeveringslijn>();

    public virtual Leverancier Leveranciers { get; set; } = null!;

    public virtual Personeelslid OntvangerPersoneelslid { get; set; } = null!;
}
