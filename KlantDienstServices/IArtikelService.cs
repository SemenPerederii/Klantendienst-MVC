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
        Task<List<Artikel>> GetAllArtikelenAsync();
        Task<Artikel> GetArtikelByIdAsync(int id);
        Task<List<Artikel>> ZoekArtikelenOpFilterAsync(ArtikelFilterDto filters);
    }
}
