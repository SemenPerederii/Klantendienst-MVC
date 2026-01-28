using KlantenDienstData.Enums;
using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public class SQLActiecodeRepository : IActiecodeRepository
    {
        private readonly PrulariaDbContext _context;

        public SQLActiecodeRepository(PrulariaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actiecode>> GetAllAsync()
        {
            return await _context.Actiecodes.ToListAsync();
        }

        public IQueryable<Actiecode> GetActiecodeQuery()
        {
            return _context.Actiecodes.AsNoTracking();
        }
        public async Task<Actiecode?> GetByIdAsync(int id)
        {
            return await _context.Actiecodes.FindAsync(id);
        }
    }
}
