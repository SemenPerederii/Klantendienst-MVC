using KlantenDienstData.Models;
using KlantenDienstWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public interface IKlantService
    {
        Task<IEnumerable<KlantOverzichtViewModel>> GetAllKlantenAsync();
        Task<Klant?> GetKlantAsync(int id);
    }
}
