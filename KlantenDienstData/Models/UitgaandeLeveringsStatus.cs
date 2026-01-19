using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class UitgaandeLeveringsStatus
{
    public int UitgaandeLeveringsStatusId { get; set; }

    public string Naam { get; set; } = null!;

    public virtual ICollection<UitgaandeLevering> Uitgaandeleveringen { get; set; } = new List<UitgaandeLevering>();
}
