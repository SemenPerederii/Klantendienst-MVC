using KlantenDienstData.Models;

namespace KlantenDienstData.Repositories
{
    public interface IPersoneelslidRepository
    {
        Task<List<PersoneelslidAccount>> GetAllePersoneelslidAccountsAsync();
        Task<PersoneelslidAccount?> FindByEmailAsync(string email);
        Task<Personeelslid?> FindPersoneelslidByIdAsync(int personeelslidId);
        Task UpdatePaswoordAsync(int id, string nieuwHashPaswoord);
    }
}