using KlantenDienstData.Models;

namespace KlantenDienstData.Repositories
{
    public interface ILoginRepository
    {
        Task<List<PersoneelslidAccount>> GetAllePersoneelslidAccounts();
        //Task EmailEnPaswoordCombinationBestaatAsync(string email, string paswoord);
    }
}