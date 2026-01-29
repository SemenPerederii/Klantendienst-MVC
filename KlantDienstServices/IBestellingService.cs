using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public interface IBestellingService
    {
        Task<IEnumerable<Bestelling>> GetAllBestellingenAsync();
    }
}
