using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Chatgesprek
{
    public int chatgesprekId { get; set; }

    public int gebruikersAccountId { get; set; }

    public virtual ICollection<Chatgespreklijn> chatgespreklijnens { get; set; } = new List<Chatgespreklijn>();

    public virtual GebruikersAccount gebruikersAccount { get; set; } = null!;
}
