using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class Bestelling
{
    public int bestelId { get; set; }

    public DateTime besteldatum { get; set; }

    public int klantId { get; set; }

    public bool betaald { get; set; }

    public string? betalingscode { get; set; }

    public int betaalwijzeId { get; set; }

    public bool annulatie { get; set; }

    public DateOnly? annulatiedatum { get; set; }

    public string? terugbetalingscode { get; set; }

    public int bestellingsStatusId { get; set; }

    public bool actiecodeGebruikt { get; set; }

    public string? bedrijfsnaam { get; set; }

    public string? btwNummer { get; set; }

    public string voornaam { get; set; } = null!;

    public string familienaam { get; set; } = null!;

    public int facturatieAdresId { get; set; }

    public int leveringsAdresId { get; set; }

    public virtual ICollection<Bestellijn> bestellijnens { get; set; } = new List<Bestellijn>();

    public virtual Bestellingsstatus bestellingsStatus { get; set; } = null!;

    public virtual betaalwijze betaalwijze { get; set; } = null!;

    public virtual Adres facturatieAdres { get; set; } = null!;

    public virtual Klant klant { get; set; } = null!;

    public virtual Adres leveringsAdres { get; set; } = null!;

    public virtual ICollection<UitgaandeLevering> uitgaandeleveringens { get; set; } = new List<UitgaandeLevering>();
}
