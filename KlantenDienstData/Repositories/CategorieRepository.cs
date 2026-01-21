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
            return await _context.Categorieen.ToListAsync();
        }
        public async Task<Categorie?> AddCategorieAsync(Categorie categorie)
        {
            try
            {
                _context.Categorieen.Add(categorie);
                await _context.SaveChangesAsync();
                return categorie;
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                    throw;
                if (ex is DbUpdateException dbEx)
                    throw new InvalidOperationException("Opslaan van categorie is mislukt.", dbEx);

                throw new InvalidOperationException("Onverwachte fout bij toevoegen van een categorie.", ex);
            }
        }
        public async Task<bool> CategorieBestaatAlAsync(string naam)
        {
            return await _context.Categorieen.AnyAsync(c => c.Naam.ToLower() == naam.ToLower());
        }
    }
}