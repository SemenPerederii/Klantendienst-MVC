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
    }
}
