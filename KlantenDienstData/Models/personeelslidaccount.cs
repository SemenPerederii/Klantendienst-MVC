using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class personeelslidaccount
{
    public int personeelslidAccountId { get; set; }

    public string emailadres { get; set; } = null!;

    public string paswoord { get; set; } = null!;

    public bool disabled { get; set; }

    public virtual ICollection<chatgespreklijnen> chatgespreklijnens { get; set; } = new List<chatgespreklijnen>();

    public virtual ICollection<personeelsleden> personeelsledens { get; set; } = new List<personeelsleden>();
}
