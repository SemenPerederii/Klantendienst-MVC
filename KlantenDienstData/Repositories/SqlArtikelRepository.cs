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

        public async Task DeactiveerArtikelAsync(int artikelId)
        {
            var artikel = await GetArtikelByIdAsync(artikelId);

            if (artikel == null)
            {
                return;
            }

            artikel.MinimumVoorraad = 0;
            artikel.MaximumVoorraad = 0;
            artikel.Bestelpeil = 0;
            artikel.AantalBesteldLeverancier = 0;

            await _context.SaveChangesAsync();
        }
    }
}
