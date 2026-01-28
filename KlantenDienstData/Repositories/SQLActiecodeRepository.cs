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

        public async Task<Actiecode?> GetByIdAsync(int id)
        {
            return await _context.Actiecodes.FindAsync(id);
        }

        public async Task Toevoegen(Actiecode actiecode)
        {
            await _context.Actiecodes.AddAsync(actiecode);
            await _context.SaveChangesAsync();
        }

        public async Task WijzigActieCode(int id, Actiecode nieuweActiecode)
        {
            Actiecode? oud = await _context.Actiecodes.FindAsync(id);
            if(oud != null)
            {
                _context.Entry(oud).CurrentValues.SetValues(nieuweActiecode);
                await _context.SaveChangesAsync();
            }
        }
    }
}
