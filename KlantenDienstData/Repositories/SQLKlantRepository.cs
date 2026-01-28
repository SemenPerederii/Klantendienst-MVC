using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public class SQLKlantRepository : IKlantRepository
    {
        private readonly PrulariaDbContext _context;
        public SQLKlantRepository(PrulariaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Klant>> GetAllKlantenAsync()
        {
            return await _context.Klanten
                .Include(k => k.Natuurlijkepersonen)
                .Include(k => k.Rechtspersonen)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Klant?> GetKlantAsync(int id)
        {
            return await _context.Klanten
                .Include(klant => klant.Rechtspersonen).ThenInclude(rp => rp.Contactpersonen)
                .Include(klant => klant.Natuurlijkepersonen)
                .Include(klant => klant.FacturatieAdres)
                .Include(klant => klant.LeveringsAdres)
                .Include(klant => klant.Bestellingen)
                .ThenInclude(bestelling => bestelling.Bestellijnen)
                .FirstOrDefaultAsync(klant => klant.KlantId == id);
                
        }
    }
}
