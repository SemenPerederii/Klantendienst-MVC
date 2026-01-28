using KlantenDienstData.Models;
using KlantenDienstServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public interface IActiecodeService
    {
        Task<IEnumerable<Actiecode>> GetAllActiecodesAsync();
        Task<Actiecode?> GetActiecodeByIdAsync(int id);
        Task<IEnumerable<Actiecode>> GetActieveVandaagAsync(DateOnly vandaag);
        Task<IEnumerable<Actiecode>> GetNietActieveVandaagAsync(DateOnly vandaag);
        Task<IEnumerable<Actiecode>> GetActiefOpDatumAsync(DateOnly datum);
        ActiecodeStatus GetStatus(Actiecode actie, DateOnly datum);
    }
}
