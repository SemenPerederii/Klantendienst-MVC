using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class RechtsPersoon
{
    public int klantId { get; set; }

    public string naam { get; set; } = null!;

    public string btwNummer { get; set; } = null!;

    public virtual ICollection<Contactpersoon> contactpersonens { get; set; } = new List<Contactpersoon>();

    public virtual Klant klant { get; set; } = null!;
}
