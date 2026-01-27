using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public interface IArtikelRepository
    {
       Task<List<Artikel>> GetAllArtikelenAsync();
        IQueryable<Artikel> GetArtikelQuery();
       Task<bool> VoegArtikelToeAsync(Artikel artikel);
        Task<bool> WijzigArtikelAsync(int artikelId, Artikel niewArtikel);
        Task<Artikel?> GetArtikelAsync(int id);
        Task<IEnumerable<Artikel>> GetArtikelenByCategorieAsync(int categorieId);
        Task SaveChangesAsync();
    }
}
