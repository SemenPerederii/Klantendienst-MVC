using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class eventwachtrijartikelen
{
    public int artikelId { get; set; }

    public int aantal { get; set; }

    public int maxAantalInMagazijnPlaats { get; set; }
}
