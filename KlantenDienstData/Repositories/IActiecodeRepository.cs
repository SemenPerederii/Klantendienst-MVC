using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public interface IActiecodeRepository
    {
        Task<IEnumerable<Actiecode>> GetAllAsync();
        Task<Actiecode?> GetByIdAsync(int id);
    }
}
