using KlantenDienstData.Models;
using KlantenDienstData.Repositories;

namespace KlantenDienstServices
{
    public class BestellingService : IBestellingService
    {
        private readonly IBestellingRepository _repositoryBestelling;
        public BestellingService(IBestellingRepository repositoryBestelling)
        {
            _repositoryBestelling = repositoryBestelling;
        }
        public async Task<IEnumerable<Bestelling>> GetAllBestellingenAsync()
        {
            return await _repositoryBestelling.GetAllBestellingenAsync();
        }
        public async Task<Bestelling?> GetBestellingAsync(int id) => await _repositoryBestelling.GetBestellingAsync(id);
    }
}