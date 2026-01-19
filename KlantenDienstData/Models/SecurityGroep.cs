using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class SecurityGroep
{
    public int securityGroepId { get; set; }

    public string naam { get; set; } = null!;

    public virtual ICollection<PersoneelsLid> personeelslids { get; set; } = new List<PersoneelsLid>();
}
