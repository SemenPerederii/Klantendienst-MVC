using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;

namespace KlantenDienstData.Repositories
{
    public class SQLLoginRepository : ILoginRepository
    {
        private readonly PrulariaDbContext _context;
        public SQLLoginRepository(PrulariaDbContext context)
        {
            _context = context;
        }
        public async Task<List<PersoneelslidAccount>> GetAllePersoneelslidAccounts()
        {
            var personeelslidAccounts = _context.PersoneelslidAccounts.ToListAsync();
            return await personeelslidAccounts;
        }
        //public async Task EmailEnPaswoordCombinationBestaatAsync(string email, string paswoord)
        //{
        //    return await NotImplementedException();
        //}
    }
}