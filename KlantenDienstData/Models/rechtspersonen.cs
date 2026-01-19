using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class rechtspersonen
{
    public int klantId { get; set; }

    public string naam { get; set; } = null!;

    public string btwNummer { get; set; } = null!;

    public virtual ICollection<contactpersonen> contactpersonens { get; set; } = new List<contactpersonen>();

    public virtual klanten klant { get; set; } = null!;
}
