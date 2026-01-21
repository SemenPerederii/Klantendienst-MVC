using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<bool> VoegArtikelToeAsync(Artikel artikel)
        {
            await _context.Artikelen.AddAsync(artikel);
            return await _context.Artikelen.ContainsAsync(artikel);
        }

        public async Task<bool> WijzigArtikelAsync(int artikelId, Artikel niewArtikel)
        {
            Artikel? oud = await _context.Artikelen.FindAsync(artikelId);
            if (oud == null)
                return false;
            if (oud.Equals(niewArtikel))
                return false;
            //aanpassen
            _context.Artikelen.Entry(oud).CurrentValues.SetValues(niewArtikel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
