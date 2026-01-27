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

        public async Task<Categorie?> GetByIdAsync(int id)
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

        public async Task<Categorie?> AddSubcategorieAsync(Categorie categorie, int hoofdcategorieId, IEnumerable<int>? subCategorieIds)
        {
            if (categorie == null)
                throw new ArgumentNullException(nameof(categorie));

            if (hoofdcategorieId <= 0)
                throw new InvalidOperationException("Hoofdcategorie is verplicht.");

            var parentExists = await _context.Categorieen
                .AnyAsync(c => c.CategorieId == hoofdcategorieId);

            if (!parentExists)
                throw new InvalidOperationException("Hoofdcategorie bestaat niet.");

            categorie.HoofdCategorieId = hoofdcategorieId;

            _context.Categorieen.Add(categorie);

            await _context.SaveChangesAsync();

            if (subCategorieIds?.Any() == true)
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