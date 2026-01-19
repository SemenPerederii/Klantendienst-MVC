using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Wishlistitem
{
    public int wishListItemId { get; set; }

    public int artikelId { get; set; }

    public int gebruikersAccountId { get; set; }

    public DateOnly aanvraagDatum { get; set; }

    public int aantal { get; set; }

    public DateOnly? emailGestuurdDatum { get; set; }

    public virtual Artikel artikel { get; set; } = null!;

    public virtual GebruikersAccount gebruikersAccount { get; set; } = null!;
}
