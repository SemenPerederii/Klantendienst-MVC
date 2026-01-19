using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class uitgaandeleveringen
{
    public int uitgaandeLeveringsId { get; set; }

    public int bestelId { get; set; }

    public DateOnly vertrekDatum { get; set; }

    public DateOnly? aankomstDatum { get; set; }

    public string trackingcode { get; set; } = null!;

    public int klantId { get; set; }

    public int uitgaandeLeveringsStatusId { get; set; }

    public virtual bestellingen bestel { get; set; } = null!;

    public virtual klanten klant { get; set; } = null!;

    public virtual uitgaandeleveringsstatussen uitgaandeLeveringsStatus { get; set; } = null!;
}
