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
        Task<Artikel> GetArtikelByIdAsync(int id);
        IQueryable<Artikel> GetArtikelQuery();
        Task DeactiveerArtikelAsync(int artikelId);
    }
}
