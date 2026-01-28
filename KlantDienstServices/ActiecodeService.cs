using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using KlantenDienstServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public class ActiecodeService : IActiecodeService
    {
        private readonly IActiecodeRepository _repositoryActiecode;

        public ActiecodeService(IActiecodeRepository repository)
        {
            _repositoryActiecode = repository;
        }

        public async Task<IEnumerable<Actiecode>> GetAllActiecodesAsync()
        {
            return await _repositoryActiecode.GetAllAsync();
        }
        public async Task<Actiecode?> GetActiecodeByIdAsync(int id)
        {
            return await _repositoryActiecode.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Actiecode>> GetActieveVandaagAsync(DateOnly vandaag)
        {
            return await _repositoryActiecode.GetActiefOpDatumAsync(vandaag);
        }

        public async Task<IEnumerable<Actiecode>> GetNietActieveVandaagAsync(DateOnly vandaag)
        {
            return await _repositoryActiecode.GetNietActiefOpDatumAsync(vandaag);
        }

        public async Task<IEnumerable<Actiecode>> GetActiefOpDatumAsync(DateOnly datum)
        {
            return await _repositoryActiecode.GetActiefOpDatumAsync(datum);
        }

        public ActiecodeStatus GetStatus(Actiecode actie, DateOnly datum)
        {
            return (actie.GeldigVanDatum <= datum && actie.GeldigTotDatum >= datum)
                ? ActiecodeStatus.Actief
                : ActiecodeStatus.NietActief;
        }
    }
}
