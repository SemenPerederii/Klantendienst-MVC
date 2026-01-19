using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class PersoneelsLidAccount
{
    public int PersoneelslidAccountId { get; set; }

    public string Emailadres { get; set; } = null!;

    public string Paswoord { get; set; } = null!;

    public bool Disabled { get; set; }

    public virtual ICollection<Chatgespreklijn> Chatgespreklijnen { get; set; } = new List<Chatgespreklijn>();

    public virtual ICollection<PersoneelsLid> Personeelsleden { get; set; } = new List<PersoneelsLid>();
}
