using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public class ArtikelService
    {
        private readonly IArtikelRepository _artikelRepository;
        public ArtikelService(IArtikelRepository artikelRepository)
        {
            _artikelRepository = artikelRepository;
        }

        public async Task<List<Artikel>> GetAllArtikelenAsync()
        {
            return await _artikelRepository.GetAllArtikelenAsync();
        }
        public async Task<bool> VoegArtikelToeAsync(Artikel artikel) => await _artikelRepository.VoegArtikelToeAsync(artikel);
        public async Task<bool> WijzigArtikelAsync(int artikelId, Artikel nieuwArtikel) => await _artikelRepository.WijzigArtikelAsync(artikelId, nieuwArtikel);
    }
}
