using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class PersoneelsLid
{
    public int personeelslidId { get; set; }

    public string voornaam { get; set; } = null!;

    public string familienaam { get; set; } = null!;

    public bool? inDienst { get; set; }

    public int personeelslidAccountId { get; set; }

    public virtual ICollection<InkomendeLevering> inkomendeleveringens { get; set; } = new List<InkomendeLevering>();

    public virtual personeelslidaccount personeelslidAccount { get; set; } = null!;

    public virtual ICollection<SecurityGroep> securityGroeps { get; set; } = new List<SecurityGroep>();
}
