using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class chatgespreklijnen
{
    public int chatgesprekLijnId { get; set; }

    public int chatgesprekId { get; set; }

    public string bericht { get; set; } = null!;

    public DateTime tijdstip { get; set; }

    public int? gebruikersAccountId { get; set; }

    public int? personeelslidAccountId { get; set; }

    public virtual chatgesprekken chatgesprek { get; set; } = null!;

    public virtual gebruikersaccount? gebruikersAccount { get; set; }

    public virtual personeelslidaccount? personeelslidAccount { get; set; }
}
