namespace KlantenDienstServices
{
    public sealed class ArtikelFilterDto
    {
        public int? Id { get; set; }
        public string? Ean { get; set; }
        public string? Naam { get; set; }
        public decimal? MinPrijs { get; set; }
        public decimal? MaxPrijs { get; set; }
        public bool EnkelInVoorraad { get; set; }
        public List<int> CategorieIds { get; set; } = new();
        public ArtikelSorteerOptie Sortering { get; set; } = ArtikelSorteerOptie.NaamAsc;
    }
    public enum ArtikelSorteerOptie
    {
        NaamAsc,
        NaamDesc,
        PrijsAsc,
        PrijsDesc,
        EanAsc,
        EanDesc,
        VoorraadAsc,
        VoorraadDesc
    }
}
