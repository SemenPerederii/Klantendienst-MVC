using System;
using System.Collections.Generic;

namespace KlantenDienstData.Models;

public partial class UitgaandeLeveringsStatus
{
    public int uitgaandeLeveringsStatusId { get; set; }

    public string naam { get; set; } = null!;

    public virtual ICollection<UitgaandeLevering> uitgaandeleveringens { get; set; } = new List<UitgaandeLevering>();
}
