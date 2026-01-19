using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class bestellingen
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

    public virtual ICollection<bestellijnen> bestellijnens { get; set; } = new List<bestellijnen>();

    public virtual bestellingsstatussen bestellingsStatus { get; set; } = null!;

    public virtual betaalwijze betaalwijze { get; set; } = null!;

    public virtual adressen facturatieAdres { get; set; } = null!;

    public virtual klanten klant { get; set; } = null!;

    public virtual adressen leveringsAdres { get; set; } = null!;

    public virtual ICollection<uitgaandeleveringen> uitgaandeleveringens { get; set; } = new List<uitgaandeleveringen>();
}
