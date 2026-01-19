using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class uitgaandeleveringsstatussen
{
    public int uitgaandeLeveringsStatusId { get; set; }

    public string naam { get; set; } = null!;

    public virtual ICollection<uitgaandeleveringen> uitgaandeleveringens { get; set; } = new List<uitgaandeleveringen>();
}
