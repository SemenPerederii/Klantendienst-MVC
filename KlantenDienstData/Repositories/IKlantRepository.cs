using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public interface IKlantRepository
    {
        Task<IEnumerable<Klant>> GetAllKlantenAsync();
        Task<Klant?> GetKlantAsync(int id);
        Task DisableAccountAsync(int id);
    }
}
