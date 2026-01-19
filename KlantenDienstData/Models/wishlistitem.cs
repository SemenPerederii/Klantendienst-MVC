using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Wishlistitem
{
    public int WishListItemId { get; set; }

    public int ArtikelId { get; set; }

    public int GebruikersAccountId { get; set; }

    public DateOnly AanvraagDatum { get; set; }

    public int Aantal { get; set; }

    public DateOnly? EmailGestuurdDatum { get; set; }

    public virtual Artikel Artikel { get; set; } = null!;

    public virtual GebruikersAccount GebruikersAccount { get; set; } = null!;
}
