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

        public async Task<List<Actiecode>> GetAllAsync()
        {
            return await _context.Actiecodes.ToListAsync();
        }

        public async Task<Actiecode?> GetByIdAsync(int id)
        {
            return await _context.Actiecodes.FindAsync(id);
        }
    }
}
