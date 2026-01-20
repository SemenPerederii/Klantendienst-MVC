using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;

namespace KlantenDienstData.Repositories
{
    public class PersoneelslidRepository : IPersoneelslidRepository //moeten beide nog geinjecteerd worden in program.cs
    {
        private readonly PrulariaDbContext _context;
        public PersoneelslidRepository(PrulariaDbContext context)
        {
            _context = context;
        }
        public async Task<List<PersoneelslidAccount>> GetAllePersoneelslidAccountsAsync()
        {
            var personeelslidAccounts = _context.PersoneelslidAccounts.ToListAsync();
            return await personeelslidAccounts;
        }
        public async Task<PersoneelslidAccount?> FindByEmailAsync(string email)
        {
            return await _context.PersoneelslidAccounts.FindAsync(email);
        }
    }
}