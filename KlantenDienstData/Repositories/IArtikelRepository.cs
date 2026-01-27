using KlantenDienstData.Models;

namespace KlantenDienstData.Repositories
{
    public interface IArtikelRepository
    {
        Task<List<Artikel>> GetAllArtikelenAsync(ArtikelSorteerOpties? sorteerOpties = null, SorteerRichting? sorteerRichting = null);
        IQueryable<Artikel> GetArtikelQuery();
       Task<bool> VoegArtikelToeAsync(Artikel artikel);
        Task<bool> WijzigArtikelAsync(int artikelId, Artikel niewArtikel);
        Task<Artikel?> GetArtikelAsync(int id);
    }
}
