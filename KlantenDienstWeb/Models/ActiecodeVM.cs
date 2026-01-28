using KlantenDienstData.Enums;
using KlantenDienstData.Models;

namespace KlantenDienstWeb.Models
{
    public class ActiecodeVM
    {
        public IEnumerable<Actiecode> Actiecodes { get; set; } = new List<Actiecode>();
        public ActiecodeSorteerOpties SorteerOpties { get; set; } 
        public SorteerRichting SorteerRichting { get; set; }
        public ActiecodeStatus HuidigeFilter { get; set; } 
        public DateOnly? HuidigeDatum { get; set; }
        public string? HuidigeDatumString => HuidigeDatum?.ToString("yyyy-MM-dd");
    }
}
