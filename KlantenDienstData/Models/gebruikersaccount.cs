using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class GebruikersAccount
{
    public int gebruikersAccountId { get; set; }

    public string emailadres { get; set; } = null!;

    public string paswoord { get; set; } = null!;

    public bool disabled { get; set; }

    public virtual ICollection<Chatgesprek> chatgesprekkens { get; set; } = new List<Chatgesprek>();

    public virtual ICollection<Chatgespreklijn> chatgespreklijnens { get; set; } = new List<Chatgespreklijn>();

    public virtual ICollection<Contactpersoon> contactpersonens { get; set; } = new List<Contactpersoon>();

    public virtual ICollection<NatuurlijkePersoon> natuurlijkepersonens { get; set; } = new List<NatuurlijkePersoon>();

    public virtual ICollection<wishlistitem> wishlistitems { get; set; } = new List<wishlistitem>();
}
