using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly PrulariaDbContext _context;

        public CategorieRepository(PrulariaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Categorie>> GetAll()
        {
            return await _context.Categorieen
                .Include(c => c.InversehoofdCategorie).ToListAsync();
        }

        public async Task<Categorie?> GetCategorieAsync(int id)
        {
            return await _context.Categorieen.FindAsync(id);
        }
    }
}
