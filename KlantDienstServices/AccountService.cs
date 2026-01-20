using KlantenDienstData.Models;
using KlantenDienstData.Repositories;

namespace KlantenDienstServices
{
    public class AccountService //moet nog geinjecteerd worden in program.cs
    {
        public async Task<Personeelslid?> Login(string email, string paswoord)
        {
            var personeelslid = await PersoneelslidRepository.FindByEmailAsync(email);
            if (personeelslid is null || !VerifyPaswoord(paswoord, personeelslid.PersoneelslidAccount.Paswoord))
            {
                return null;
            }
            return personeelslid;
        }

        private bool VerifyPaswoord(string paswoord, string paswoordHash)
        {
            return BCrypt.Net.BCrypt.Verify(paswoord, paswoordHash);
        }
    }
}