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

        public async Task<List<Artikel>> GetAllArtikelenAsync(ArtikelSorteerOpties? sorteerOpties = null, SorteerRichting? sorteerRichting = null)
        {
            var sortOptie = sorteerOpties ?? ArtikelSorteerOpties.Naam;
            var richting = sorteerRichting ?? SorteerRichting.Asc;
            IQueryable<Artikel> query = _context.Artikelen
                .Include(a => a.Categorieën)
                .Include(a => a.Bestellijnen)
                .Include(a => a.Inkomendeleveringslijnen)
                .Include(a => a.Magazijnplaatsen);
            query = sortOptie switch
            {
                ArtikelSorteerOpties.Naam => richting == SorteerRichting.Asc
                    ? query.OrderBy(a => a.Naam)
                    : query.OrderByDescending(a => a.Naam),

                ArtikelSorteerOpties.Prijs => richting == SorteerRichting.Asc
                    ? query.OrderBy(a => a.Prijs)
                    : query.OrderByDescending(a => a.Prijs),

                ArtikelSorteerOpties.Voorraad => richting == SorteerRichting.Asc
                    ? query.OrderBy(a => a.Voorraad)
                    : query.OrderByDescending(a => a.Voorraad),

                _ => query.OrderBy(a => a.Naam)
            };
            return await query.ToListAsync();
        }

        public async Task<Artikel?> GetArtikelAsync(int id)
        {
            //return await _context.Artikelen.FindAsync(id);
            return await _context.Artikelen.Include(artikel => artikel.Categorieën).Where(artikel => artikel.ArtikelId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> VoegArtikelToeAsync(Artikel artikel)
        {
            await _context.Artikelen.AddAsync(artikel);
            await _context.SaveChangesAsync();
            return await _context.Artikelen.ContainsAsync(artikel);
        }

        public async Task<bool> WijzigArtikelAsync(int artikelId, Artikel nieuwArtikel)
        {
            Artikel? oudArtikel = await _context.Artikelen.Where(artikel => artikel.ArtikelId == artikelId).Include(artikel=>artikel.Categorieën).FirstOrDefaultAsync();
            if (oudArtikel == null)
                return false;
            if (oudArtikel.Equals(nieuwArtikel))
                return false;

            

            //pas categorieën aan
            if (nieuwArtikel.Categorieën != null)
            {
                var gewildeIds = nieuwArtikel.Categorieën
                    .Select(c => c.CategorieId)
                    .Where(id => id != 0)
                    .ToList();

                var oudeCategorieën = oudArtikel.Categorieën.ToList();

                //verwijder niet meer gewilde categorieën
                foreach (var teVerwijderen in oudeCategorieën
                    .Where(
                    categorie => 
                    !gewildeIds.Contains(categorie.CategorieId)))
                    oudArtikel.Categorieën.Remove(teVerwijderen);

                //toe tevoegen categorien
                var momenteleIds = oudeCategorieën.Select(categorie => categorie.CategorieId);
                var nogToeTeVoegenIds = gewildeIds.Except(momenteleIds);

                if (nogToeTeVoegenIds.Any())
                {
                    var toeTeVoegenCategorieën = 
                        await _context.Set<Categorie>()
                        .Where(categorie => 
                        nogToeTeVoegenIds.Contains(categorie.CategorieId))
                        .ToListAsync();

                    foreach (var categorie in toeTeVoegenCategorieën)
                    {
                        //geen duplicaten
                        if (!oudArtikel.Categorieën.Any(c => c.CategorieId == categorie.CategorieId))
                            oudArtikel.Categorieën.Add(categorie);
                    }

                }
            }
            else
            {
                oudArtikel.Categorieën.Clear();
            }

            //toepassen
            _context.Entry(oudArtikel).CurrentValues.SetValues(nieuwArtikel);

            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<Artikel> GetArtikelQuery()
        {
            return _context.Artikelen
                .AsNoTracking()
                .Include(a => a.Categorieën); 
        }

    }
}
