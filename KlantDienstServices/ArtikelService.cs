using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public class ArtikelService : IArtikelService
    {
        private readonly IArtikelRepository _artikelRepository;
        public ArtikelService(IArtikelRepository artikelRepository)
        {
            _artikelRepository = artikelRepository;
        }

        public bool CheckStatusActief(Artikel artikel)
        {
            if (artikel.MinimumVoorraad > 0 || artikel.MaximumVoorraad > 0 || artikel.Bestelpeil > 0 || artikel.AantalBesteldLeverancier > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

        public async Task<List<Artikel>> GetAllArtikelenAsync()
        {
            return await _artikelRepository.GetAllArtikelenAsync();
        }

        public async Task<Artikel> GetArtikelByIdAsync(int id) =>
            await _artikelRepository.GetArtikelByIdAsync(id);

        public async Task<List<Artikel>> ZoekArtikelenOpFilterAsync(ArtikelFilterDto filters)
        {
            IQueryable<Artikel> query = _artikelRepository.GetArtikelQuery();

            if (filters.Id.HasValue)
                query = query.Where(a => a.ArtikelId == filters.Id.Value);

            if (!string.IsNullOrWhiteSpace(filters.Ean))
                query = query.Where(a => a.EAN.Contains(filters.Ean));

            if (!string.IsNullOrWhiteSpace(filters.Naam))
                query = query.Where(a => a.Naam.Contains(filters.Naam));

            if (filters.MinPrijs.HasValue)
                query = query.Where(a => a.Prijs >= filters.MinPrijs.Value);

            if (filters.MaxPrijs.HasValue)
                query = query.Where(a => a.Prijs <= filters.MaxPrijs.Value);

            if (filters.EnkelInVoorraad)
                query = query.Where(a => a.Voorraad > 0);

            if (filters.CategorieIds is { Count: > 0 })
                query = query.Where(a => a.Categorieën.Any(c => filters.CategorieIds.Contains(c.CategorieId)));

            return await query.ToListAsync();
        }

        public async Task<bool> VoegArtikelToeAsync(Artikel artikel) => await _artikelRepository.VoegArtikelToeAsync(artikel);
        public async Task<bool> WijzigArtikelAsync(int artikelId, Artikel nieuwArtikel) => await _artikelRepository.WijzigArtikelAsync(artikelId, nieuwArtikel);
        public async Task<Artikel?> GetArtikelAsync(int id)=>await _artikelRepository.GetArtikelAsync(id);
    }
}
