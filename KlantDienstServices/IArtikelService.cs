using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public interface IArtikelService
    {
        bool CheckStatusActief(Artikel artikel);
        Task<List<Artikel>> GetAllArtikelenAsync();
        Task<List<Artikel>> ZoekArtikelenOpFilterAsync(ArtikelFilterDto filters);
        Task<bool> VoegArtikelToeAsync(Artikel artikel);
        Task<bool> WijzigArtikelAsync(int artikelId, Artikel nieuwArtikel);
        Task<Artikel?> GetArtikelAsync(int id);
        Task<IEnumerable<Artikel>> GetArtikelenByCategorieAsync(int categorieId);
    }
}
