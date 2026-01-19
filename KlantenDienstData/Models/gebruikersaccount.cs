using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class gebruikersaccount
{
    public int gebruikersAccountId { get; set; }

    public string emailadres { get; set; } = null!;

    public string paswoord { get; set; } = null!;

    public bool disabled { get; set; }

    public virtual ICollection<chatgesprekken> chatgesprekkens { get; set; } = new List<chatgesprekken>();

    public virtual ICollection<chatgespreklijnen> chatgespreklijnens { get; set; } = new List<chatgespreklijnen>();

    public virtual ICollection<contactpersonen> contactpersonens { get; set; } = new List<contactpersonen>();

    public virtual ICollection<natuurlijkepersonen> natuurlijkepersonens { get; set; } = new List<natuurlijkepersonen>();

    public virtual ICollection<wishlistitem> wishlistitems { get; set; } = new List<wishlistitem>();
}
