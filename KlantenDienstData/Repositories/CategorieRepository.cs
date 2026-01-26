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
                .Include(c => c.InversehoofdCategorie)
                .Include(c => c.ArtikelCategorieen)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Categorie?> GetCategorieAsync(int id)
        {
            return await _context.Categorieen.FindAsync(id);
        }

        public async Task<Categorie> GetByIdAsync(int id)
        {
            return await _context.Categorieen
                .Include(c => c.InversehoofdCategorie)
                .Include(c => c.ArtikelCategorieen)
                .FirstOrDefaultAsync(c => c.CategorieId == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasChildrenAsync(int id)
        {
            return await _context.Categorieen.AnyAsync(c => c.HoofdCategorieId == id);
        }

        public async Task<bool> HasArtikelenAsync(int id)
        {
            return await _context.ArtikelCategorieen.AnyAsync(ac => ac.CategorieId == id);
        }

        public async Task DeleteAsync(Categorie categorie)
        {
            _context.Categorieen.Remove(categorie);
            await Task.CompletedTask;
        }
    }
}
