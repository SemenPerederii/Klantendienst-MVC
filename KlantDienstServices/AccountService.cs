using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using KlantenDienstServices.DTO_s;

namespace KlantenDienstServices;
public class AccountService
{
    private readonly IPersoneelslidRepository _repository;
    public AccountService(IPersoneelslidRepository repository)
    {
        _repository = repository;
    }
    public async Task<PersoneelslidAccount?> Login(LogInInfoDTO logInInfo)
    {
        if (string.IsNullOrWhiteSpace(logInInfo.Emailadres) || string.IsNullOrWhiteSpace(logInInfo.Paswoord))
        {
            return null;
        }
        var personeelslid = await _repository.FindByEmailAsync(logInInfo.Emailadres);
        if (personeelslid == null)
        {
            return null;
        }
        if (!VerifyPaswoord(logInInfo.Paswoord, personeelslid.Paswoord))
        {
            return null;
        }
        if(!personeelslid.Personeelsleden.Any(p => p.SecurityGroepen.Any(s => s.SecurityGroepId == 2)))
        {
            logInInfo.ErrorMessage = "Geen toegang tot klantenservice.";
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

    public async Task WijzigPaswoord(int id, string nieuwPaswoord) =>
       await _repository.UpdatePaswoordAsync(id, BCrypt.Net.BCrypt.HashPassword(nieuwPaswoord));
}