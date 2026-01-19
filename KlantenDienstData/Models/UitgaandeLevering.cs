using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class UitgaandeLevering
{
    public int UitgaandeLeveringsId { get; set; }

    public int BestelId { get; set; }

    public DateOnly VertrekDatum { get; set; }

    public DateOnly? AankomstDatum { get; set; }

    public string Trackingcode { get; set; } = null!;

    public int KlantId { get; set; }

    public int UitgaandeLeveringsStatusId { get; set; }

    public virtual Bestelling Bestel { get; set; } = null!;

    public virtual Klant Klant { get; set; } = null!;

    public virtual UitgaandeLeveringsStatus UitgaandeLeveringsStatus { get; set; } = null!;
}