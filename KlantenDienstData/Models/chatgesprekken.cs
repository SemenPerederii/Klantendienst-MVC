using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class chatgesprekken
{
    public int chatgesprekId { get; set; }

    public int gebruikersAccountId { get; set; }

    public virtual ICollection<chatgespreklijnen> chatgespreklijnens { get; set; } = new List<chatgespreklijnen>();

    public virtual gebruikersaccount gebruikersAccount { get; set; } = null!;
}
