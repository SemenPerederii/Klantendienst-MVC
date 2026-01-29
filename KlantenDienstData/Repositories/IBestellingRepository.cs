using KlantenDienstData.Models;

namespace KlantenDienstData.Repositories;
public interface IBestellingRepository
{
    Task<Bestelling?> GetBestellingAsync(int id);
    Task<IEnumerable<Bestelling>> GetAllBestellingenAsync();
}