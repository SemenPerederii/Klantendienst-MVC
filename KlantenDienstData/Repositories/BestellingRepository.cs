using KlantenDienstData.Models;
namespace KlantenDienstData.Repositories;
public class BestellingRepository : IBestellingRepository
{
    public Task<Bestelling?> GetBestellingAsync(int id)
    {
        throw new NotImplementedException();
    }
}