using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public class SqlArtikelRepository : IArtikelRepository
    {
        private readonly PrulariaDbContext _context;

        public SqlArtikelRepository(PrulariaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Artikel>> GetAllArtikelenAsync() =>
            await _context.Artikelen
            .Include(a => a.Categorieën)
            .Include(a => a.Bestellijnen)
            .Include(a => a.Inkomendeleveringslijnen)
            .Include(a => a.Magazijnplaatsen)
            .ToListAsync();

        public async Task<Artikel> GetArtikelByIdAsync(int id) =>
           await _context.Artikelen
            .Include(a => a.Categorieën)
            .Include(a => a.Bestellijnen)
            .Include(a => a.Inkomendeleveringslijnen)
            .Include(a => a.Magazijnplaatsen)
            .FirstOrDefaultAsync(a => a.ArtikelId == id)!;

        public IQueryable<Artikel> GetArtikelQuery()
        {
            return _context.Artikelen
                .AsNoTracking()
                .Include(a => a.Categorieën); 
        }

        public async Task UpdateArtikelAsync(Artikel artikel)
        {
             _context.Artikelen.Update(artikel);
            await _context.SaveChangesAsync();
        }
    }
}
