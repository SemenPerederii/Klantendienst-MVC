using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class contactpersonen
{
    public int contactpersoonId { get; set; }

    public string voornaam { get; set; } = null!;

    public string familienaam { get; set; } = null!;

    public string functie { get; set; } = null!;

    public int klantId { get; set; }

    public int gebruikersAccountId { get; set; }

    public virtual gebruikersaccount gebruikersAccount { get; set; } = null!;

    public virtual rechtspersonen klant { get; set; } = null!;
}
