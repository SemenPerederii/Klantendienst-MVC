using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class ArtikelLeveranciersInfolijn
{
    public int ArtikelLeveranciersInfoLijnId { get; set; }

    public int ArtikelId { get; set; }

    public string Vraag { get; set; } = null!;

    public string Antwoord { get; set; } = null!;

    public virtual Artikel Artikel { get; set; } = null!;
}
