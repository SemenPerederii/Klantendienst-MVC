using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using KlantenDienstWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public class KlantService : IKlantService
    {
        private readonly IKlantRepository _repositoryKlant;
        public KlantService(IKlantRepository repository)
        {
            _repositoryKlant = repository;
        }

        public async Task DisableAccountAsync(int id) => await _repositoryKlant.DisableAccountAsync(id);

        public async Task<IEnumerable<KlantOverzichtViewModel>> GetAllKlantenAsync()
        {
            var klanten = await _repositoryKlant.GetAllKlantenAsync();

            return klanten.Select(k => new KlantOverzichtViewModel
            {
                KlantId = k.KlantId,
                Naam = k.Natuurlijkepersonen != null
                    ? $"{k.Natuurlijkepersonen.Voornaam} {k.Natuurlijkepersonen.Familienaam}"
                    : k.Rechtspersonen!.Naam,
                Type = k.Natuurlijkepersonen != null
                    ? "Persoon"
                    : "Bedrijf"
            });
        }

        public Task<Klant?> GetKlantAsync(int id) => _repositoryKlant.GetKlantAsync(id);
    }
}
