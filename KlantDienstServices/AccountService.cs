using KlantenDienstData.Models;
using KlantenDienstData.Repositories;

namespace KlantenDienstServices;
public class AccountService
{
    private readonly IPersoneelslidRepository _repository;
    public AccountService(IPersoneelslidRepository repository)
    {
        _repository = repository;
    }
    public async Task<PersoneelslidAccount?> Login(string email, string paswoord)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(paswoord))
        {
            return null;
        }
        var personeelslid = await _repository.FindByEmailAsync(email);
        if (personeelslid == null)
        {
            return null;
        }
        if (!VerifyPaswoord(paswoord, personeelslid.Paswoord))
        {
            return null;
        }
        return personeelslid;
    }
    private bool VerifyPaswoord(string paswoord, string paswoordHash)
    {
        return BCrypt.Net.BCrypt.Verify(paswoord, paswoordHash);
    }
    public async Task<Personeelslid?> GetPersoneelslidById(int personeelslidId)
    {
        return await _repository.FindPersoneelslidByIdAsync(personeelslidId);
    }
}