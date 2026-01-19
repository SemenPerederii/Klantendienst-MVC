using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public class SqlArtikelRepository : IArtikelRepository
    {
        private readonly PrulariaDbContext _context;

        public SqlArtikelRepository(PrulariaDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Artikel>> GetAllArtikelenAsync()
        {
            throw new NotImplementedException();
        }
    }
}
