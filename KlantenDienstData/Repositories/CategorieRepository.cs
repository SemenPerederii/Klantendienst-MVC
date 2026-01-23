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
        public async Task<Categorie?> GetByIdAsync(int id)
        {
            return await _context.Categorieen
                .FirstOrDefaultAsync(c => c.CategorieId == id);
        }

        public async Task<List<Categorie>> GetByIdsAsync(IEnumerable<int>? ids)
        {
            if (ids == null || !ids.Any())
                return new List<Categorie>();

            return await _context.Categorieen
                .Where(c => ids.Contains(c.CategorieId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Categorie>> HoofdcategorieAsync()
        {
            return await _context.Categorieen
                        .Where(c => c.HoofdCategorieId == null)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Categorie>> SubcategorieenAsync(int hoofdCategorieId)
        {
            return await _context.Categorieen
                        .Where(c => c.HoofdCategorieId == hoofdCategorieId)
                        .ToListAsync();
        }

        public async Task<Categorie?> AddCategorieAsync(Categorie categorie, IEnumerable<int>? subCategorieIds)
        {
            if (categorie == null)
                throw new ArgumentNullException(nameof(categorie));

            _context.Categorieen.Add(categorie);
            await _context.SaveChangesAsync();

            if (subCategorieIds != null && subCategorieIds.Any())
            {
                var subCategorieen = await GetByIdsAsync(subCategorieIds);

                foreach (var sub in subCategorieen)
                {
                    sub.HoofdCategorieId = categorie.CategorieId;
                }

                await _context.SaveChangesAsync();
            }

            return categorie;
        }

        public async Task<bool> CategorieBestaatAlAsync(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam))
                return false;
            return await _context.Categorieen.AnyAsync(c => c.Naam.ToLower() == naam.ToLower());
        }
    }
}