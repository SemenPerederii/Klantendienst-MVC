using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class KlantReview
{
    public int klantenReviewId { get; set; }

    public string nickname { get; set; } = null!;

    public int score { get; set; }

    public string? commentaar { get; set; }

    public DateOnly datum { get; set; }

    public int bestellijnId { get; set; }

    public virtual Bestellijn bestellijn { get; set; } = null!;
}
