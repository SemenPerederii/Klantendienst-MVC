namespace KlantenDienstWeb.Models
{
    public class KlantIndexViewModel
    {
        public IEnumerable<KlantOverzichtViewModel> Klanten { get; set; } = new List<KlantOverzichtViewModel>();
        
        // Filter properties
        public string? NaamFilter { get; set; }
        public string? TypeFilter { get; set; }
        
        // Sort properties
        public string SortOrder { get; set; } = "naam_asc";
        public string NaamSortParam => SortOrder == "naam_asc" ? "naam_desc" : "naam_asc";
        public string TypeSortParam => SortOrder == "type_asc" ? "type_desc" : "type_asc";
    }
}