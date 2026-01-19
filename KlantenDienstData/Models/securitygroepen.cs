using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class securitygroepen
{
    public int securityGroepId { get; set; }

    public string naam { get; set; } = null!;

    public virtual ICollection<personeelsleden> personeelslids { get; set; } = new List<personeelsleden>();
}
