using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class personeelsleden
{
    public int personeelslidId { get; set; }

    public string voornaam { get; set; } = null!;

    public string familienaam { get; set; } = null!;

    public bool? inDienst { get; set; }

    public int personeelslidAccountId { get; set; }

    public virtual ICollection<inkomendeleveringen> inkomendeleveringens { get; set; } = new List<inkomendeleveringen>();

    public virtual personeelslidaccount personeelslidAccount { get; set; } = null!;

    public virtual ICollection<securitygroepen> securityGroeps { get; set; } = new List<securitygroepen>();
}
