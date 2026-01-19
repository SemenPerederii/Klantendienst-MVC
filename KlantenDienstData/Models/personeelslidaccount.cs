using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class personeelslidaccount
{
    public int personeelslidAccountId { get; set; }

    public string emailadres { get; set; } = null!;

    public string paswoord { get; set; } = null!;

    public bool disabled { get; set; }

    public virtual ICollection<Chatgespreklijn> chatgespreklijnens { get; set; } = new List<Chatgespreklijn>();

    public virtual ICollection<PersoneelsLid> personeelsledens { get; set; } = new List<PersoneelsLid>();
}
