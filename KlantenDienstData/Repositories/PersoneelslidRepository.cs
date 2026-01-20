using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;

namespace KlantenDienstData.Repositories;
public class PersoneelslidRepository : IPersoneelslidRepository
{
    private readonly PrulariaDbContext _context;

    public PersoneelslidRepository(PrulariaDbContext context)
    {
        _context = context;
    }

    public async Task<List<PersoneelslidAccount>> GetAllePersoneelslidAccountsAsync()
    {
        return await _context.PersoneelslidAccounts.ToListAsync();
    }

    public async Task<PersoneelslidAccount?> FindByEmailAsync(string email)
    {
        return await _context.PersoneelslidAccounts.SingleOrDefaultAsync(p => p.Emailadres == email);
    }
}