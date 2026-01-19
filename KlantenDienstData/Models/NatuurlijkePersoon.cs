using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class NatuurlijkePersoon
{
    public int klantId { get; set; }

    public string voornaam { get; set; } = null!;

    public string familienaam { get; set; } = null!;

    public int gebruikersAccountId { get; set; }

    public virtual GebruikersAccount gebruikersAccount { get; set; } = null!;

    public virtual Klant klant { get; set; } = null!;
}
