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
    public async Task<PersoneelslidAccount?> FindByEmailAsync(string email) =>
        await _context.PersoneelslidAccounts
        .Include(pa => pa.Personeelsleden)
            .ThenInclude(p => p.SecurityGroepen)
        .FirstOrDefaultAsync(pa => pa.Emailadres == email);

    public async Task<Personeelslid?> FindPersoneelslidByIdAsync(int personeelslidId)
    {
        return await _context.Personeelsleden
            .Include(p => p.PersoneelslidAccount)
            .FirstOrDefaultAsync(p => p.PersoneelslidId == personeelslidId);
    }
}