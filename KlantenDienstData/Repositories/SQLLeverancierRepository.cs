using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public class SQLLeverancierRepository : ILeverancierRepository
    {
        private readonly PrulariaDbContext _context;

        public SQLLeverancierRepository(PrulariaDbContext context)
        {
            _context = context;
        }
        public async Task<Leverancier?> GetLeverancierAsync(int id)
        {
            return await _context.Leveranciers.FindAsync(id);
        }
    }
}
