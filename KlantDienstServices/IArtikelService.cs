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
        Task DeactiveerArtikelAsync(Artikel artikel);
        Task<List<Artikel>> GetAllArtikelenAsync();
        Task<Artikel> GetArtikelByIdAsync(int id);
        Task<List<Artikel>> ZoekArtikelenOpFilterAsync(ArtikelFilterDto filters);
    }
}
