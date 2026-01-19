using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class natuurlijkepersonen
{
    public int klantId { get; set; }

    public string voornaam { get; set; } = null!;

    public string familienaam { get; set; } = null!;

    public int gebruikersAccountId { get; set; }

    public virtual gebruikersaccount gebruikersAccount { get; set; } = null!;

    public virtual klanten klant { get; set; } = null!;
}
