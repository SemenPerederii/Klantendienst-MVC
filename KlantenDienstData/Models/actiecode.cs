using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class actiecode
{
    public int actiecodeId { get; set; }

    public string naam { get; set; } = null!;

    public DateOnly geldigVanDatum { get; set; }

    public DateOnly geldigTotDatum { get; set; }

    public bool isEenmalig { get; set; }
}
