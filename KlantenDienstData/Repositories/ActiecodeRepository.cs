using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public class ActiecodeRepository : IActiecodeRepository
    {
        private readonly PrulariaDbContext _context;

        public ActiecodeRepository(PrulariaDbContext context)
        {
            _context = context;
        }
    }
}
