using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public class LeverancierService
    {
        private readonly ILeverancierRepository _leverancierRepository;
        public LeverancierService(ILeverancierRepository leverancierRepository)
        {
            _leverancierRepository = leverancierRepository;
        }
        public async Task<Leverancier?> GetLeverancierAsync(int id)=> await _leverancierRepository.GetLeverancierAsync(id);
    }
}
