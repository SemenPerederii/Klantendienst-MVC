using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class EventwachtrijArtikel
{
    public int ArtikelId { get; set; }

    public int Aantal { get; set; }

    public int MaxAantalInMagazijnPlaats { get; set; }
}
