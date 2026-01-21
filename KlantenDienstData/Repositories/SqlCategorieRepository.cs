using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public class SqlCategorieRepository : ICategorieRepository
    {
        private readonly PrulariaDbContext _context;

        public SqlCategorieRepository(PrulariaDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Categorie categorie)
        {
            _context.Categorieen.Remove(categorie);
            await Task.CompletedTask;
        }

        public async Task<List<Categorie>> GetAll()
        {
            return await _context.Categorieen.AsNoTracking().ToListAsync();
        }

        public async Task<Categorie> GetByIdAsync(int id)
        {
            return await _context.Categorieen.FirstOrDefaultAsync(c => c.CategorieId == id);
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
    }
}
