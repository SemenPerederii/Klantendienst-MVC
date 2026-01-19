using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Actiecode
{
    public int ActiecodeId { get; set; }

    public string Naam { get; set; } = null!;

    public DateOnly GeldigVanDatum { get; set; }

    public DateOnly GeldigTotDatum { get; set; }

    public bool IsEenmalig { get; set; }
}
